using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _context;

        public PropertyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Property>> GetAllAsync()
        {
            return await _context.Properties.ToListAsync();
        }

        public async Task<Property?> GetByIdAsync(int id)
        {
            return await _context.Properties
                .FirstOrDefaultAsync(o => o.IdProperty == id);
        }

        public async Task<Property?> GetByCodeInternalAsync(string code)
        {
            return await _context.Properties
                .FirstOrDefaultAsync(o => o.CodeInternal == code);
        }

        public async Task AddAsync(Property property)
        {
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property != null)
            {
                _context.Properties.Remove(property);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ChangePriceAsync(int id, decimal newPrice)
        {
            var property = await _context.Properties
                .FirstOrDefaultAsync(o => o.IdProperty == id);

            property.Price = newPrice;

            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Property>> GetFilteredAsync(decimal? minPrice, decimal? maxPrice, int? year, string? CodeInternal)
        {
            var query = _context.Properties.AsQueryable();

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (year.HasValue)
                query = query.Where(p => p.Year == year.Value);

            if (!string.IsNullOrEmpty(CodeInternal))
                query = query.Where(p => p.CodeInternal == CodeInternal);

            var x = await query
                .AsNoTracking()
                .ToListAsync();

            return x;
        }

    }
}
