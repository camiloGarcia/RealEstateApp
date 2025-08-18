using MongoDB.Driver;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Interfaces;
using RealEstate.Infrastructure.Data;
using RealEstate.Infrastructure.Entities;

namespace RealEstate.Infrastructure.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly IMongoCollection<OwnerDocument> _owners;

    public OwnerRepository(MongoDbContext context)
    {
        _owners = context.Owners;
    }

    public async Task<IEnumerable<Owner>> GetAllAsync()
    {
        var documents = await _owners.Find(_ => true).ToListAsync();
        return documents.Select(doc => doc.ToDomain());
    }

    public async Task<Owner?> GetByIdAsync(string idOwner)
    {
        var document = await _owners.Find(owner => owner.IdOwner == idOwner).FirstOrDefaultAsync();
        return document?.ToDomain();
    }

    public async Task<Owner> CreateAsync(Owner owner)
    {
        var document = OwnerDocument.FromDomain(owner);
        await _owners.InsertOneAsync(document);
        return document.ToDomain();
    }

    public async Task<Owner?> UpdateAsync(string idOwner, Owner owner)
    {
        var filter = Builders<OwnerDocument>.Filter.Eq(o => o.IdOwner, idOwner);
        var update = Builders<OwnerDocument>.Update
            .Set(o => o.Name, owner.Name)
            .Set(o => o.Address, owner.Address)
            .Set(o => o.Photo, owner.Photo)
            .Set(o => o.Birthday, owner.Birthday);

        var result = await _owners.UpdateOneAsync(filter, update);
        
        if (result.ModifiedCount > 0)
        {
            return await GetByIdAsync(idOwner);
        }
        
        return null;
    }

    public async Task<bool> DeleteAsync(string idOwner)
    {
        var filter = Builders<OwnerDocument>.Filter.Eq(o => o.IdOwner, idOwner);
        var result = await _owners.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}
