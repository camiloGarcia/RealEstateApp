using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Entities;

public class PropertyDocument
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
    
    [BsonElement("price")]
    public decimal Price { get; set; }
    
    [BsonElement("imageUrl")]
    public string ImageUrl { get; set; } = string.Empty;
    
    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public static PropertyDocument FromDomain(Property property)
    {
        return new PropertyDocument
        {
            Id = property.Id,
            IdOwner = property.IdOwner,
            Name = property.Name,
            Address = property.Address,
            Price = property.Price,
            ImageUrl = property.ImageUrl,
            CreatedAt = property.CreatedAt,
            UpdatedAt = property.UpdatedAt
        };
    }

    public Property ToDomain()
    {
        return new Property
        {
            Id = Id,
            IdOwner = IdOwner,
            Name = Name,
            Address = Address,
            Price = Price,
            ImageUrl = ImageUrl,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt
        };
    }
}
