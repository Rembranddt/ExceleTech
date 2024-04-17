using MediatR;
using System.Net;
using ExceleTech.Application.Responses;

namespace ExceleTech.Application.Behaviors
{
    internal class ExceptionHandlingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TResponse : BaseResponse
        {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }

            catch (Exception ex)
            {
                if (typeof(TResponse).IsGenericType &&
               typeof(TResponse).GetGenericTypeDefinition() == typeof(BaseResponse<>))
                {
                    Type resultType = typeof(TResponse).GetGenericArguments()[0];

                    Type ElementType = typeof(BaseResponse<>).MakeGenericType(resultType);


                    TResponse response = (TResponse)Activator.CreateInstance(ElementType);
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.AddError(ex.Message + "" + ex.StackTrace);
                    return response;
                }

                else
                {
                    TResponse response = (TResponse)Activator.CreateInstance(typeof(BaseResponse));
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    return response;

                }
            }
        }
    }
}
