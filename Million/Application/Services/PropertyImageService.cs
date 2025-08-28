using Domain.Models;
using Domain.Interfaces;

namespace Application.Services
{
    public class PropertyImageService
    {
        private readonly IPropertyImageRepository _ipropertyImageRepository;
        private readonly IPropertyRepository _ipropertyRepository;
        public PropertyImageService(IPropertyImageRepository ipropertyImageRepository, IPropertyRepository ipropertyRepository)
        {
            _ipropertyRepository = ipropertyRepository;
            _ipropertyImageRepository = ipropertyImageRepository;
        }

        public async Task<IEnumerable<PropertyImage>> GetAllPropertyImagesAsync()
        {
            return await _ipropertyImageRepository.GetAllAsync();
        }

        public async Task<PropertyImage?> GetPropertyImageByIdAsync(int id)
        {
            return await _ipropertyImageRepository.GetByIdAsync(id);
        }

        public async Task AddPropertyImageAsync(PropertyImage propertyImage)
        {        
            var property = await _ipropertyRepository.GetByIdAsync(propertyImage.PropertyIdProperty);
            if (property == null)
                throw new KeyNotFoundException("la propiedad no existe.");

            await _ipropertyImageRepository.AddAsync(propertyImage);
        }

        public async Task DeletePropertyImageAsync(int id)
        {
            var existing = await _ipropertyImageRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("imagen no encontrada.");

            await _ipropertyImageRepository.DeleteAsync(id);
        }

    }
}
