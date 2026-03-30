

namespace salespoints.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // 🔹 Validación de token en blacklist
                var token = context.Request.Headers["Authorization"]
                    .ToString()
                    .Replace("Bearer ", "");

                if (!string.IsNullOrEmpty(token))
                {
                    var response = RepositoryReturn.Error<string>(
                        "Token inválido o ha sido revocado",
                        401
                    );

                    context.Response.StatusCode = response.StatusCode;
                    context.Response.ContentType = "application/json";

                    var json = System.Text.Json.JsonSerializer.Serialize(response);

                    await context.Response.WriteAsync(json);
                    return;
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            BaseResponse<object> response;

            switch (exception)
            {
                case UnauthorizedAccessException:
                    response = RepositoryReturn.Error<object>(
                        "No autorizado",
                        401
                    );
                    break;

                case ArgumentException:
                    response = RepositoryReturn.Error<object>(
                        exception.Message,
                        400
                    );
                    break;

                default:
                    response = RepositoryReturn.ErrorException<object>(
                        exception,
                        "Error interno del servidor"
                    );
                    break;
            }

            context.Response.StatusCode = response.StatusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}