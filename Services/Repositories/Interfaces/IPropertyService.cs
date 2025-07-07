using realstate.Services.DTOs;

namespace realstate.Services.Repositories.Interfaces
{
    public interface IPropertyService
    {
        Task<PropertyDto> CreateAsync(CreatePropertyDto dto);
        Task<IEnumerable<PropertyDto>> GetAllAsync(int page, int pageSize);
        Task<PropertyDto?> GetByIdAsync(int id);
        Task UpdateAsync(int id, UpdatePropertyDto dto);
        Task DeleteAsync(int id);
        Task<IEnumerable<PropertyDto>> SearchAsync(string searchTerm, int page, int pageSize);
        Task<IEnumerable<PropertyDto>> GetByLocationAsync(string location, int page, int pageSize);
        Task<IEnumerable<PropertyDto>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice, int page, int pageSize);
        Task<IEnumerable<PropertyDto>> GetAvailableAsync(int page, int pageSize);
        Task IncrementViewCountAsync(int propertyId);
    }
}
