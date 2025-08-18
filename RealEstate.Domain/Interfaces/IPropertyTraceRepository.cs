using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces;

public interface IPropertyTraceRepository
{
    Task<IEnumerable<PropertyTrace>> GetAllAsync();
    Task<IEnumerable<PropertyTrace>> GetByPropertyIdAsync(string idProperty);
    Task<PropertyTrace?> GetByIdAsync(string idPropertyTrace);
    Task<PropertyTrace> CreateAsync(PropertyTrace propertyTrace);
    Task<PropertyTrace?> UpdateAsync(string idPropertyTrace, PropertyTrace propertyTrace);
    Task<bool> DeleteAsync(string idPropertyTrace);
}
