using Microsoft.AspNetCore.Mvc;

using ASPNETTweeter.Models;
using ASPNETTweeter.Services;

namespace ASPNETTweeter.Controllers;

/// <summary>
/// This Controller handles the REST API for tweet management.  It provides
/// multiple endpoints for simple CRUD operations, except for updating tweets,
/// only liking them.
/// </summary>
[ApiController]
[Route("[controller]")]
public class TweetController : ControllerBase {
    /// <summary>
    /// This endpoint gets all of the tweets currently in the database.
    /// </summary>
    /// <returns>An ActionResult<List<Tweet>> containing all the tweets
    /// currently in the database.</returns>
    [HttpGet]
    public ActionResult<List<Tweet>> GetTweets() => TweetService.GetTweets();

    /// <summary>
    /// This endpoint creates a Tweet and adds it to the database.
    /// </summary>
    /// <param name="tweet">The Tweet to create, accepted in the body of the 
    /// request as JSON.</param>
    /// <returns>An IActionResult representing the created Tweet or an error if the
    /// Tweet could not be created (e.g. it was longer than the max limit).</returns>
    [HttpPost]
    public IActionResult Tweet(Tweet tweet) {
        try {
            TweetService.AddTweet(tweet);

            return CreatedAtAction(nameof(GetTweets), null, tweet);
        } catch(ArgumentException) {
            return BadRequest();
        }
    }

    /// <summary>
    /// This endpoint deletes a Tweet, removing it from the database.
    /// </summary>
    /// <param name="id">The ID of the Tweet to delete.</param>
    /// <returns>An IActionResult indicating success or failure depending on
    /// whether a Tweet with the given ID existed.</returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteTweet(string id) {
        try {
            TweetService.DeleteTweet(id);

            return NoContent();
        } catch(Exception e) when (e is ArgumentException || e is FormatException) {
            return NotFound();
        }
    }

    /// <summary>
    /// This endpoint likes a Tweet, incrementing its like counter in the database.
    /// </summary>
    /// <param name="id">The ID of the Tweet to delete.</param>
    /// <returns>An IActionResult indicating success or failure depending on
    /// whether a Tweet with the given ID existed.</returns>
    [HttpPatch("{id}")]
    public IActionResult LikeTweet(string id) {
        try {
            TweetService.LikeTweet(id);

            return NoContent();
        } catch(Exception e) when (e is ArgumentException || e is FormatException) {
            return NotFound();
        }
    }
}