using ExceleTech.Application.DTO;
using ExceleTech.Application.Responses;
using ExceleTech.Application.Responses.ProductResponses;
using MediatR;
namespace ExceleTech.Application.Commands.ProductCommands
{
    public class CreateProductCommand : IRequest<BaseResponse<CreateProductResponse>>
    {
        public CreateProductCommand(CreateProductDTO createProductDTO) 
        { 
            this.createProductDTO = createProductDTO;
        }
        
        public CreateProductDTO createProductDTO {  get;  init; }
    }
}
