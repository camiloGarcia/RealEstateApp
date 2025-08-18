using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Interfaces;

public interface IPropertyImageRepository
{
    Task<IEnumerable<PropertyImage>> GetAllAsync();
    Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(string idProperty);
    Task<PropertyImage?> GetByIdAsync(string idPropertyImage);
    Task<PropertyImage> CreateAsync(PropertyImage propertyImage);
    Task<PropertyImage?> UpdateAsync(string idPropertyImage, PropertyImage propertyImage);
    Task<bool> DeleteAsync(string idPropertyImage);
}
