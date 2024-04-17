using ExceleTech.Application.Responses.BasketResponses;
using ExceleTech.Application.Responses;
using MediatR;
using ExceleTech.Domain.Interfaces.Repositories;
using ExceleTech.Domain.Entities;
using System.Net;
using AutoMapper;
using ExceleTech.Application.Resources;

namespace ExceleTech.Application.Commands.BasketCommands.DeleteBasketLineItemCommand
{
    public class DeleteBasketLineItemCommandHandler : IRequestHandler<DeleteBasketLineItemCommand, BaseResponse<DeleteBasketLineItemResponse>>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBasketLineItemCommandHandler(
            IBasketRepository basketRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<DeleteBasketLineItemResponse>> Handle(DeleteBasketLineItemCommand request, CancellationToken cancellationToken)
        {
            Basket basket = await _basketRepository.GetBasketByUserIdAsync(request.UserId);
            BaseResponse<DeleteBasketLineItemResponse> response = new BaseResponse<DeleteBasketLineItemResponse>();

            if (basket == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.AddError(ErrorMessage.UserNotFound);
                return response;    
            }

            BasketLineItem item = basket.DeleteLineItem(request.LineItemId);

            if (item == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.AddError(ErrorMessage.LiteItemNotFound);
                return response;
            }

            response.AddData(_mapper.Map<DeleteBasketLineItemResponse>(item));

            return response;
        }
    }
}
