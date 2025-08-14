using MongoDB.Driver;
using RealEstate.Domain.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Entities;

namespace RealEstate.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly IMongoCollection<PropertyDocument> _properties;

    public PropertyRepository(MongoDbContext context)
    {
        _properties = context.Properties;
    }

    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        var documents = await _properties.Find(_ => true).ToListAsync();
        return documents.Select(doc => doc.ToDomain());
    }

    public async Task<IEnumerable<Property>> GetFilteredAsync(PropertyFilterDto filter)
    {
        var filterBuilder = Builders<PropertyDocument>.Filter;
        var filterDefinition = filterBuilder.Empty;

        // Apply name filter
        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            filterDefinition &= filterBuilder.Regex(x => x.Name, 
                new MongoDB.Bson.BsonRegularExpression(filter.Name, "i"));
        }

        // Apply address filter
        if (!string.IsNullOrWhiteSpace(filter.Address))
        {
            filterDefinition &= filterBuilder.Regex(x => x.Address, 
                new MongoDB.Bson.BsonRegularExpression(filter.Address, "i"));
        }

        // Apply price range filters
        if (filter.MinPrice.HasValue)
        {
            filterDefinition &= filterBuilder.Gte(x => x.Price, filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            filterDefinition &= filterBuilder.Lte(x => x.Price, filter.MaxPrice.Value);
        }

        var skip = (filter.Page - 1) * filter.PageSize;
        
        var documents = await _properties.Find(filterDefinition)
            .SortByDescending(x => x.CreatedAt)
            .Skip(skip)
            .Limit(filter.PageSize)
            .ToListAsync();

        return documents.Select(doc => doc.ToDomain());
    }

    public async Task<Property?> GetByIdAsync(string id)
    {
        var document = await _properties.Find(x => x.Id == id).FirstOrDefaultAsync();
        return document?.ToDomain();
    }

    public async Task<Property> CreateAsync(Property property)
    {
        var document = PropertyDocument.FromDomain(property);
        await _properties.InsertOneAsync(document);
        return document.ToDomain();
    }

    public async Task<Property?> UpdateAsync(string id, Property property)
    {
        var document = PropertyDocument.FromDomain(property);
        var result = await _properties.ReplaceOneAsync(x => x.Id == id, document);
        return result.IsAcknowledged && result.ModifiedCount > 0 ? document.ToDomain() : null;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _properties.DeleteOneAsync(x => x.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task<long> GetCountAsync(PropertyFilterDto filter)
    {
        var filterBuilder = Builders<PropertyDocument>.Filter;
        var filterDefinition = filterBuilder.Empty;

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            filterDefinition &= filterBuilder.Regex(x => x.Name, 
                new MongoDB.Bson.BsonRegularExpression(filter.Name, "i"));
        }

        if (!string.IsNullOrWhiteSpace(filter.Address))
        {
            filterDefinition &= filterBuilder.Regex(x => x.Address, 
                new MongoDB.Bson.BsonRegularExpression(filter.Address, "i"));
        }

        if (filter.MinPrice.HasValue)
        {
            filterDefinition &= filterBuilder.Gte(x => x.Price, filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            filterDefinition &= filterBuilder.Lte(x => x.Price, filter.MaxPrice.Value);
        }

        return await _properties.CountDocumentsAsync(filterDefinition);
    }
}
