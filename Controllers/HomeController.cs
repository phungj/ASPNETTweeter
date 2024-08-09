using Microsoft.AspNetCore.Mvc;

using ASPNETTweeter.Services;
using ASPNETTweeter.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNETTweeter.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : Controller {
    [HttpGet]
    public ViewResult Home() {
        return View();
    }

    private string GenerateTweetTable(List<Tweet> tweets) {
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

        // TODO: Complete impl

        return table;
    }
}