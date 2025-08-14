using RealEstate.Domain.DTOs;
using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces;

public interface IPropertyService
{
    Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync();
    Task<IEnumerable<PropertyDto>> GetFilteredPropertiesAsync(PropertyFilterDto filter);
    Task<PropertyDto?> GetPropertyByIdAsync(string id);
    Task<PropertyDto> CreatePropertyAsync(CreatePropertyDto createDto);
    Task<PropertyDto?> UpdatePropertyAsync(string id, UpdatePropertyDto updateDto);
    Task<bool> DeletePropertyAsync(string id);
    Task<long> GetPropertiesCountAsync(PropertyFilterDto filter);
}
