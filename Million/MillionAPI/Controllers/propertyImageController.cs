using Application.DTOs;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MillionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class propertyImageController : ControllerBase
    {
        private readonly PropertyImageService _propertyImageService;

        public propertyImageController(PropertyImageService propertyImageService)
        {
            _propertyImageService = propertyImageService;
        }

        /// <summary>
        /// Obtiene todas las imagenes de las propiedades
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPropertysImage()
        {
            var propertysImage = await _propertyImageService.GetAllPropertyImagesAsync();
            return Ok(propertysImage);
        }

        /// <summary>
        /// Obtiene una imagen en especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyImage(int id)
        {
            var propertysImage = await _propertyImageService.GetPropertyImageByIdAsync(id);
            if (propertysImage == null) return NotFound();
            return Ok(propertysImage);
        }

        /// <summary>
        /// Permite almacenar una nueva imagen
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePropertyImage([FromBody] PropertyImageDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var propertyImage = new PropertyImage
            {
                PropertyIdProperty = dto.PropertyIdProperty,
                File = dto.File,
                Enabled = dto.Enabled
            };

            await _propertyImageService.AddPropertyImageAsync(propertyImage);
            return CreatedAtAction(nameof(GetPropertyImage), new { id = propertyImage.IdPropertyImage }, propertyImage);
        }


        /// <summary>
        /// Elimina una imagen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyImage(int id)
        {
            await _propertyImageService.DeletePropertyImageAsync(id);
            return NoContent();
        }
    }
}
