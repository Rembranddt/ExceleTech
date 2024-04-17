using MediatR;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.OrderResponses;

namespace ExceleTech.Application.Queries.OrderQueries
{
    public class GetUserOrdersQuery : IRequest<BaseResponse<List<OrderResponse>>>
    {
        public GetUserOrdersQuery(Guid UserId)
        {
            this.UserId = UserId;
        }

        public Guid UserId { get; init; }
    }
}
