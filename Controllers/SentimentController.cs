using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using ASPNETTweeter.Services;

namespace ASPNETTweeter.Controllers;

[ApiController]
[Route("[controller]")]
public class SentimentController : ControllerBase {
    [HttpPost("{id}")]
    public IActionResult TweetSentimentAnalysis(string id) {
        try {
            return Content(JsonConvert.SerializeObject(SentimentService.AnalyzeTweet(TweetService.GetTweet(id).Content)), "application/json");
        } catch(ArgumentException) {
            return NotFound();
        }
    }
}