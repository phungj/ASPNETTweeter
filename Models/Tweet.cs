using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ASPNETTweeter.Models;

/// <summary>
/// This class represents the schema for a tweet as it is stored in
/// MongoDB.  Note that the Id property has some additional annotations 
/// that encode the MongoDB ObjectID as a string for ease of use.
/// </summary>
public class Tweet {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id {get; set;}
    public string? Content {get; set;}

    public uint Likes {get; set;} = 0;
}