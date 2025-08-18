using RealEstate.Application.DTOs;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;

namespace RealEstate.Application.Services;

public class PropertyImageService : IPropertyImageService
{
    private readonly IPropertyImageRepository _propertyImageRepository;

    public PropertyImageService(IPropertyImageRepository propertyImageRepository)
    {
        _propertyImageRepository = propertyImageRepository;
    }

    public async Task<IEnumerable<PropertyImageDto>> GetAllPropertyImagesAsync()
    {
        var images = await _propertyImageRepository.GetAllAsync();
        return images.Select(img => new PropertyImageDto
        {
            IdPropertyImage = img.IdPropertyImage,
            IdProperty = img.IdProperty,
            File = img.File,
            Enabled = img.Enabled
        });
    }

    public async Task<IEnumerable<PropertyImageDto>> GetPropertyImagesByPropertyIdAsync(string idProperty)
    {
        var images = await _propertyImageRepository.GetByPropertyIdAsync(idProperty);
        return images.Select(img => new PropertyImageDto
        {
            IdPropertyImage = img.IdPropertyImage,
            IdProperty = img.IdProperty,
            File = img.File,
            Enabled = img.Enabled
        });
    }

    public async Task<PropertyImageDto?> GetPropertyImageByIdAsync(string idPropertyImage)
    {
        var image = await _propertyImageRepository.GetByIdAsync(idPropertyImage);
        if (image == null) return null;

        return new PropertyImageDto
        {
            IdPropertyImage = image.IdPropertyImage,
            IdProperty = image.IdProperty,
            File = image.File,
            Enabled = image.Enabled
        };
    }

    public async Task<PropertyImageDto> CreatePropertyImageAsync(CreatePropertyImageDto createDto)
    {
        var propertyImage = new PropertyImage
        {
            IdPropertyImage = Guid.NewGuid().ToString(),
            IdProperty = createDto.IdProperty,
            File = createDto.File,
            Enabled = createDto.Enabled
        };

        var createdImage = await _propertyImageRepository.CreateAsync(propertyImage);
        
        return new PropertyImageDto
        {
            IdPropertyImage = createdImage.IdPropertyImage,
            IdProperty = createdImage.IdProperty,
            File = createdImage.File,
            Enabled = createdImage.Enabled
        };
    }

    public async Task<PropertyImageDto?> UpdatePropertyImageAsync(string idPropertyImage, UpdatePropertyImageDto updateDto)
    {
        var existingImage = await _propertyImageRepository.GetByIdAsync(idPropertyImage);
        if (existingImage == null) return null;

        var updatedImage = new PropertyImage
        {
            IdPropertyImage = existingImage.IdPropertyImage,
            IdProperty = existingImage.IdProperty,
            File = updateDto.File,
            Enabled = updateDto.Enabled
        };

        var result = await _propertyImageRepository.UpdateAsync(idPropertyImage, updatedImage);
        if (result == null) return null;

        return new PropertyImageDto
        {
            IdPropertyImage = result.IdPropertyImage,
            IdProperty = result.IdProperty,
            File = result.File,
            Enabled = result.Enabled
        };
    }

    public async Task<bool> DeletePropertyImageAsync(string idPropertyImage)
    {
        return await _propertyImageRepository.DeleteAsync(idPropertyImage);
    }
}
