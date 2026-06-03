using Application.DTOs.IngredientsDTO;
using Application.Interfaces.Handlers.Ingredient;
using Application.UseCases.Ingredient.Commands;
using Application.UseCases.Ingredient.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioStock.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly ICreateIngredientHandler _createIngredientHandler;
        private readonly IDeleteIngredientHandler _deleteIngredientHandler;
        private readonly IUpdateIngredientHandler _updateIngredientHandler;
        private readonly IGetAllIngredientHandler _getAllIngredientHandler;
        private readonly IGetByIdIngredientHandler _getByIdIngredientHandler;

        public IngredientController(ICreateIngredientHandler createIngredientHandler, IDeleteIngredientHandler deleteIngredientHandler, IUpdateIngredientHandler updateIngredientHandler, IGetAllIngredientHandler getAllIngredientHandler, IGetByIdIngredientHandler getByIdIngredientHandler)
        {
            _createIngredientHandler = createIngredientHandler;
            _deleteIngredientHandler = deleteIngredientHandler;
            _updateIngredientHandler = updateIngredientHandler;
            _getAllIngredientHandler = getAllIngredientHandler;
            _getByIdIngredientHandler = getByIdIngredientHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromBody] string ingredient)
        {
            var command = new CreateIngredientCommand(ingredient);
            var result = await _createIngredientHandler.Handle(command);
            if (result != "OK")
                return BadRequest(result);
            return StatusCode(201, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
            var command = new DeleteIngredientCommand(id);
            var result = await _deleteIngredientHandler.Handle(command);
            if (result != "OK")
                return NotFound(result);
            return StatusCode(204, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredient(Guid id, [FromBody] string ingredient)
        {
            var command = new UpdateIngredientCommand(ingredient);
            var result = await _updateIngredientHandler.Handle(id, command);
            if (result != "OK")
                return NotFound(result);
            return StatusCode(204, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            var query = new GetAllIngredientsQuery();
            var (ingredients, message) = await _getAllIngredientHandler.Handle(query);
            if (message != "OK")
                return NotFound(message);
            return Ok(ingredients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdIngredient(Guid id)
        {
            var query = new GetByIdIngredientQuery(id);
            var (ingredient, message) = await _getByIdIngredientHandler.Handle(query);
            if (message != "OK")
                return NotFound(message);
            return Ok(ingredient);
        }


    }
}
