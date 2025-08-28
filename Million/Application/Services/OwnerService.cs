using Domain.Models;
using Domain.Interfaces;

namespace Application.Services
{
    public class OwnerService
    {
        private readonly IOwnerRepository _ownerRepository;


        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            return await _ownerRepository.GetAllAsync();
        }

        public async Task<Owner?> GetOwnerByIdAsync(int id)
        {
            return await _ownerRepository.GetByIdAsync(id);
        }

        public async Task AddOwnerAsync(Owner owner)
        {
            if (string.IsNullOrWhiteSpace(owner.Name))
                throw new ArgumentException("El nombre del propietario es obligatorio.");

            await _ownerRepository.AddAsync(owner);
        }

        public async Task UpdateOwnerAsync(Owner owner)
        {
            int id = owner.IdOwner;
            var existing = await _ownerRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Propietario no encontrado.");

            existing.Name = owner.Name;
            existing.Address = owner.Address;
            existing.Birthday = owner.Birthday;
            existing.Photo = owner.Photo;
            await _ownerRepository.UpdateAsync(existing);
        }

        public async Task DeleteOwnerAsync(int id)
        {
            var existing = await _ownerRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Propietario no encontrado.");

            await _ownerRepository.DeleteAsync(id);
        }
    }
}
