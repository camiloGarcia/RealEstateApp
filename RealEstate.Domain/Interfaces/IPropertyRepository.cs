using RealEstate.Domain.DTOs;
using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllAsync();
    Task<IEnumerable<Property>> GetFilteredAsync(PropertyFilterDto filter);
    Task<Property?> GetByIdAsync(string id);
    Task<Property> CreateAsync(Property property);
    Task<Property?> UpdateAsync(string id, Property property);
    Task<bool> DeleteAsync(string id);
    Task<long> GetCountAsync(PropertyFilterDto filter);
}
