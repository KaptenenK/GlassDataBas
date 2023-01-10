using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GlassDataBas.Models;

public class Glass
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string GlassName { get; set; } 

    public string? Description { get; set; } 

    public decimal Price { get; set; } 
    public string Category { get; set; } 

   
}
