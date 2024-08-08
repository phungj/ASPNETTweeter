using Microsoft.AspNetCore.Mvc;

using ASPNETTweeter.Models;
using ASPNETTweeter.Services;
using System.Net;

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
}