using Microsoft.EntityFrameworkCore;
using realstate.DAL.Models;
using realstate.DAL.Repositories.Interfaces;

namespace realstate.DAL.Repositories.Implementations
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Property>> GetPropertiesByLocationAsync(string location)
        {
            return await _context.Properties
                .AsNoTracking()
                .Where(p => p.City.Contains(location) || p.State.Contains(location))
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetPropertiesByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _context.Properties
                .AsNoTracking()
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> GetAvailablePropertiesAsync()
        {
            return await _context.Properties
                .AsNoTracking()
                .Where(p => p.IsActive && p.Status == "Available")
                .ToListAsync();
        }

        public async Task<IEnumerable<Property>> SearchPropertiesAsync(string searchTerm)
        {
            return await _context.Properties
                .AsNoTracking()
                .Where(p => p.Title.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task IncrementViewCountAsync(int propertyId)
        {
            var property = await _context.Properties.FindAsync(propertyId);
            if (property != null)
            {
                property.ViewCount++;
                _context.Properties.Update(property);
                await _context.SaveChangesAsync();
            }
        }
    }
}

