using realstate.DAL.Models;

namespace realstate.DAL.Repositories.Interfaces
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        Task<IEnumerable<Property>> GetPropertiesByLocationAsync(string location);
        Task<IEnumerable<Property>> GetPropertiesByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<Property>> GetAvailablePropertiesAsync();
        Task<IEnumerable<Property>> SearchPropertiesAsync(string searchTerm);
        Task IncrementViewCountAsync(int propertyId);
    }
}
