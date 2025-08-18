using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Entities;

public class PropertyImageDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("idPropertyImage")]
    public string IdPropertyImage { get; set; } = string.Empty;

    [BsonElement("idProperty")]
    public string IdProperty { get; set; } = string.Empty;

    [BsonElement("file")]
    public string File { get; set; } = string.Empty;

    [BsonElement("enabled")]
    public bool Enabled { get; set; }

    public static PropertyImageDocument FromDomain(PropertyImage propertyImage)
    {
        return new PropertyImageDocument
        {
            IdPropertyImage = propertyImage.IdPropertyImage,
            IdProperty = propertyImage.IdProperty,
            File = propertyImage.File,
            Enabled = propertyImage.Enabled
        };
    }

    public PropertyImage ToDomain()
    {
        return new PropertyImage
        {
            IdPropertyImage = IdPropertyImage,
            IdProperty = IdProperty,
            File = File,
            Enabled = Enabled
        };
    }
}
