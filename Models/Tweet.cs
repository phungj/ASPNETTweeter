using MongoDB.Bson;

namespace ASPNETTweeter.Models;

public class Tweet {
    public ObjectId Id {get; set;}
    public string? Content {get; set;}

    public uint Likes {get; set;} = 0;
}