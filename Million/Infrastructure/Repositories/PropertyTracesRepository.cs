using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PropertyTracesRepository : IPropertyTracesRepository
    {
        private readonly AppDbContext _context;
        public PropertyTracesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PropertyTrace>> GetAllAsync()
        {
            return await _context.PropertyTraces.ToListAsync();
        }

        public async Task<PropertyTrace?> GetByIdAsync(int id)
        {
            return await _context.PropertyTraces
                .FirstOrDefaultAsync(o => o.IdPropertyTrace == id);
        }

        public async Task AddAsync(PropertyTrace propertyTrace)
        {
            _context.PropertyTraces.Add(propertyTrace);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PropertyTrace propertyTrace)
        {
            _context.PropertyTraces.Update(propertyTrace);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var propertyTrace = await _context.PropertyTraces.FindAsync(id);
            if (propertyTrace != null)
            {
                _context.PropertyTraces.Remove(propertyTrace);
                await _context.SaveChangesAsync();
            }
        }
    }
}
