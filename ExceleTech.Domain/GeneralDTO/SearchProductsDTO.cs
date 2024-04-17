using ExceleTech.Domain.Enums;

namespace ExceleTech.Application.DTO
{
    public class SearchProductsDTO
    {
        public string SearchString { get; init; }

        public SortCriteria? Criteria { get; init; }

        public bool IsAscending { get; init; }  

        public ProductCategory? Category { get; init; }

    }
}
