using MongoDB.Driver;
using MongoDB.Bson;

using ASPNETTweeter.Models;

namespace ASPNETTweeter.Services;

public static class TweetService {    
    static readonly IMongoCollection<Tweet> TweetCollection;

    static TweetService() {
        string? MONGO_INITDB_ROOT_PASSWORD = System.Environment.GetEnvironmentVariable("MONGO_INITDB_ROOT_PASSWORD");
        
        TweetCollection = new MongoClient($"mongodb://root:{MONGO_INITDB_ROOT_PASSWORD}@mongo")
                                         .GetDatabase("tweets")
                                         .GetCollection<Tweet>("tweets");

    }

    public static List<Tweet> GetTweets() {
        return TweetCollection.Find(_ => true).ToList();
    }

    public static void AddTweet(Tweet tweet) {
        uint MAX_TWEET_LENGTH = 280;

        if(string.IsNullOrEmpty(tweet.Content)) {
            throw new ArgumentException("empty or null content string was passed");
        } else if(tweet.Content.Length > MAX_TWEET_LENGTH) {
            throw new ArgumentException("tweet was greater than max tweet length of 280 characters");
        } else {
            TweetCollection.InsertOne(tweet);
        }
    }

    public static void DeleteTweet(ObjectId id) {
        long deletedCount = TweetCollection.DeleteOne(tweet => tweet.Id == id).DeletedCount;

        if(deletedCount == 0) {
            throw new ArgumentException("tweet with given ID not found");
        }
    }

    public static void LikeTweet(ObjectId id) {
        UpdateDefinition<Tweet> likeUpdate = Builders<Tweet>.Update.Inc("Likes", 1);

        long updatedCount = TweetCollection.UpdateOne(tweet => tweet.Id == id, likeUpdate).ModifiedCount;

        if(updatedCount == 0) {
            throw new ArgumentException("tweet with given ID not found");
        }
    }
}