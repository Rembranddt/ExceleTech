using ExceleTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceleTech.Domain.Interfaces.Services
{
    public interface ICacheService<T> where T : class
    {
        Task<T> GetDataAsync(string key);
        Task SetDataAsync(string key,T value, TimeSpan ExpirationTime);
        Task RemoveDataAsync(string key);

    }
}
