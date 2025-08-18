using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Entities;

public class OwnerDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("idOwner")]
    public string IdOwner { get; set; } = string.Empty;

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("address")]
    public string Address { get; set; } = string.Empty;

    [BsonElement("photo")]
    public string Photo { get; set; } = string.Empty;

    [BsonElement("birthday")]
    public DateTime Birthday { get; set; }

    public static OwnerDocument FromDomain(Owner owner)
    {
        return new OwnerDocument
        {
            IdOwner = owner.IdOwner,
            Name = owner.Name,
            Address = owner.Address,
            Photo = owner.Photo,
            Birthday = owner.Birthday
        };
    }

    public Owner ToDomain()
    {
        return new Owner
        {
            IdOwner = IdOwner,
            Name = Name,
            Address = Address,
            Photo = Photo,
            Birthday = Birthday
        };
    }
}
