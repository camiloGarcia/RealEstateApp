using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Entities;

public class PropertyTraceDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("idPropertyTrace")]
    public string IdPropertyTrace { get; set; } = string.Empty;

    [BsonElement("dateSale")]
    public DateTime DateSale { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("value")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Value { get; set; }

    [BsonElement("tax")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Tax { get; set; }

    [BsonElement("idProperty")]
    public string IdProperty { get; set; } = string.Empty;

    public static PropertyTraceDocument FromDomain(PropertyTrace propertyTrace)
    {
        return new PropertyTraceDocument
        {
            IdPropertyTrace = propertyTrace.IdPropertyTrace,
            DateSale = propertyTrace.DateSale,
            Name = propertyTrace.Name,
            Value = propertyTrace.Value,
            Tax = propertyTrace.Tax,
            IdProperty = propertyTrace.IdProperty
        };
    }

    public PropertyTrace ToDomain()
    {
        return new PropertyTrace
        {
            IdPropertyTrace = IdPropertyTrace,
            DateSale = DateSale,
            Name = Name,
            Value = Value,
            Tax = Tax,
            IdProperty = IdProperty
        };
    }
}
