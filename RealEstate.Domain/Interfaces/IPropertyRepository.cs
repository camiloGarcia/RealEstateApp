using RealEstate.Domain.Entities;
using RealEstate.Domain.Filters;
namespace RealEstate.Domain.Interfaces;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAllAsync();
    Task<IEnumerable<Property>> GetFilteredAsync(PropertyFilter filter);
    Task<Property?> GetByIdAsync(string id);
    Task<Property> CreateAsync(Property property);
    Task<Property?> UpdateAsync(string id, Property property);
    Task<bool> DeleteAsync(string id);
    Task<long> GetCountAsync(PropertyFilter filter);
}
