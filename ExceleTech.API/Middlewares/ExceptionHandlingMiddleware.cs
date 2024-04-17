using ExceleTech.Application.Responses;
using System.Net;
using ILogger = Serilog.ILogger;
namespace ExceleTech.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger logger)
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
                await HandleExceptionAsync(context,ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.Error(ex,ex.Message);

            var response = ex switch
            {
                Exception _ => new BaseResponse()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                }
            }; 
            response.AddError(ex.Message);
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
