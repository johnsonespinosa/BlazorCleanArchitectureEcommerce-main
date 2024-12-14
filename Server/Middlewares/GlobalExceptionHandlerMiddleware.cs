using Application.Commons.Exceptions;
using Application.Models;
using System.Net;
using System.Text.Json;

namespace Server.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (NotFoundException ex)
            {
                await HandleNotFoundExceptionAsync(context, ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleUnauthorizedAccessAsync(context, ex);
            }
            catch (ArgumentNullException ex)
            {
                await HandleArgumentNullExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleGenericExceptionAsync(context, ex);
            }
        }

        private Task HandleArgumentNullExceptionAsync(HttpContext context, ArgumentNullException exception)
        {
            var response = new Response<string>(new[] { $"El argumento '{exception.ParamName}' no puede ser nulo." });

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private Task HandleUnauthorizedAccessAsync(HttpContext context, UnauthorizedAccessException exception)
        {
            var writingResponse = new Response<string>(new[] { "No está autorizado a acceder a este recurso." });

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(JsonSerializer.Serialize(writingResponse));
        }

        private Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException exception)
        {
            var writingResponse = new Response<string>(new[] { exception.Message });

            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(JsonSerializer.Serialize(writingResponse));
        }

        private Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            var errors = exception.Errors.SelectMany(e => e.Value).ToArray(); // Aplanar los errores
            var writingResponse = new Response<string>(errors);

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(JsonSerializer.Serialize(writingResponse));
        }

        private Task HandleGenericExceptionAsync(HttpContext context, Exception exception)
        {
            var writingResponse = new Response<string>(new[] { "Se produjo un error inesperado." });

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(JsonSerializer.Serialize(writingResponse));
        }
    }
}
