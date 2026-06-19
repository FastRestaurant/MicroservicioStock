using System.Data;
using Application.DTOs.Stock;
using Application.Interfaces.Repositories;
using Application.UseCases.Stock.Commands;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StockConsumptionRepository : IStockConsumptionRepository
    {
        private const string ConsumeMovement = "Consume";
        private const string ReleaseMovement = "Release";
        private const string DishProductType = "Dish";
        private const string DrinkProductType = "Drink";

        private readonly AppDbContext _context;

        public StockConsumptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<StockOperationResultDTO> ConsumeForOrderAsync(ConsumeStockForOrderCommand command, CancellationToken cancellationToken = default)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);

            var alreadyConsumed = await _context.StockMovements.AnyAsync(m =>
                m.OrderItemId == command.OrderItemId &&
                m.MovementType == ConsumeMovement,
                cancellationToken);

            if (alreadyConsumed)
            {
                await transaction.CommitAsync(cancellationToken);
                return StockOperationResultDTO.Ok("El stock ya fue descontado para este item.");
            }

            var requirements = await GetRequirementsAsync(command, cancellationToken);
            if (requirements.Count == 0)
            {
                await transaction.CommitAsync(cancellationToken);
                throw new DomainException("No hay stock configurado para el producto solicitado.");
            }

            foreach (var requirement in requirements)
            {
                var updated = await TryDecreaseStockAsync(requirement.StockId, requirement.RequiredQuantity, cancellationToken);
                if (!updated)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw new DomainException($"Stock insuficiente para {requirement.Name}.");
                }

                _context.StockMovements.Add(new StockMovement
                {
                    OrderId = command.OrderId,
                    OrderItemId = command.OrderItemId,
                    ProductId = command.ProductId,
                    ProductType = command.ProductType,
                    StockId = requirement.StockId,
                    IngredientId = requirement.IngredientId,
                    Quantity = requirement.RequiredQuantity,
                    MovementType = ConsumeMovement,
                    CreatedAt = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return StockOperationResultDTO.Ok("Stock descontado correctamente.");
        }

        public async Task<StockOperationResultDTO> ReleaseForOrderAsync(ReleaseStockForOrderCommand command, CancellationToken cancellationToken = default)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);

            var consumeMovements = await _context.StockMovements
                .Where(m => m.OrderId == command.OrderId &&
                            m.OrderItemId == command.OrderItemId &&
                            m.MovementType == ConsumeMovement)
                .ToListAsync(cancellationToken);

            if (consumeMovements.Count == 0)
            {
                await transaction.CommitAsync(cancellationToken);
                return StockOperationResultDTO.Ok("No habia stock consumido para liberar.");
            }

            var releasedStockIds = await _context.StockMovements
                .Where(m => m.OrderId == command.OrderId &&
                            m.OrderItemId == command.OrderItemId &&
                            m.MovementType == ReleaseMovement)
                .Select(m => m.StockId)
                .ToListAsync(cancellationToken);

            var pendingReleaseMovements = consumeMovements
                .Where(m => !releasedStockIds.Contains(m.StockId))
                .ToList();

            if (pendingReleaseMovements.Count == 0)
            {
                await transaction.CommitAsync(cancellationToken);
                return StockOperationResultDTO.Ok("El stock ya habia sido liberado para este item.");
            }

            foreach (var movement in pendingReleaseMovements)
            {
                await IncreaseStockAsync(movement.StockId, movement.Quantity, cancellationToken);

                _context.StockMovements.Add(new StockMovement
                {
                    OrderId = movement.OrderId,
                    OrderItemId = movement.OrderItemId,
                    ProductId = movement.ProductId,
                    ProductType = movement.ProductType,
                    StockId = movement.StockId,
                    IngredientId = movement.IngredientId,
                    Quantity = movement.Quantity,
                    MovementType = ReleaseMovement,
                    CreatedAt = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return StockOperationResultDTO.Ok("Stock liberado correctamente.");
        }

        private async Task<List<StockRequirement>> GetRequirementsAsync(ConsumeStockForOrderCommand command, CancellationToken cancellationToken)
        {
            if (command.ProductType.Equals(DishProductType, StringComparison.OrdinalIgnoreCase))
                return await GetDishRequirementsAsync(command, cancellationToken);

            if (command.ProductType.Equals(DrinkProductType, StringComparison.OrdinalIgnoreCase))
                return await GetDrinkRequirementsAsync(command, cancellationToken);

            return new List<StockRequirement>();
        }

        private async Task<List<StockRequirement>> GetDishRequirementsAsync(ConsumeStockForOrderCommand command, CancellationToken cancellationToken)
        {
            var recipeItems = await _context.IngredientDish
                .Include(x => x.Ingredient)
                .ThenInclude(x => x.Stock)
                .Where(x => x.Id_Dish == command.ProductId)
                .ToListAsync(cancellationToken);

            return recipeItems
                .Where(x => x.Ingredient.Stock != null)
                .Select(x => new StockRequirement(
                    x.Ingredient.Id_Stock,
                    x.Id_Ingredient,
                    x.Ingredient.Name,
                    x.RequiredQuantity * command.Quantity,
                    x.Ingredient.Stock.Count))
                .ToList();
        }

        private async Task<List<StockRequirement>> GetDrinkRequirementsAsync(ConsumeStockForOrderCommand command, CancellationToken cancellationToken)
        {
            var stock = await _context.Stock
                .FirstOrDefaultAsync(x => x.Id_Drink == command.ProductId, cancellationToken);

            if (stock == null)
                return new List<StockRequirement>();

            return new List<StockRequirement>
            {
                new(stock.Id, null, "Bebida", command.Quantity, stock.Count)
            };
        }

        private async Task<bool> TryDecreaseStockAsync(Guid stockId, int quantity, CancellationToken cancellationToken)
        {
            var affectedRows = await _context.Database.ExecuteSqlInterpolatedAsync(
                $"UPDATE [Stock] SET [Count] = [Count] - {quantity} WHERE [Id] = {stockId} AND [Count] >= {quantity}",
                cancellationToken);

            return affectedRows == 1;
        }

        private async Task IncreaseStockAsync(Guid stockId, int quantity, CancellationToken cancellationToken)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"UPDATE [Stock] SET [Count] = [Count] + {quantity} WHERE [Id] = {stockId}",
                cancellationToken);
        }

        private sealed record StockRequirement(Guid StockId, Guid? IngredientId, string Name, int RequiredQuantity, int AvailableQuantity);
    }
}
