using Application.DTOs;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MillionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly PropertyService _propertyService;

        public PropertyController(PropertyService propertyService)
        {
            _propertyService = propertyService;
        }
        /// <summary>
        /// Obtiene todas las propiedades
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPropertys()
        {
            var propertys = await _propertyService.GetAllPropertysAsync();
            return Ok(propertys);
        }

        /// <summary>
        /// Obtiene una propiedad especifica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProperty(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null) return NotFound();
            return Ok(property);
        }

        /// <summary>
        /// Crea una propiedad
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] PropertyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var property = new Property
            {
                Name = dto.Name,
                Address = dto.Address,
                Price = dto.Price,
                CodeInternal = dto.CodeInternal,
                Year = dto.Year,
                OwnerIdOwner = dto.OwnerIdOwner
            };

            await _propertyService.AddPropertyAsync(property);
            return CreatedAtAction(nameof(GetProperty), new { id = property.IdProperty }, property);
        }

        /// <summary>
        /// Actualiza una propiedad
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(int id, [FromBody] PropertyDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id == 0) return BadRequest("debe ingresar un id");

            var property = new Property
            {
                IdProperty = id,
                Name = dto.Name,
                Address = dto.Address,
                Price = dto.Price,
                CodeInternal = dto.CodeInternal,
                Year = dto.Year,
                OwnerIdOwner = dto.OwnerIdOwner
            };

            await _propertyService.UpdatePropertyAsync(property);
            return NoContent();
        }

        /// <summary>
        /// Elimina una Propiedad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            await _propertyService.DeletePropertyAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Cambia el precio de una propiedad
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newprice"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("{id}/change-price")]
        public async Task<IActionResult> ChangePriceProperty([FromRoute] int id, [FromQuery] int newprice)
        {
            await _propertyService.ChangePricePropertyAsync(id, newprice);
            return NoContent();
        }

        /// <summary>
        /// Filtra las propiedades respecto a una combinacion de filtros a elecion del usuario
        /// </summary>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <param name="year"></param>
        /// <param name="CodeInternal"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("minPrice/maxPrice/year/CodeInternal")]
        public async Task<IActionResult> ChangePriceProperty([FromQuery]  decimal? minPrice, [FromQuery] decimal? maxPrice, [FromQuery] int? year, [FromQuery] string? CodeInternal)
        {
            var property = await _propertyService.GetFilteredPropertyAsync(minPrice, maxPrice, year, CodeInternal);           
           
           return Ok(property);
        }
    }
}
