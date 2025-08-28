using Application.DTOs;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MillionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyTracesController : ControllerBase
    {
        private readonly PropertyTracesService _propertyTracesService;

        public PropertyTracesController(PropertyTracesService propertyTracesService)
        {
            _propertyTracesService = propertyTracesService;
        }

        /// <summary>
        /// Obtiene todos los "rastros"
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPropertysTraces()
        {
            var propertysTraces = await _propertyTracesService.GetAllPropertyTracesAsync();
            return Ok(propertysTraces);
        }

        /// <summary>
        /// obtiene un "rastro" especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyTraces(int id)
        {
            var propertysTrace = await _propertyTracesService.GetPropertyTracesByIdAsync(id);
            if (propertysTrace == null) return NotFound();
            return Ok(propertysTrace);
        }

        /// <summary>
        /// Crea un nuevo "rastro"
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePropertyTraces([FromBody] PropertyTraceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var propertyTrace = new PropertyTrace
            {
                PropertyIdProperty = dto.PropertyIdProperty,
                DateSale = dto.DateSale,
                Name = dto.Name,
                Value = dto.Value,
                Tax = dto.Tax
            };

            await _propertyTracesService.AddPropertyTracesAsync(propertyTrace);
            return CreatedAtAction(nameof(GetPropertyTraces), new { id = propertyTrace.IdPropertyTrace }, propertyTrace);
        }

        /// <summary>
        /// Elimina un "Rastro"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyTraces(int id)
        {
            await _propertyTracesService.DeletePropertyTracesAsync(id);
            return NoContent();
        }

    }
}
