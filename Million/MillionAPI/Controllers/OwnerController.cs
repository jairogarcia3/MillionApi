using Application.DTOs;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MillionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly OwnerService _ownerService;

        public OwnerController(OwnerService ownerService)
        {
            _ownerService = ownerService;
        }
        /// <summary>
        /// Obtiene todos los Propietarios
        /// </summary>
        /// <returns></returns>
        // GET: api/owner
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetOwners()
        {
            var owners = await _ownerService.GetAllOwnersAsync();
            return Ok(owners);
        }

        /// <summary>
        /// Obtiene un propietario especifico
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/owner/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOwner(int id)
        {
            var owner = await _ownerService.GetOwnerByIdAsync(id);
            if (owner == null) return NotFound();
            return Ok(owner);
        }

        /// <summary>
        /// Registra un nuevo propietario
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        // POST: api/owner
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOwner([FromBody] OwnerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var owner = new Owner
            {
                Name = dto.Name,
                Address = dto.Address,
                Birthday = dto.Birthday,
                Photo = dto.Photo
            };

            await _ownerService.AddOwnerAsync(owner);
            return CreatedAtAction(nameof(GetOwner), new { id = owner.IdOwner }, owner);
        }

        /// <summary>
        /// Actualiza un propietario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        // PUT: api/owner/{id}
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOwner(int id, [FromBody] OwnerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id == 0) return BadRequest("debe ingresar un id");

            var owner = new Owner
            {
                IdOwner = id,
                Name = dto.Name,
                Address = dto.Address,
                Birthday = dto.Birthday,
                Photo = dto.Photo
            };           

            await _ownerService.UpdateOwnerAsync(owner);
            return NoContent();
        }

        /// <summary>
        /// Elimina un propietario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/owner/{id}
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            await _ownerService.DeleteOwnerAsync(id);
            return NoContent();
        }
    }
}
