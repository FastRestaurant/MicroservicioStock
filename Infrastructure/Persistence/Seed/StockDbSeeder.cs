using System.Security.Cryptography;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seed;

public static class StockDbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        foreach (var ingredient in BuildIngredients())
            await EnsureIngredientAsync(context, ingredient.Name, ingredient.Count);

        
        await context.SaveChangesAsync();

        var ingredientsByName = await context.Ingredient
            .ToDictionaryAsync(ingredient => ingredient.Name, ingredient => ingredient.Id);

        foreach (var drink in BuildDrinks())
            await EnsureDrinkStockAsync(context, drink.Name, drink.Count);

        foreach (var recipe in BuildRecipes())
            await EnsureRecipeAsync(context, recipe.DishName, recipe.Items, ingredientsByName);

        await context.SaveChangesAsync();
    }

    private static async Task EnsureIngredientAsync(AppDbContext context, string name, int count)
    {
        var existing = await context.Ingredient
            .Include(ingredient => ingredient.Stock)
            .FirstOrDefaultAsync(ingredient => ingredient.Name == name);

        if (existing is not null)
        {
            if (existing.Stock.Count <= 0)
                existing.Stock.Count = count;

            return;
        }

        var stockId = StableId($"stock:ingredient:{name}");
        context.Stock.Add(new Stock
        {
            Id = stockId,
            Count = count
        });

        context.Ingredient.Add(new Ingredient
        {
            Id = StableId($"ingredient:{name}"),
            Name = name,
            Id_Stock = stockId
        });
    }

    private static async Task EnsureDrinkStockAsync(AppDbContext context, string name, int count)
    {
        var drinkId = StableId($"drink:{name}");
        var existing = await context.Stock.FirstOrDefaultAsync(stock => stock.Id_Drink == drinkId);
        if (existing is not null)
        {
            if (existing.Count <= 0)
                existing.Count = count;

            return;
        }

        context.Stock.Add(new Stock
        {
            Id = StableId($"stock:drink:{name}"),
            Count = count,
            Id_Drink = drinkId
        });
    }

    private static async Task EnsureRecipeAsync(
        AppDbContext context,
        string dishName,
        IReadOnlyCollection<RecipeItem> items,
        IReadOnlyDictionary<string, Guid> ingredientsByName)
    {
        var dishId = StableId($"dish:{dishName}");

        foreach (var item in items)
        {
            var ingredientId = ingredientsByName[item.IngredientName];
            var existing = await context.IngredientDish
                .FirstOrDefaultAsync(recipe => recipe.Id_Dish == dishId && recipe.Id_Ingredient == ingredientId);

            if (existing is null)
            {
                context.IngredientDish.Add(new IngredientDish
                {
                    IdIngredientDish = StableId($"recipe:{dishName}:{item.IngredientName}"),
                    Id_Dish = dishId,
                    Id_Ingredient = ingredientId,
                    RequiredQuantity = item.RequiredQuantity
                });

                continue;
            }

            existing.RequiredQuantity = item.RequiredQuantity;
        }
    }

    private static List<IngredientSeed> BuildIngredients() => new()
    {
        new("Carne vacuna", 100000),
        new("Carne picada", 60000),
        new("Pollo", 80000),
        new("Jamon", 40000),
        new("Queso", 60000),
        new("Queso provolone", 30000),
        new("Papa", 120000),
        new("Calamar", 40000),
        new("Pan rallado", 30000),
        new("Huevo", 1000),
        new("Pan de hamburguesa", 300),
        new("Lechuga", 25000),
        new("Tomate", 50000),
        new("Harina", 80000),
        new("Ricota", 40000),
        new("Pure de papa", 50000),
        new("Salsa de tomate", 60000),
        new("Salsa blanca", 30000),
        new("Dulce de leche", 20000),
        new("Leche", 60000),
        new("Azucar", 40000),
        new("Pan", 30000),
        new("Helado", 40000),
        new("Queso crema", 25000),
        new("Frutos rojos", 15000),
        new("Cafe", 10000),
        new("Cacao", 10000),
        new("Vainillas", 20000),
        new("Cebolla", 25000),
        new("Aceite", 50000),
        new("Limon", 20000)
    };

    private static List<DrinkSeed> BuildDrinks() => new()
    {
        new("Coca-Cola 500ml", 300),
        new("Agua mineral 500ml", 300),
        new("Sprite 500ml", 300),
        new("Fanta 500ml", 300),
        new("Agua con gas 500ml", 300),
        new("Jugo de naranja", 200),
        new("Limonada", 200),
        new("Cerveza Quilmes", 250),
        new("Cerveza Stella Artois", 250),
        new("Café", 500)
    };

    private static List<RecipeSeed> BuildRecipes() => new()
    {
        Recipe("Empanadas de carne", Item("Carne vacuna", 120), Item("Cebolla", 40), Item("Harina", 80), Item("Aceite", 10)),
        Recipe("Empanadas de jamón y queso", Item("Jamon", 80), Item("Queso", 80), Item("Harina", 80)),
        Recipe("Provoleta", Item("Queso provolone", 200), Item("Aceite", 5)),
        Recipe("Papas fritas", Item("Papa", 350), Item("Aceite", 30)),
        Recipe("Rabas", Item("Calamar", 250), Item("Harina", 80), Item("Aceite", 30), Item("Limon", 30)),
        Recipe("Milanesa napolitana", Item("Carne vacuna", 250), Item("Pan rallado", 80), Item("Huevo", 2), Item("Salsa de tomate", 80), Item("Jamon", 60), Item("Queso", 100)),
        Recipe("Milanesa con papas", Item("Carne vacuna", 250), Item("Pan rallado", 80), Item("Huevo", 2), Item("Papa", 250)),
        Recipe("Bife de chorizo", Item("Carne vacuna", 350), Item("Papa", 250)),
        Recipe("Pollo grillado", Item("Pollo", 300), Item("Lechuga", 80), Item("Tomate", 120)),
        Recipe("Suprema a la suiza", Item("Pollo", 280), Item("Pan rallado", 80), Item("Huevo", 2), Item("Salsa blanca", 100), Item("Queso", 120)),
        Recipe("Hamburguesa completa", Item("Carne picada", 180), Item("Pan de hamburguesa", 1), Item("Lechuga", 40), Item("Tomate", 60), Item("Queso", 40), Item("Jamon", 40), Item("Huevo", 1)),
        Recipe("Ravioles de ricota", Item("Harina", 140), Item("Ricota", 160), Item("Salsa de tomate", 120), Item("Queso", 30)),
        Recipe("Sorrentinos de jamón y queso", Item("Harina", 150), Item("Jamon", 100), Item("Queso", 120), Item("Salsa de tomate", 120)),
        Recipe("Tallarines caseros", Item("Harina", 180), Item("Huevo", 2), Item("Salsa de tomate", 120)),
        Recipe("Ñoquis de papa", Item("Pure de papa", 250), Item("Harina", 80), Item("Salsa de tomate", 120)),
        Recipe("Flan casero", Item("Leche", 250), Item("Huevo", 2), Item("Azucar", 80), Item("Dulce de leche", 60)),
        Recipe("Budín de pan", Item("Pan", 140), Item("Leche", 200), Item("Huevo", 2), Item("Azucar", 80)),
        Recipe("Helado artesanal", Item("Helado", 220)),
        Recipe("Cheesecake", Item("Queso crema", 120), Item("Azucar", 50), Item("Frutos rojos", 60), Item("Harina", 40)),
        Recipe("Tiramisú", Item("Cafe", 80), Item("Cacao", 20), Item("Vainillas", 100), Item("Queso crema", 100))
    };

    private static RecipeSeed Recipe(string dishName, params RecipeItem[] items)
        => new(dishName, items);

    private static RecipeItem Item(string ingredientName, int requiredQuantity)
        => new(ingredientName, requiredQuantity);

    private static Guid StableId(string key)
    {
        var bytes = MD5.HashData(Encoding.UTF8.GetBytes(key));
        return new Guid(bytes);
    }

    private sealed record IngredientSeed(string Name, int Count);
    private sealed record DrinkSeed(string Name, int Count);
    private sealed record RecipeSeed(string DishName, IReadOnlyCollection<RecipeItem> Items);
    private sealed record RecipeItem(string IngredientName, int RequiredQuantity);
}
