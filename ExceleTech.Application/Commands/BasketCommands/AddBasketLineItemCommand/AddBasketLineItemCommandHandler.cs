using AutoMapper;
using ExceleTech.Application.Resources;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.BasketResponses;
using ExceleTech.Domain.Entities;
using ExceleTech.Domain.Interfaces.Repositories;
using MediatR;
using System.Net;

namespace ExceleTech.Application.Commands.BasketCommands.AddBasketLineItemCommand
{
    internal class AddBasketLineItemCommandHandler : IRequestHandler<AddBasketLineItemCommand, BaseResponse<AddBasketLineItemResponse>>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddBasketLineItemCommandHandler(
            IBasketRepository basketRepository,
            IUnitOfWork unitOfWork,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<AddBasketLineItemResponse>> Handle(AddBasketLineItemCommand request, CancellationToken cancellationToken)
        {
            Basket basket = await _basketRepository.GetBasketByUserIdAsync(request.UserId);
            BaseResponse<AddBasketLineItemResponse> response = new BaseResponse<AddBasketLineItemResponse>();
            if (basket == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.AddError(ErrorMessage.BasketNotFound);

                return response;
            }

            Product product = await _productRepository.GetProductByIdAsync(request.AddLineItemDTO.ProductId);

            if (product == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.AddError(ErrorMessage.ProductNotFound);
                return response;
            }

            BasketLineItem lineItem = basket.AddLineItem(request.AddLineItemDTO.Quantity, product);


            await _unitOfWork.SaveChangesAsync();
            response.AddData(_mapper.Map<AddBasketLineItemResponse>(lineItem));

            return response;
        }
    }
}
