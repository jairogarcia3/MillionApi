using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Property>> GetAllAsync();
        Task<Property?> GetByIdAsync(int id);       
        Task<Property?> GetByCodeInternalAsync(string id);       
        Task AddAsync(Property property);
        Task UpdateAsync(Property property);
        Task DeleteAsync(int id);
        Task ChangePriceAsync(int id, decimal newPrice);
        Task<IEnumerable<Property>> GetFilteredAsync(decimal? minPrice, decimal? maxPrice, int? year, string? CodeInternal);
    }
}
