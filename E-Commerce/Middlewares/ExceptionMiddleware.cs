using System.Net;
using System.Text.Json;

namespace E_Commerce.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = context.Response;

            // 🔥 Default
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = ex.Message;

            // 🔥 Custom handling (optional but powerful)
            if (ex is KeyNotFoundException)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if (ex is UnauthorizedAccessException)
            {
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (ex is ArgumentException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            var result = new
            {
                Success = false,
                StatusCode = response.StatusCode,
                Message = message
            };

            var json = JsonSerializer.Serialize(result);

            await context.Response.WriteAsync(json);
        }
    }
}