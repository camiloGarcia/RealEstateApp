using MongoDB.Driver;

namespace RealEstate.Infrastructure.Data;

public static class MongoIndexInitializer
{
    private static bool _initialized;

    public static async Task EnsureIndexesAsync(MongoDbContext context)
    {
        if (_initialized) return;

        var keys = new List<CreateIndexModel<Entities.PropertyDocument>>
        {
            new(CreateIndexKeysDefinitionBuilder().Ascending(p => p.Name)),
            new(CreateIndexKeysDefinitionBuilder().Ascending(p => p.Address)),
            new(CreateIndexKeysDefinitionBuilder().Ascending(p => p.Price))
        };

        await context.Properties.Indexes.CreateManyAsync(keys);
        _initialized = true;
    }

    private static IndexKeysDefinitionBuilder<Entities.PropertyDocument> CreateIndexKeysDefinitionBuilder()
        => Builders<Entities.PropertyDocument>.IndexKeys;
}
