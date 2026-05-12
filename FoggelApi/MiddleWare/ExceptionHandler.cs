using System.Net.Mime;
using System.Text.Json;

namespace FoggelApi.MiddleWare
{
    public class ExceptionHandler(RequestDelegate @next, ILogger<ExceptionHandler> logger)
    {

        private readonly RequestDelegate _next = @next;
        private readonly ILogger<ExceptionHandler> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred while processing the request. " +
                    "TraceId: {TraceId}. Path: {Path}. Method: {Method}.",
                    context.TraceIdentifier,
                    context.Request.Path,
                    context.Request.Method);                

                context.Response.ContentType = MediaTypeNames.Application.Json;
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var json = new
                {
                    Message = "An unexpected error occurred. Please try again later.",
                    TraceId = context.TraceIdentifier,
                };

                var jsonResponse = JsonSerializer.Serialize(json);

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
