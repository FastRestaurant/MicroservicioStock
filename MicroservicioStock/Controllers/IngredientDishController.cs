using Application.DTOs.IngredientDishDTO;
using Application.DTOs.IngredientsDTO;
using Application.Interfaces.Handlers.Ingredient;
using Application.Interfaces.Handlers.IngredientDish;
using Application.UseCases.Ingredient.Commands;
using Application.UseCases.Ingredient.Handlers;
using Application.UseCases.Ingredient.Queries;
using Application.UseCases.IngredientDish.Commands;
using Application.UseCases.IngredientDish.Handlers;
using Application.UseCases.IngredientDish.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioStock.Controllers
{
    [Route("api/v1/ingredient-dishes")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class IngredientDishController : ControllerBase
    {
        private readonly ICreateIngredientDishHandler _createIngredientDishHandler;
        private readonly IDeleteIngredientDishHandler _deleteIngredientDishHandler;
        private readonly IGetAllIngredientDishHandler _getAllIngredientDishHandler;
        private readonly IGetByIdIngredientDishHandler _getByIdIngredientDishHandler;
        private readonly IUpdateIngredientDishHandler _updateIngredientDishHandler;
        private readonly IGetIngredientDishesByDishHandler _getIngredientDishesByDishHandler;
        private readonly IReplaceDishIngredientsHandler _replaceDishIngredientsHandler;

        public IngredientDishController(
            ICreateIngredientDishHandler createIngredientDishHandler,
            IDeleteIngredientDishHandler deleteIngredientDishHandler,
            IGetAllIngredientDishHandler getAllIngredientDishHandler,
            IGetByIdIngredientDishHandler getByIdIngredientDishHandler,
            IUpdateIngredientDishHandler updateIngredientDishHandler,
            IGetIngredientDishesByDishHandler getIngredientDishesByDishHandler,
            IReplaceDishIngredientsHandler replaceDishIngredientsHandler)
        {
            _createIngredientDishHandler = createIngredientDishHandler;
            _deleteIngredientDishHandler = deleteIngredientDishHandler;
            _getAllIngredientDishHandler = getAllIngredientDishHandler;
            _getByIdIngredientDishHandler = getByIdIngredientDishHandler;
            _updateIngredientDishHandler = updateIngredientDishHandler;
            _getIngredientDishesByDishHandler = getIngredientDishesByDishHandler;
            _replaceDishIngredientsHandler = replaceDishIngredientsHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredientDish([FromBody] IngredientDishRequestDTO dto)
        {
            var command = new CreateIngredientDishCommand(dto.Id_Ingredient, dto.Id_Dish, dto.RequiredQuantity);
            await _createIngredientDishHandler.Handle(command);
            return Created(string.Empty, null);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientDish(Guid id)
        {
            var command = new DeleteIngredientDishCommand(id);
            await _deleteIngredientDishHandler.Handle(command);
            return NoContent();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllIngredientDishes()
        {
            var query = new GetAllIngredientDishQuery();
            var ingredientDishes = await _getAllIngredientDishHandler.Handle(query);
            return Ok(ingredientDishes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdIngredientDish(Guid id)
        {
            var query = new GetByIdIngredientDishQuery(id);
            var ingredientDish = await _getByIdIngredientDishHandler.Handle(query);
            return Ok(ingredientDish);
        }

        [HttpGet("dish/{dishId}")]
        public async Task<IActionResult> GetByDish(Guid dishId)
        {
            var query = new GetIngredientDishesByDishQuery(dishId);
            var ingredientDishes = await _getIngredientDishesByDishHandler.Handle(query);
            return Ok(ingredientDishes);
        }

        [HttpPut("dish/{dishId}")]
        public async Task<IActionResult> ReplaceByDish(Guid dishId, [FromBody] ReplaceDishIngredientsRequestDTO request)
        {
            var command = new ReplaceDishIngredientsCommand(dishId, request.Items);
            await _replaceDishIngredientsHandler.Handle(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredientDish(Guid id, [FromBody] IngredientDishRequestDTO ingredientDish)
        {
            var command = new UpdateIngredientDishCommand(ingredientDish.RequiredQuantity);
            await _updateIngredientDishHandler.Handle(id, command);
            return NoContent();
        }
    }
}
