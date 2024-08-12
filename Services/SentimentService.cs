using Newtonsoft.Json;

using ASPNETTweeter.Models;
using System.Text;

namespace ASPNETTweeter.Services;

/// <summary>
/// This Service handles hitting the Flask app for sentiment analysis.
/// That is its only purpose.
/// </summary>
public class SentimentService {
    private static readonly HttpClient httpClient = new HttpClient();

    /// <summary>
    /// This method hits the Flask app to conduct sentiment analysis on the
    /// given content.
    /// </summary>
    /// <param name="content">The text to conduct sentiment analysis on.</param>
    /// <returns>A SentimentAnalysisResult representing the results of the sentiment
    /// analysis model on the content.</returns>
    public static string Analyze(string content) {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/sentiment") {
            Content = new StringContent(JsonConvert.SerializeObject(new Dictionary<string, string>(){{"content", content}}), 
                                        Encoding.UTF8, 
                                        "application/json")
        };

        HttpResponseMessage responseMessage = httpClient.Send(requestMessage);

        using StreamReader reader = new StreamReader(responseMessage.Content.ReadAsStream());

        return reader.ReadToEnd();
    }
}