using MediatR;
using ILogger = Serilog.ILogger;

namespace ExceleTech.Application.Behaviors
{
    internal class RequestTestPipilineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        {
        private readonly ILogger _logger;

        public RequestTestPipilineBehavior(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string RequestName = typeof(TRequest).Name;
            _logger.Information($"Test {RequestName}");
            TResponse response = await next();

            return response;
        }
    }
}
