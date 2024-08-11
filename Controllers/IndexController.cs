using Microsoft.AspNetCore.Mvc;

using ASPNETTweeter.Services;
using ASPNETTweeter.Models;
using Microsoft.AspNetCore.Html;

namespace ASPNETTweeter.Controllers;

[ApiController]
[Route("/")]
public class IndexController : Controller {
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