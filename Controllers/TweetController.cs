using Microsoft.AspNetCore.Mvc;

using ASPNETTweeter.Models;
using ASPNETTweeter.Services;
using MongoDB.Bson;

namespace ASPNETTweeter.Controllers;

[ApiController]
[Route("[controller]")]
public class TweetController : ControllerBase {
    [HttpGet]
    public ActionResult<List<Tweet>> GetTweets() => TweetService.GetTweets();

    [HttpPost]
    public IActionResult Tweet(Tweet tweet) {
        try {
            TweetService.AddTweet(tweet);

            return CreatedAtAction(nameof(GetTweets), null, tweet);
        } catch(ArgumentException) {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTweet(string id) {
        try {
            TweetService.DeleteTweet(new ObjectId(id));

            return NoContent();
        } catch(ArgumentException) {
            return NotFound();
        }
    }

    [HttpPut("{id}")]
    public IActionResult LikeTweet(string id) {
        try {
            TweetService.LikeTweet(new ObjectId(id));

            return NoContent();
        } catch(ArgumentException) {
            return NotFound();
        }
    }
}