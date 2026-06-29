using Application.DTOs.Stock;
using Application.Interfaces.Handlers.Stock;
using Application.UseCases.Stock.Commands;
using Application.UseCases.Stock.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioStock.Controllers
{
    [ApiController]
    [Route("api/v1/stocks")]
    public class StockController : ControllerBase
    {
        private readonly ICreateStockHandler _createStockHandler;
        private readonly IUpdateStockHandler _updateStockHandler;
        private readonly IDeleteStockHandler _deleteStockHandler;
        private readonly IGetAllStockHandler _getAllStockHandler;
        private readonly IGetByIdStockHandler _getByIdStockHandler;
        private readonly IConsumeStockForOrderHandler _consumeStockForOrderHandler;
        private readonly IReleaseStockForOrderHandler _releaseStockForOrderHandler;

        public StockController(
            ICreateStockHandler createStockHandler,
            IUpdateStockHandler updateStockHandler,
            IDeleteStockHandler deleteStockHandler,
            IGetAllStockHandler getAllStockHandler,
            IGetByIdStockHandler getByIdStockHandler,
            IConsumeStockForOrderHandler consumeStockForOrderHandler,
            IReleaseStockForOrderHandler releaseStockForOrderHandler)
        {
            _createStockHandler = createStockHandler;
            _updateStockHandler = updateStockHandler;
            _deleteStockHandler = deleteStockHandler;
            _getAllStockHandler = getAllStockHandler;
            _getByIdStockHandler = getByIdStockHandler;
            _consumeStockForOrderHandler = consumeStockForOrderHandler;
            _releaseStockForOrderHandler = releaseStockForOrderHandler;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _getAllStockHandler.Handle(
                new GetAllStockQuery());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var stock = await _getByIdStockHandler.Handle(
                new GetByIdStockQuery(id));

            return Ok(stock);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            [FromBody] CreateStockCommand command)
        {
            await _createStockHandler.Handle(command);

            return Created(string.Empty, null);
        }

        [HttpPost("consumptions")]
        [Authorize(Roles = "Admin,Waitress")]
        public async Task<IActionResult> ConsumeForOrder(
            [FromBody] ConsumeStockForOrderCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _consumeStockForOrderHandler.Handle(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("releases")]
        [Authorize(Roles = "Admin,Waitress,Kitchen")]
        public async Task<IActionResult> ReleaseForOrder(
            [FromBody] ReleaseStockForOrderCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _releaseStockForOrderHandler.Handle(command, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] StockRequestDTO dto)
        {
            var command = new UpdateStockCommand(dto.Count, dto.RowVersion);

            await _updateStockHandler.Handle(id, command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deleteStockHandler.Handle(
                new DeleteStockCommand(id));

            return NoContent();
        }
    }
}
