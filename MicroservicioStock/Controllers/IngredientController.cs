using Application.DTOs.IngredientsDTO;
using Application.Interfaces.Handlers.Ingredient;
using Application.UseCases.Ingredient.Commands;
using Application.UseCases.Ingredient.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioStock.Controllers
{
    [Route("api/v1/ingredients")]
    [ApiController]
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientRequestDTO ingredient)
        {
            var command = new CreateIngredientCommand(ingredient.Name, ingredient.InitialStock, ingredient.UnitType);
            await _createIngredientHandler.Handle(command);
            return Created(string.Empty, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
            var command = new DeleteIngredientCommand(id);
            await _deleteIngredientHandler.Handle(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredient(Guid id, [FromBody] IngredientRequestDTO ingredient)
        {
            var command = new UpdateIngredientCommand(ingredient.Name);
            await _updateIngredientHandler.Handle(id, command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            var query = new GetAllIngredientsQuery();
            var ingredients = await _getAllIngredientHandler.Handle(query);
            return Ok(ingredients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdIngredient(Guid id)
        {
            var query = new GetByIdIngredientQuery(id);
            var ingredient = await _getByIdIngredientHandler.Handle(query);
            return Ok(ingredient);
        }


    }
}
