using ExceleTech.Application.Responses;
using MediatR;
using ExceleTech.Application.Responses.ProductResponses;
using ExceleTech.Domain.Interfaces.Repositories;
using ExceleTech.Domain.Entities;
using System.Net;
using ExceleTech.Application.Resources;
using AutoMapper;
using ExceleTech.Domain.Common;
namespace ExceleTech.Application.Commands.ProductCommands
{
    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, BaseResponse<CreateProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(
            IProductRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper )
        {
            _productRepository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetProductBySkuValueAsync(request.createProductDTO.Sku);
            BaseResponse<CreateProductResponse> response = new BaseResponse<CreateProductResponse>();
              
            if (product != null)
            {
                response.StatusCode = HttpStatusCode.Conflict;
                response.AddError(ErrorMessage.SkuIsAlreadyExists);
                return response;
            }

            product = await _productRepository.GetProductByNameAsync(request.createProductDTO.ProductName);

            if (product != null)
            {
                response.StatusCode = HttpStatusCode.Conflict;
                response.AddError(ErrorMessage.ProductAlreadyExists);
                return response;
            }

            product = Product.Create(request.createProductDTO.ProductName,
                                    request.createProductDTO.Category,
                                    Sku.Create(request.createProductDTO.Sku),
                                    request.createProductDTO.Money);
            
            _productRepository.Add(product);
            await _unitOfWork.SaveChangesAsync();
            response.AddData(_mapper.Map<CreateProductResponse>(product));

            return response;
        }
    }
}
