using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Entities;

public class PropertyDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    
    [BsonElement("idProperty")]
    public string IdProperty { get; set; } = string.Empty;
    
    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;
    
    [BsonElement("address")]
    public string Address { get; set; } = string.Empty;
    
    [BsonElement("price")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }
    
    [BsonElement("codeInternal")]
    public string CodeInternal { get; set; } = string.Empty;
    
    [BsonElement("year")]
    public int Year { get; set; }
    
    [BsonElement("idOwner")]
    public string IdOwner { get; set; } = string.Empty;
    
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
            IdProperty = property.IdProperty,
            IdOwner = property.IdOwner,
            Name = property.Name,
            Address = property.Address,
            Price = property.Price,
            CodeInternal = property.CodeInternal,
            Year = property.Year,
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
            IdProperty = IdProperty,
            IdOwner = IdOwner,
            Name = Name,
            Address = Address,
            Price = Price,
            CodeInternal = CodeInternal,
            Year = Year,
            ImageUrl = ImageUrl,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt
        };
    }
}
