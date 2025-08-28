using Domain.Models;

namespace Domain.Interfaces
{
    public interface IPropertyImageRepository
    {
        Task<IEnumerable<PropertyImage>> GetAllAsync();
        Task<PropertyImage?> GetByIdAsync(int id);       
        Task AddAsync(PropertyImage property);
        Task UpdateAsync(PropertyImage owner);
        Task DeleteAsync(int id);
        
    }
}
