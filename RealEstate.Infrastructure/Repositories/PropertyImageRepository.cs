using MongoDB.Driver;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Entities;

namespace RealEstate.Infrastructure.Repositories;

public class PropertyImageRepository : IPropertyImageRepository
{
    private readonly IMongoCollection<PropertyImageDocument> _propertyImages;

    public PropertyImageRepository(MongoDbContext context)
    {
        _propertyImages = context.PropertyImages;
    }

    public async Task<IEnumerable<PropertyImage>> GetAllAsync()
    {
        var documents = await _propertyImages.Find(_ => true).ToListAsync();
        return documents.Select(doc => doc.ToDomain());
    }

    public async Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(string idProperty)
    {
        var documents = await _propertyImages.Find(img => img.IdProperty == idProperty).ToListAsync();
        return documents.Select(doc => doc.ToDomain());
    }

    public async Task<PropertyImage?> GetByIdAsync(string idPropertyImage)
    {
        var document = await _propertyImages.Find(img => img.IdPropertyImage == idPropertyImage).FirstOrDefaultAsync();
        return document?.ToDomain();
    }

    public async Task<PropertyImage> CreateAsync(PropertyImage propertyImage)
    {
        var document = PropertyImageDocument.FromDomain(propertyImage);
        await _propertyImages.InsertOneAsync(document);
        return document.ToDomain();
    }

    public async Task<PropertyImage?> UpdateAsync(string idPropertyImage, PropertyImage propertyImage)
    {
        var filter = Builders<PropertyImageDocument>.Filter.Eq(img => img.IdPropertyImage, idPropertyImage);
        var update = Builders<PropertyImageDocument>.Update
            .Set(img => img.File, propertyImage.File)
            .Set(img => img.Enabled, propertyImage.Enabled);

        var result = await _propertyImages.UpdateOneAsync(filter, update);
        
        if (result.ModifiedCount > 0)
        {
            return await GetByIdAsync(idPropertyImage);
        }
        
        return null;
    }

    public async Task<bool> DeleteAsync(string idPropertyImage)
    {
        var filter = Builders<PropertyImageDocument>.Filter.Eq(img => img.IdPropertyImage, idPropertyImage);
        var result = await _propertyImages.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}
