using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PropertyTracesService
    {
        private readonly IPropertyTracesRepository _ipropertyTracesRepository;
        private readonly IPropertyRepository _ipropertyRepository;

        public PropertyTracesService(IPropertyTracesRepository ipropertyTracesRepository, IPropertyRepository ipropertyRepository)
        {
            _ipropertyRepository = ipropertyRepository;
            _ipropertyTracesRepository = ipropertyTracesRepository;
        }

        public async Task<IEnumerable<PropertyTrace>> GetAllPropertyTracesAsync()
        {
            return await _ipropertyTracesRepository.GetAllAsync();
        }

        public async Task<PropertyTrace?> GetPropertyTracesByIdAsync(int id)
        {
            return await _ipropertyTracesRepository.GetByIdAsync(id);
        }

        public async Task AddPropertyTracesAsync(PropertyTrace propertyImage)
        {
            var property = await _ipropertyRepository.GetByIdAsync(propertyImage.PropertyIdProperty);
            if (property == null)
                throw new KeyNotFoundException("la propiedad no existe.");

            await _ipropertyTracesRepository.AddAsync(propertyImage);
        }

        public async Task DeletePropertyTracesAsync(int id)
        {
            var existing = await _ipropertyTracesRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("traza no encontrada.");

            await _ipropertyTracesRepository.DeleteAsync(id);
        }
    }
}
