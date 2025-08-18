using MongoDB.Driver;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Entities;

namespace RealEstate.Infrastructure.Repositories;

public class PropertyTraceRepository : IPropertyTraceRepository
{
    private readonly IMongoCollection<PropertyTraceDocument> _propertyTraces;

    public PropertyTraceRepository(MongoDbContext context)
    {
        _propertyTraces = context.PropertyTraces;
    }

    public async Task<IEnumerable<PropertyTrace>> GetAllAsync()
    {
        var documents = await _propertyTraces.Find(_ => true).ToListAsync();
        return documents.Select(doc => doc.ToDomain());
    }

    public async Task<IEnumerable<PropertyTrace>> GetByPropertyIdAsync(string idProperty)
    {
        var documents = await _propertyTraces.Find(trace => trace.IdProperty == idProperty).ToListAsync();
        return documents.Select(doc => doc.ToDomain());
    }

    public async Task<PropertyTrace?> GetByIdAsync(string idPropertyTrace)
    {
        var document = await _propertyTraces.Find(trace => trace.IdPropertyTrace == idPropertyTrace).FirstOrDefaultAsync();
        return document?.ToDomain();
    }

    public async Task<PropertyTrace> CreateAsync(PropertyTrace propertyTrace)
    {
        var document = PropertyTraceDocument.FromDomain(propertyTrace);
        await _propertyTraces.InsertOneAsync(document);
        return document.ToDomain();
    }

    public async Task<PropertyTrace?> UpdateAsync(string idPropertyTrace, PropertyTrace propertyTrace)
    {
        var filter = Builders<PropertyTraceDocument>.Filter.Eq(trace => trace.IdPropertyTrace, idPropertyTrace);
        var update = Builders<PropertyTraceDocument>.Update
            .Set(trace => trace.DateSale, propertyTrace.DateSale)
            .Set(trace => trace.Name, propertyTrace.Name)
            .Set(trace => trace.Value, propertyTrace.Value)
            .Set(trace => trace.Tax, propertyTrace.Tax);

        var result = await _propertyTraces.UpdateOneAsync(filter, update);
        
        if (result.ModifiedCount > 0)
        {
            return await GetByIdAsync(idPropertyTrace);
        }
        
        return null;
    }

    public async Task<bool> DeleteAsync(string idPropertyTrace)
    {
        var filter = Builders<PropertyTraceDocument>.Filter.Eq(trace => trace.IdPropertyTrace, idPropertyTrace);
        var result = await _propertyTraces.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}
