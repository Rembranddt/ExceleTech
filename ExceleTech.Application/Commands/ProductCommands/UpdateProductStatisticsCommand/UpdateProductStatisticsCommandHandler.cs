using ExceleTech.Application.Responses;
using ExceleTech.Domain.Interfaces.Repositories;
using MediatR;

namespace ExceleTech.Application.Commands.ProductCommands.UpdateProductStatisticsCommand
{
    internal class UpdateProductStatisticsCommandHandler : IRequestHandler<UpdateProductStatisticsCommand, BaseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public UpdateProductStatisticsCommandHandler(
            IUnitOfWork unitOfWork, 
            IProductRepository productRepository, 
            IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<BaseResponse> Handle(UpdateProductStatisticsCommand request, CancellationToken cancellationToken)
        {
            Dictionary<Guid, int> data = await _orderRepository.GetStatisticAboutOrderedProductsAsync();
            await _productRepository.UpdateProductStatisticsAsync(data);
            await _unitOfWork.SaveChangesAsync();
            
            return new BaseResponse();
        }
    }
}
