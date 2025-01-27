using System.Net;

namespace TestTaskWebApi.Midleware
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
               
                _logger.LogError(ex, "An error occurred.");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new
                {
                    success = false,
                    message = "An unexpected error occurred.",
                    details = ex.Message
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
