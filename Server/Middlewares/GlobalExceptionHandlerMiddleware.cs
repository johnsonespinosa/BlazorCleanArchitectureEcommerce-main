using Application.Commons.Exceptions;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;

namespace Server.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
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
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/problem+json";

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Se produjo un error",
                Type = exception.GetType().Name,
                Detail = exception.Message,
                Instance = context.Request.Path // Proporciona la ruta que causó el error
            };

            switch (exception)
            {
                case ValidationException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    break;
                case NotFoundException:
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    break;
                case UnauthorizedAccessException:
                    problemDetails.Status = StatusCodes.Status401Unauthorized;
                    break;
                case ArgumentNullException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    break;
                default:
                    // Log la excepción para el seguimiento
                    _logger.LogError(exception, "An unexpected error occurred.");
                    break;
            }

            return context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
