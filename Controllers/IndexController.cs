using Microsoft.AspNetCore.Mvc;

using ASPNETTweeter.Services;
using ASPNETTweeter.Models;
using Microsoft.AspNetCore.Html;

namespace ASPNETTweeter.Controllers;

/// <summary>
/// This Controller handles the population and displaying of the Tweeter
/// homepage.  It can be found at the index of the application and displays
/// simple usage instructions alongside the current tweets.
/// </summary>
[ApiController]
[Route("/")]
public class IndexController : Controller {
    /// <summary>
    /// This function handles the displaying of the index.  It uses a helper
    /// function to generate an HTML table representing the tweets, adds it
    /// to its View, and then displays it.
    /// </summary>
    /// <returns>A ViewResult representing the index page with the current tweets.</returns>
    [HttpGet]
    public ViewResult Index() {
        ViewData["Tweets"] = GenerateTweetTable(TweetService.GetTweets());

        return View();
    }

    private HtmlString GenerateTweetTable(List<Tweet> tweets) {
        string table = @"
        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Tweet</th>
                    <th>Likes</th>
                </tr>
            </thead>

            <tbody>
        ";

        foreach(Tweet tweet in tweets) {
            table += @$"
            <tr>
                <td>{tweet.Id}</td>
                <td>{tweet.Content}</td>
                <td>{tweet.Likes}</td>
            </tr>
            ";
        }

        return new HtmlString(table + "</tbody></table>");
    }
}