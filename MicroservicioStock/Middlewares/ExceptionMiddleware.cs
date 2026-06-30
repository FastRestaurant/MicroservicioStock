using Application.DTOs;
using Domain.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MicroservicioStock.Middlewares
{
    public sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException exception)
            {
                await WriteErrorAsync(context, StatusCodes.Status400BadRequest, exception.Message);
            }
            catch (NotFoundException exception)
            {
                await WriteErrorAsync(context, StatusCodes.Status404NotFound, exception.Message);
            }
            catch (ConflictException exception)
            {
                await WriteErrorAsync(context, StatusCodes.Status409Conflict, exception.Message);
            }
            catch (UnauthorizedException exception)
            {
                await WriteErrorAsync(context, StatusCodes.Status401Unauthorized, exception.Message);
            }
            catch (DomainException exception)
            {
                await WriteErrorAsync(context, StatusCodes.Status400BadRequest, exception.Message);
            }
            catch (DbUpdateConcurrencyException)
            {
                await WriteErrorAsync(context, StatusCodes.Status409Conflict, "El recurso fue modificado por otro usuario. Recargue e intente nuevamente.");
            }
            catch (DbUpdateException exception) when (IsExpectedDataConflict(exception))
            {
                await WriteErrorAsync(context, StatusCodes.Status409Conflict, "El recurso no se pudo modificar debido a un conflicto de datos.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Ocurrio una excepcion no controlada mientras se procesaba la solicitud.");
                await WriteErrorAsync(context, StatusCodes.Status500InternalServerError, "Ocurrio un error inesperado.");
            }
        }

        private static Task WriteErrorAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsJsonAsync(new ErrorResponseDTO
            {
                Message = message,
                StatusCode = statusCode,
                Timestamp = DateTime.UtcNow
            });
        }

        private static bool IsExpectedDataConflict(DbUpdateException exception)
        {
            return exception.InnerException is SqlException sqlException &&
                (sqlException.Number == 2601 || sqlException.Number == 2627 || sqlException.Number == 547);
        }
    }
}
