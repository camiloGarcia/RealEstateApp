using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces;

public interface IPropertyImageService
{
    Task<IEnumerable<PropertyImageDto>> GetAllPropertyImagesAsync();
    Task<IEnumerable<PropertyImageDto>> GetPropertyImagesByPropertyIdAsync(string idProperty);
    Task<PropertyImageDto?> GetPropertyImageByIdAsync(string idPropertyImage);
    Task<PropertyImageDto> CreatePropertyImageAsync(CreatePropertyImageDto createDto);
    Task<PropertyImageDto?> UpdatePropertyImageAsync(string idPropertyImage, UpdatePropertyImageDto updateDto);
    Task<bool> DeletePropertyImageAsync(string idPropertyImage);
}
