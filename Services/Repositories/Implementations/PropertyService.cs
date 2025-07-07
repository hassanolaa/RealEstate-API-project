using AutoMapper;
using realstate.DAL.Models;
using realstate.DAL.Repositories.Interfaces;
using realstate.Services.DTOs;
using realstate.Services.Repositories.Interfaces;

namespace realstate.Services.Repositories.Implementations
{
    public class PropertyService : IPropertyService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PropertyService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<PropertyDto> CreateAsync(CreatePropertyDto dto)
        {
            var entity = _mapper.Map<Property>(dto);
            await _uow.Properties.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return _mapper.Map<PropertyDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _uow.Properties.DeleteAsync(id);
            await _uow.SaveChangesAsync();
        }

        public async Task<IEnumerable<PropertyDto>> GetAllAsync(int page, int pageSize)
        {
            var list = await _uow.Properties.GetAllAsync(page, pageSize);
            return _mapper.Map<IEnumerable<PropertyDto>>(list);
        }

        public async Task<PropertyDto?> GetByIdAsync(int id)
        {
            var entity = await _uow.Properties.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<PropertyDto>(entity);
        }

        public async Task<IEnumerable<PropertyDto>> GetAvailableAsync(int page, int pageSize)
        {
            var list = await _uow.Properties.GetAvailablePropertiesAsync();
            return _mapper.Map<IEnumerable<PropertyDto>>(list);
        }

        public async Task<IEnumerable<PropertyDto>> GetByLocationAsync(string location, int page, int pageSize)
        {
            var list = await _uow.Properties.GetPropertiesByLocationAsync(location);
            return _mapper.Map<IEnumerable<PropertyDto>>(list);
        }

        public async Task<IEnumerable<PropertyDto>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice, int page, int pageSize)
        {
            var list = await _uow.Properties.GetPropertiesByPriceRangeAsync(minPrice, maxPrice);
            return _mapper.Map<IEnumerable<PropertyDto>>(list);
        }

        public async Task<IEnumerable<PropertyDto>> SearchAsync(string searchTerm, int page, int pageSize)
        {
            var list = await _uow.Properties.SearchPropertiesAsync(searchTerm);
            return _mapper.Map<IEnumerable<PropertyDto>>(list);
        }

        public async Task IncrementViewCountAsync(int propertyId)
        {
            await _uow.Properties.IncrementViewCountAsync(propertyId);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdatePropertyDto dto)
        {
            var entity = await _uow.Properties.GetByIdAsync(id);
            if (entity == null) throw new KeyNotFoundException("Property not found");
            _mapper.Map(dto, entity);
            await _uow.Properties.UpdateAsync(entity);
            await _uow.SaveChangesAsync();
        }
    }
}
