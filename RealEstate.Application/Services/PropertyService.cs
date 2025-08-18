using AutoMapper;
using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Filters;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IMapper _mapper;

    public PropertyService(IPropertyRepository propertyRepository, IMapper mapper)
    {
        _propertyRepository = propertyRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PropertyDto>> GetAllPropertiesAsync()
    {
        var properties = await _propertyRepository.GetAllAsync();
        return properties.Select(p => _mapper.Map<PropertyDto>(p));
    }

    public async Task<IEnumerable<PropertyDto>> GetFilteredPropertiesAsync(PropertyFilterDto filter)
    {
        var domainFilter = new PropertyFilter
        {
            Name = filter.Name,
            Address = filter.Address,
            MinPrice = filter.MinPrice,
            MaxPrice = filter.MaxPrice,
            Page = filter.Page,
            PageSize = filter.PageSize
        };
        var properties = await _propertyRepository.GetFilteredAsync(domainFilter);
        return properties.Select(p => _mapper.Map<PropertyDto>(p));
    }

    public async Task<PropertyDto?> GetPropertyByIdAsync(string id)
    {
        var property = await _propertyRepository.GetByIdAsync(id);
        return property != null ? _mapper.Map<PropertyDto>(property) : null;
    }

    public async Task<PropertyDto> CreatePropertyAsync(CreatePropertyDto createDto)
    {
        var property = new Property
        {
            // Id will be generated as a Mongo ObjectId in the repository if empty
            IdProperty = Guid.NewGuid().ToString(),
            IdOwner = createDto.IdOwner,
            Name = createDto.Name,
            Address = createDto.Address,
            Price = createDto.Price,
            CodeInternal = createDto.CodeInternal,
            Year = createDto.Year,
            ImageUrl = createDto.ImageUrl,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var createdProperty = await _propertyRepository.CreateAsync(property);
        return _mapper.Map<PropertyDto>(createdProperty);
    }

    public async Task<PropertyDto?> UpdatePropertyAsync(string id, UpdatePropertyDto updateDto)
    {
        var existingProperty = await _propertyRepository.GetByIdAsync(id);
        if (existingProperty == null)
            return null;

        existingProperty.Name = updateDto.Name;
        existingProperty.Address = updateDto.Address;
        existingProperty.Price = updateDto.Price;
        existingProperty.CodeInternal = updateDto.CodeInternal;
        existingProperty.Year = updateDto.Year;
        existingProperty.ImageUrl = updateDto.ImageUrl;
        existingProperty.UpdatedAt = DateTime.UtcNow;

        var updatedProperty = await _propertyRepository.UpdateAsync(id, existingProperty);
        return updatedProperty != null ? _mapper.Map<PropertyDto>(updatedProperty) : null;
    }

    public async Task<bool> DeletePropertyAsync(string id)
    {
        return await _propertyRepository.DeleteAsync(id);
    }

    public async Task<long> GetPropertiesCountAsync(PropertyFilterDto filter)
    {
        var domainFilter = new PropertyFilter
        {
            Name = filter.Name,
            Address = filter.Address,
            MinPrice = filter.MinPrice,
            MaxPrice = filter.MaxPrice,
            Page = filter.Page,
            PageSize = filter.PageSize
        };
        return await _propertyRepository.GetCountAsync(domainFilter);
    }
}
