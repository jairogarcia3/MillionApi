using NUnit.Framework;
using Moq;
using Application.Services;
using Domain.Models;
using Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace UnitTest
{
    public class PropertyService
    {
        private readonly IPropertyRepository _repository;

        public PropertyService(IPropertyRepository repository)
        {
            _repository = repository;
        }

        public async Task AddPropertyAsync(Property property)
        {
            if (property.Price <= 0)
                throw new ArgumentException("precio debe ser mayor a 0");

            if (string.IsNullOrEmpty(property.Address))
                throw new ArgumentException("debe agregar una direccion");

            if (string.IsNullOrEmpty(property.CodeInternal))
                throw new ArgumentException("debe ingresar un codigo");

            if (property.Year <= 0)
                throw new ArgumentException("debe ingresar un año valido");

            await _repository.AddAsync(property);
        }

        public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Property?> GetPropertyByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdatePropertyAsync(Property property)
        {
            if (property.Price <= 0)
                throw new ArgumentException("precio debe ser mayor a 0");

            if (string.IsNullOrEmpty(property.Address))
                throw new ArgumentException("debe agregar una direccion");

            if (string.IsNullOrEmpty(property.CodeInternal))
                throw new ArgumentException("debe ingresar un codigo");

            if (property.Year <= 0)
                throw new ArgumentException("debe ingresar un año valido");


            await _repository.UpdateAsync(property);
        }
    }
}
