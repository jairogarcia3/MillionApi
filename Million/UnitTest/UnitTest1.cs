using Moq;
using NUnit.Framework;
using Domain.Models;
using Domain.Interfaces;
using Application.Services;
using System;
using System.Threading.Tasks;

namespace UnitTest
{
    public class PropertyServiceTests
    {
        private Mock<IPropertyRepository> _repoMock;
        private PropertyService _service;

        [SetUp] 
        public void Setup()
        {
            _repoMock = new Mock<IPropertyRepository>();
            _service = new PropertyService(_repoMock.Object);
        }

        [Test]
        public async Task AddPropertyAsync()
        {
            
            var property = new Property { IdProperty = 1, Name = "casa 2 piso", Price = 100000, Address = "calle 27", Year = 2025 , CodeInternal = "C103", OwnerIdOwner = 1 };
            
            await _service.AddPropertyAsync(property); 
            _repoMock.Verify(r => r.AddAsync(property), Times.Once);
        }

        /// <summary>
        ///  este es un mensaje para la persona que esta revisando mi prueba tecnica, para este caso en especial estoy validando que el precio falle 
        ///  pero se pueden descomentar las validaciones inferiores para validar otos campos del objeto
        /// </summary>
        [Test]
        public void AddPropertyAsyncFail()  
        {
            var property = new Property { IdProperty = 1, Name = "casa 2 piso", Price = 0, Address = "calle 27", Year = 2025, CodeInternal = "C103", OwnerIdOwner = 1 };

            // Act + Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => _service.AddPropertyAsync(property));
            Assert.AreEqual("precio debe ser mayor a 0", ex!.Message);
            //Assert.AreEqual("debe agregar una direccion", ex!.Message);
            //Assert.AreEqual("debe ingresar un codigo", ex!.Message);
            //Assert.AreEqual("debe ingresar un año valido", ex!.Message);
        }

        [Test]
        public async Task GetAllPropertiesAsync()       
        {
           
            var properties = new List<Property>
            {
                new Property { IdProperty = 1, Name = "Casa blanca", Price = 120000 },
                new Property { IdProperty = 2, Name = "casa de nariño", Price = 500000 },
                new Property { IdProperty = 3, Name = "casa de jairo", Price = 90000 },
                new Property { IdProperty = 4, Name = "casa del revisor", Price = 170000 }
            };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(properties);

            var result = await _service.GetAllPropertiesAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(4, ((List<Property>)result).Count);
            Assert.AreEqual("Casa blanca", ((List<Property>)result)[0].Name);

            _repoMock.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Test]
        public async Task GetPropertyByIdAsync_WhenPropertyExists_ReturnsProperty()
        {
           
            var property = new Property { IdProperty = 1, Name = "Casa muy lejana", Price = 120000 };
            
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(property);

            var result = await _service.GetPropertyByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Casa muy lejana", result.Name);
            Assert.AreEqual(120000, result.Price);
           
            _repoMock.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [Test]
        public async Task UpdatePropertyAsync()
        {
            var property = new Property { IdProperty = 1, Name = "casa 2 piso", Price = 100000, Address = "calle 27", Year = 2025, CodeInternal = "C103", OwnerIdOwner = 1 };
         
            await _service.UpdatePropertyAsync(property);
            _repoMock.Verify(r => r.UpdateAsync(property), Times.Once);
        }
    }
}