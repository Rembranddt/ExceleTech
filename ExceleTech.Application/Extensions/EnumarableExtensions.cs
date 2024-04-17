using ExceleTech.Application.DTO;
using ExceleTech.Domain.Enums;

namespace ExceleTech.Application.Extensions
{
    public static class EnumarableExtensions
    {
        public static IEnumerable<ProductDTO> ApplySorting(this IEnumerable<ProductDTO> enumerable, SortCriteria? sortCriteria, bool IsAscending)
        {
            if (sortCriteria == null)
            {
                return enumerable;

            }

            if (sortCriteria == SortCriteria.Price)
            {
                if (IsAscending == true)
                    enumerable = enumerable.OrderBy(p => p.Price.Amount);
                else
                    enumerable = enumerable.OrderByDescending(p => p.Price.Amount);
                return enumerable;

            }

            if (sortCriteria == SortCriteria.DiscountPersent)
            {
                if (IsAscending == true)
                    enumerable = enumerable.OrderBy(p => p.PercentDiscount);
                else
                    enumerable = enumerable.OrderByDescending(p => p.PercentDiscount);
                return enumerable;
            }

            return enumerable;
        }
        public static IEnumerable<T> UsePagination<T>(this IEnumerable<T> query, int page, int pageSize)
       where T : class
        {
            if (page >= 0 && pageSize >= 0)
            {
                return query.Skip((page - 1) * pageSize).Take(pageSize);
            }

            return query;
        }
    }
}
