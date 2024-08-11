using Newtonsoft.Json;

using ASPNETTweeter.Models;

namespace ASPNETTweeter.Services;

public class SentimentService {
    private static readonly HttpClient httpClient = new HttpClient();

    static SentimentAnalysisResult? AnalyzeTweet(string content) {
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/sentiment") {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>(){{"content", content}})
        };

        HttpResponseMessage responseMessage = httpClient.Send(requestMessage);

        using StreamReader reader = new StreamReader(responseMessage.Content.ReadAsStream());

        return JsonConvert.DeserializeObject<SentimentAnalysisResult>(reader.ReadToEnd());
    }
}