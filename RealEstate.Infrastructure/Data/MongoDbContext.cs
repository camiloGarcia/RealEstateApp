using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RealEstate.Infrastructure.Entities;

namespace RealEstate.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<PropertyDocument> _properties;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB");
        var client = new MongoClient(connectionString);
        var databaseName = configuration["MongoDB:DatabaseName"] ?? "RealEstateDB";
        _database = client.GetDatabase(databaseName);
        _properties = _database.GetCollection<PropertyDocument>("Properties");
    }

    public IMongoCollection<PropertyDocument> Properties => _properties;
}
