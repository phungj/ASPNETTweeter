using MongoDB.Driver;

using ASPNETTweeter.Models;

namespace ASPNETTweeter.Services;

/// <summary>
/// This Service manages the MongoDB database containing the tweets.  It provides
/// methods for the basic CRUD operations for interacting with tweets, except for 
/// updating, which is liking instead.
/// </summary>
public static class TweetService {    
    static readonly IMongoCollection<Tweet> TweetCollection;

    static TweetService() {
        TweetCollection = new MongoClient($"mongodb://localhost")
                                         .GetDatabase("tweets")
                                         .GetCollection<Tweet>("tweets");
    }

    /// <summary>
    /// This method gets all the tweets currently in the database.
    /// </summary>
    /// <returns>A List<Tweet> containing every tweet currently in the database.</returns>
    public static List<Tweet> GetTweets() {
        return TweetCollection.Find(_ => true).ToList();
    }

    /// <summary>
    /// This method gets a specific tweet from the database.
    /// </summary>
    /// <param name="id">The ID of the tweet to get from the database.</param>
    /// <returns>The Tweet with the given ID from the database.</returns>
    /// <exception cref="ArgumentException">Thrown if a tweet with the given ID is not in the database.</exception>
    public static Tweet GetTweet(string id) {
        List<Tweet> tweets = TweetCollection.Find(tweet => tweet.Id == id).ToList();

        if(tweets.Count == 0) {
            throw new ArgumentException("tweet with given ID not found");
        } else {
            return tweets[0];
        }
    }

    /// <summary>
    /// This method adds a given tweet to the database.
    /// </summary>
    /// <param name="tweet">The Tweet to add to the database.</param>
    /// <exception cref="ArgumentException">Thrown if the tweet has no Content or is longer than the maximum tweet
    /// length of 280 characters.</exception>
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

    /// <summary>
    /// This method deletes the tweet with the given ID from the database.
    /// </summary>
    /// <param name="id">The ID of the tweet to delete from the database.</param>
    /// <exception cref="ArgumentException">Thrown if no tweet with the given ID exists in the database.</exception>
    public static void DeleteTweet(string id) {
        long deletedCount = TweetCollection.DeleteOne(tweet => tweet.Id == id).DeletedCount;

        if(deletedCount == 0) {
            throw new ArgumentException("tweet with given ID not found");
        }
    }

    /// <summary>
    /// This method increments the like counter of the tweet with the given ID
    /// in the database.
    /// </summary>
    /// <param name="id">The ID of the tweet to like in the database.</param>
    /// <exception cref="ArgumentException">Thrown if no tweet with the given ID exists in the database.</exception>
    public static void LikeTweet(string id) {
        UpdateDefinition<Tweet> likeUpdate = Builders<Tweet>.Update.Inc("Likes", 1);

        long updatedCount = TweetCollection.UpdateOne(tweet => tweet.Id == id, likeUpdate).ModifiedCount;

        if(updatedCount == 0) {
            throw new ArgumentException("tweet with given ID not found");
        }
    }
}