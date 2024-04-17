using ExceleTech.Domain.Common;
using ExceleTech.Domain.Enums;

namespace ExceleTech.Application.DTO;

public class CreateProductDTO
{
    public string ProductName { get; set; }
    public ProductCategory Category { get; set; }
    public string Sku { get; set; }
    public Money Money { get; set; }
}

