using Microsoft.AspNetCore.Mvc;

using ASPNETTweeter.Services;

namespace ASPNETTweeter.Controllers;

/// <summary>
/// This Controller handles the REST API for sentiment analysis.
/// It only has one endpoint and is mostly responsible for hitting
/// the Flask app and displaying the results.
/// </summary>
[ApiController]
[Route("[controller]")]
public class SentimentController : ControllerBase {
    /// <summary>
    /// This endpoint handles the sentiment analysis.  It expects a tweet ID
    /// and returns the result of the sentiment analysis from the Flask app.
    /// </summary>
    /// <param name="id">The ID of the tweet to analyze.</param>
    /// <returns>An IActionResult representing the sentiment analysis results or an error, if the 
    /// tweet ID did not exist.</returns>
    [HttpPost("{id}")]
    public IActionResult TweetSentimentAnalysis(string id) {
        try {            
            return Content(SentimentService.Analyze(TweetService.GetTweet(id).Content), "application/json");
        } catch(ArgumentException) {
            return NotFound();
        }
    }
}