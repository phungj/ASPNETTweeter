using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ASPNETTweeter.Models;

public class Tweet {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}
    public string? Content {get; set;}

    public uint Likes {get; set;} = 0;
}