using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly AppDbContext _context;
        public PropertyImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PropertyImage>> GetAllAsync()
        {
            return await _context.PropertyImages.ToListAsync();
        }

        public async Task<PropertyImage?> GetByIdAsync(int id)
        {
            return await _context.PropertyImages
                .FirstOrDefaultAsync(o => o.IdPropertyImage == id);
        }

        public async Task AddAsync(PropertyImage propertyImage)
        {
             _context.PropertyImages.Add(propertyImage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PropertyImage propertyImage)
        {
            _context.PropertyImages.Update(propertyImage);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var propertyImage = await _context.PropertyImages.FindAsync(id);
            if (propertyImage != null)
            {
                _context.PropertyImages.Remove(propertyImage);
                await _context.SaveChangesAsync();
            }
        }

    }
}
