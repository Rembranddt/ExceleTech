using ExceleTech.Domain.Entities;
using ExceleTech.Domain.Enums;

namespace ExceleTech.Application.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<Product> ApplySorting(this IQueryable<Product> query, SortCriteria? sortCriteria, bool IsAscending)
        {
            if (sortCriteria == null)
            {
                return query;
            }

            if (sortCriteria == SortCriteria.Price)
            {
                if (IsAscending == true)
                    query = query.OrderBy(p => p.Price.Amount);
                else
                    query = query.OrderByDescending(p => p.Price.Amount);

                return query;
            }

            if (sortCriteria == SortCriteria.DiscountPersent)
            {
                if (IsAscending == true)
                    query = query.OrderBy(p => p.PercentDiscount);
                else
                    query = query.OrderByDescending(p => p.PercentDiscount);

                return query;
            }

            return query;
        }

        public static IQueryable<T> UsePagination<T>(this IQueryable<T> query, int page, int pageSize)
        where T : class
        {

            if(page >= 0 && pageSize >= 0)
            {
                return query.Skip((page-1) * pageSize).Take(pageSize);
            }
            
            return query;   
        }
    }
}
