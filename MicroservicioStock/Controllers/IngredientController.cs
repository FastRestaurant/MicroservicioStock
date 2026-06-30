using Application.DTOs;
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientRequestDTO ingredient)
        {
            var command = new CreateIngredientCommand(ingredient.Name, ingredient.InitialStock, ingredient.UnitType);
            await _createIngredientHandler.Handle(command);
            return Created(string.Empty, null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
            var command = new DeleteIngredientCommand(id);
            await _deleteIngredientHandler.Handle(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateIngredient(Guid id, [FromBody] IngredientRequestDTO ingredient)
        {
            var command = new UpdateIngredientCommand(ingredient.Name, ingredient.UnitType, ingredient.RowVersion);
            await _updateIngredientHandler.Handle(id, command);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResponseDTO<IngredientResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllIngredients(int pageNumber = 1, int pageSize = 10, string? search = null)
        {
            var query = new GetAllIngredientsQuery(pageNumber, pageSize, search);
            var ingredients = await _getAllIngredientHandler.Handle(query);
            return Ok(ingredients);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IngredientResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdIngredient(Guid id)
        {
            var query = new GetByIdIngredientQuery(id);
            var ingredient = await _getByIdIngredientHandler.Handle(query);
            return Ok(ingredient);
        }


    }
}
