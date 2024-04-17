using ExceleTech.Application.Responses;
using MediatR;
using Serilog.Context;
using ILogger = Serilog.ILogger;


namespace ExceleTech.Application.Behaviors
{
    internal class RequestLoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> 
        where TResponse : BaseResponse
        {
        private readonly ILogger _logger;

        public RequestLoggingBehavior(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string RequestName = typeof(TRequest).Name;
            _logger.Information($"Processing request {RequestName}");
            TResponse response = await next();

            if(response.IsSuccess)
            {
                _logger.Information($"Request {RequestName} is complited");
            }

            else
            {
                using (LogContext.PushProperty("Errors", response.Errors, false))
                {
                    _logger.Error($"Request {RequestName} is complited with errors {string.Join("\n", response.Errors)}");
                }
            }
            
            return response;
        }
    }
}
