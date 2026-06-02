using Application.Interfaces.Handlers.Ingredient;
using Application.Interfaces.Handlers.IngredientDish;
using Application.UseCases.Ingredient.Commands;
using Application.UseCases.Ingredient.Queries;
using Application.UseCases.IngredientDish.Commands;
using Application.UseCases.IngredientDish.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroservicioStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientDishController : ControllerBase
    {
        private readonly ICreateIngredientDishHandler _createIngredientDishHandler;
        private readonly IDeleteIngredientDishHandler _deleteIngredientDishHandler;
        private readonly IGetAllIngredientDishHandler _getAllIngredientDishHandler;
        private readonly IGetByIdIngredientDishHandler _getByIdIngredientDishHandler;

        public IngredientDishController(ICreateIngredientDishHandler createIngredientDishHandler, IDeleteIngredientDishHandler deleteIngredientDishHandler, IGetAllIngredientDishHandler getAllIngredientDishHandler, IGetByIdIngredientDishHandler getByIdIngredientDishHandler)
        {
            _createIngredientDishHandler = createIngredientDishHandler;
            _deleteIngredientDishHandler = deleteIngredientDishHandler;
            _getAllIngredientDishHandler = getAllIngredientDishHandler;
            _getByIdIngredientDishHandler = getByIdIngredientDishHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredientDish([FromBody] Guid id_ingredient, Guid id_dish)
        {
            var command = new CreateIngredientDishCommand(id_ingredient, id_dish);
            var result = await _createIngredientDishHandler.Handle(command);
            if (result != "OK")
                return BadRequest(result);
            return StatusCode(201, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientDish(Guid id)
        {
            var command = new DeleteIngredientDishCommand(id);
            var result = await _deleteIngredientDishHandler.Handle(command);
            if (result != "OK")
                return NotFound(result);
            return StatusCode(204, result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllIngredientDishes()
        {
            var query = new GetAllIngredientDishQuery();
            var (ingredientDishes, message) = await _getAllIngredientDishHandler.Handle(query);
            if (message != "OK")
                return NotFound(message);
            return Ok(ingredientDishes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdIngredientDish(Guid id)
        {
            var query = new GetByIdIngredientDishQuery(id);
            var (ingredientDish, message) = await _getByIdIngredientDishHandler.Handle(query);
            if (message != "OK")
                return NotFound(message);
            return Ok(ingredientDish);
        }
    }
}
