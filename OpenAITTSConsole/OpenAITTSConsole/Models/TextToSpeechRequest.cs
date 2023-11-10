using System.Text.Json.Serialization;

namespace OpenAITTSConsole.Models;

/// <summary>
///     Represents a request to generate 
///     text-to-speech audio.
/// </summary>
internal class TextToSpeechRequest
{
    /// <summary>
    ///     The model to use for generating the audio.
    /// </summary>
    [JsonPropertyName("model")]
    public required string Model { get; set; }

    /// <summary>
    ///     The input text to generate audio for.
    /// </summary>
    [JsonPropertyName("input")]
    public required string Input { get; set; }

    /// <summary>
    ///     The voice to use for generating the audio.
    /// </summary>
    [JsonPropertyName("voice")]
    public required string Voice { get; set; }

    /// <summary>
    ///     The format of the response audio.
    /// </summary>
    [JsonPropertyName("response_format")]
    public string ResponseFormat { get; } = "mp3";

    /// <summary>
    ///     The speed of the generated audio.
    /// </summary>
    [JsonPropertyName("speed")]
    public double Speed { get; } = 1.0;
}
