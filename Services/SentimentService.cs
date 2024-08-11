using Newtonsoft.Json;

using ASPNETTweeter.Models;
using System.Text;

namespace ASPNETTweeter.Services;

public class SentimentService {
    private static readonly HttpClient httpClient = new HttpClient();

    public static SentimentAnalysisResult? AnalyzeTweet(string content) {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/sentiment") {
            Content = new StringContent(JsonConvert.SerializeObject(new Dictionary<string, string>(){{"content", content}}), 
                                        Encoding.UTF8, 
                                        "application/json")
        };

        HttpResponseMessage responseMessage = httpClient.Send(requestMessage);

        using StreamReader reader = new StreamReader(responseMessage.Content.ReadAsStream());

        return JsonConvert.DeserializeObject<SentimentAnalysisResult>(reader.ReadToEnd());
    }
}