using System.Net;
using System.Text.Json;

namespace Rekrutacja.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");

                var response = context.Response;
                response.ContentType = "application/json";

                var statusCode = ex switch
                {
                    ArgumentNullException => HttpStatusCode.BadRequest,
                    InvalidOperationException => HttpStatusCode.BadRequest,
                    UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                    KeyNotFoundException => HttpStatusCode.NotFound,
                    _ => HttpStatusCode.InternalServerError
                };

                response.StatusCode = (int)statusCode;

                var errorResponse = new
                {
                    status = (int)statusCode,
                    error = statusCode.ToString(),
                    message = ex.Message,
                    path = context.Request.Path
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await response.WriteAsync(json);
            }
        }
    }
}
