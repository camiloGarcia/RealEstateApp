using RealEstate.Domain.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync()
    {
        var properties = await _propertyRepository.GetAllAsync();
        return properties.Select(MapToDto);
    }

    public async Task<IEnumerable<PropertyDto>> GetFilteredPropertiesAsync(PropertyFilterDto filter)
    {
        var properties = await _propertyRepository.GetFilteredAsync(filter);
        return properties.Select(MapToDto);
    }

    public async Task<PropertyDto?> GetPropertyByIdAsync(string id)
    {
        var property = await _propertyRepository.GetByIdAsync(id);
        return property != null ? MapToDto(property) : null;
    }

    public async Task<PropertyDto> CreatePropertyAsync(CreatePropertyDto createDto)
    {
        var property = new Property
        {
            IdOwner = createDto.IdOwner,
            Name = createDto.Name,
            Address = createDto.Address,
            Price = createDto.Price,
            ImageUrl = createDto.ImageUrl,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var createdProperty = await _propertyRepository.CreateAsync(property);
        return MapToDto(createdProperty);
    }

    public async Task<PropertyDto?> UpdatePropertyAsync(string id, UpdatePropertyDto updateDto)
    {
        var existingProperty = await _propertyRepository.GetByIdAsync(id);
        if (existingProperty == null)
            return null;

        existingProperty.Name = updateDto.Name;
        existingProperty.Address = updateDto.Address;
        existingProperty.Price = updateDto.Price;
        existingProperty.ImageUrl = updateDto.ImageUrl;
        existingProperty.UpdatedAt = DateTime.UtcNow;

        var updatedProperty = await _propertyRepository.UpdateAsync(id, existingProperty);
        return updatedProperty != null ? MapToDto(updatedProperty) : null;
    }

    public async Task<bool> DeletePropertyAsync(string id)
    {
        return await _propertyRepository.DeleteAsync(id);
    }

    public async Task<long> GetPropertiesCountAsync(PropertyFilterDto filter)
    {
        return await _propertyRepository.GetCountAsync(filter);
    }

    private static PropertyDto MapToDto(Property property)
    {
        return new PropertyDto
        {
            Id = property.Id,
            IdOwner = property.IdOwner,
            Name = property.Name,
            Address = property.Address,
            Price = property.Price,
            ImageUrl = property.ImageUrl
        };
    }
}
