using Domain.Models;
using Domain.Interfaces;

namespace Application.Services
{
    public class PropertyService
    {
        private readonly IPropertyRepository _ipropertyRepository;
        private readonly IOwnerRepository _ownerRepository;

        public PropertyService(IPropertyRepository ipropertyRepository, IOwnerRepository ownerRepository)
        {
            _ipropertyRepository = ipropertyRepository;
            _ownerRepository = ownerRepository;
        }

        public async Task<IEnumerable<Property>> GetAllPropertysAsync()
        {
            return await _ipropertyRepository.GetAllAsync();
        }

        public async Task<Property?> GetPropertyByIdAsync(int id)
        {
            return await _ipropertyRepository.GetByIdAsync(id);
        }

        public async Task AddPropertyAsync(Property property)
        {
            var existing = await _ownerRepository.GetByIdAsync(property.OwnerIdOwner);
            if (existing == null)
                throw new KeyNotFoundException("el propietario no existe.");

            var codeinternal = await _ipropertyRepository.GetByCodeInternalAsync(property.CodeInternal);
            if (codeinternal != null)
                throw new KeyNotFoundException("el codigo interno ya existe.");

            await _ipropertyRepository.AddAsync(property);
        }

        public async Task UpdatePropertyAsync(Property property)
        {
            int id = property.IdProperty;
            var existing = await _ipropertyRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Propiedad no encontrado.");

            existing.Name = property.Name;
            existing.Address = property.Address;
            existing.Price = property.Price;
            existing.CodeInternal = property.CodeInternal;
            existing.Year = property.Year;
            await _ipropertyRepository.UpdateAsync(existing);
        }

        public async Task DeletePropertyAsync(int id)
        {
            var existing = await _ipropertyRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("propiedad no encontrada.");

            await _ipropertyRepository.DeleteAsync(id);
        }

        public async Task ChangePricePropertyAsync(int id, decimal newPrice)
        {
            
            var existing = await _ipropertyRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("propiedad no encontrada.");

            if (newPrice <= 0)
                throw new KeyNotFoundException("ingrese un precio valido.");

            await _ipropertyRepository.ChangePriceAsync(id, newPrice);
        }

        public async Task<IEnumerable<Property>> GetFilteredPropertyAsync(decimal? minPrice, decimal? maxPrice, int? year, string? CodeInternal)
        {
           return await _ipropertyRepository.GetFilteredAsync(minPrice,maxPrice,year,CodeInternal);
        }

    }
}
