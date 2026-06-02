using Application.DTOs.Stock;
using Application.Interfaces.Handlers.Stock;
using Application.UseCases.Stock.Commands;
using Application.UseCases.Stock.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ICreateStockHandler _createStockHandler;
        private readonly IUpdateStockHandler _updateStockHandler;
        private readonly IDeleteStockHandler _deleteStockHandler;
        private readonly IGetAllStockHandler _getAllStockHandler;
        private readonly IGetByIdStockHandler _getByIdStockHandler;

        public StockController(
            ICreateStockHandler createStockHandler,
            IUpdateStockHandler updateStockHandler,
            IDeleteStockHandler deleteStockHandler,
            IGetAllStockHandler getAllStockHandler,
            IGetByIdStockHandler getByIdStockHandler)
        {
            _createStockHandler = createStockHandler;
            _updateStockHandler = updateStockHandler;
            _deleteStockHandler = deleteStockHandler;
            _getAllStockHandler = getAllStockHandler;
            _getByIdStockHandler = getByIdStockHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _getAllStockHandler.Handle(
                new GetAllStockQuery());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var stock = await _getByIdStockHandler.Handle(
                new GetByIdStockQuery(id));

            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateStockCommand command)
        {
            var id = await _createStockHandler.Handle(command);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] StockRequestDTO dto)
        {
            var command = new UpdateStockCommand(dto.Count);

            await _updateStockHandler.Handle(id,command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deleteStockHandler.Handle(
                new DeleteStockCommand(id));

            return NoContent();
        }
    }
}