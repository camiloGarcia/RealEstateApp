using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RealEstate.Infrastructure.Entities;

namespace RealEstate.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<PropertyDocument> _properties;
    private readonly IMongoCollection<OwnerDocument> _owners;
    private readonly IMongoCollection<PropertyImageDocument> _propertyImages;
    private readonly IMongoCollection<PropertyTraceDocument> _propertyTraces;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB");
        var client = new MongoClient(connectionString);
        var databaseName = configuration["MongoDB:DatabaseName"] ?? "RealEstateDB";
        _database = client.GetDatabase(databaseName);
        
        _properties = _database.GetCollection<PropertyDocument>("Properties");
        _owners = _database.GetCollection<OwnerDocument>("Owners");
        _propertyImages = _database.GetCollection<PropertyImageDocument>("PropertyImages");
        _propertyTraces = _database.GetCollection<PropertyTraceDocument>("PropertyTraces");
    }

    public IMongoCollection<PropertyDocument> Properties => _properties;
    public IMongoCollection<OwnerDocument> Owners => _owners;
    public IMongoCollection<PropertyImageDocument> PropertyImages => _propertyImages;
    public IMongoCollection<PropertyTraceDocument> PropertyTraces => _propertyTraces;
}
