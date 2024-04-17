using ExceleTech.Application.Commands.ProductCommands.UpdateProductStatisticsCommand;
using ExceleTech.Application.Responses;
using ExceleTech.Domain.Interfaces.Repositories;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceleTech.Infrastructure.BackgroundJobs.UpdateProductStatistics
{
    internal class UpdateProductStatisticsJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public UpdateProductStatisticsJob(
            IUnitOfWork unitOfWork,
            IProductRepository productRepository,
            IOrderRepository orderRepository)
            
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }



        public async Task Execute(IJobExecutionContext context)
        {
              Dictionary<Guid, int> data = await _orderRepository.GetStatisticAboutOrderedProductsAsync();
              await _productRepository.UpdateProductStatisticsAsync(data);
              await _unitOfWork.SaveChangesAsync();
            
           


        }


    }

}
