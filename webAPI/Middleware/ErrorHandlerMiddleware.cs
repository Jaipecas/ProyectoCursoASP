using App.ErrorHandler;
using System.Net;
using System.Text.Json;

namespace WebAPI.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate requestDelegate, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await ExceptionHandlerAsync(httpContext, ex, _logger);
            }
        }

        public async Task ExceptionHandlerAsync(HttpContext httpContext, Exception exception, ILogger<ErrorHandlerMiddleware> logger)
        {
            object? errores = null;

            switch (exception)
            {
                case NotFoundException handler:
                    logger.LogError(exception, "Manejador error");
                    errores = handler.Errores;
                    httpContext.Response.StatusCode = (int)NotFoundException.StatusCode;
                    break;
                case UnauthorizedException handler:
                    logger.LogError(exception, "Manejador error");
                    errores = handler.Errores;
                    httpContext.Response.StatusCode = (int)UnauthorizedException.StatusCode;
                    break;
                case ExistException handler:
                    logger.LogError(exception, "Manejador error");
                    errores = handler.Errores;
                    httpContext.Response.StatusCode = (int)ExistException.StatusCode;
                    break;
                case Exception ex:
                    logger.LogError(ex, "Error Servidor");
                    errores = string.IsNullOrWhiteSpace(ex.Message) ? "Error" : ex.Message;
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            httpContext.Response.ContentType = "application/json";

            if (errores != null)
            {
                string result = JsonSerializer.Serialize(new { errores });
                await httpContext.Response.WriteAsync(result);
            }
        }
    }
}
