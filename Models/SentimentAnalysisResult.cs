namespace ASPNETTweeter.Models;

/// <summary>
/// This class represents the schema for the output of the 
/// sentiment analysis model from the Flask app.
/// </summary>
public class SentimentAnalysisResult {
    public string? Sentiment {get;}
    public double Confidence {get;}
}