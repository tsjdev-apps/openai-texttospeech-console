using System.Text.Json.Serialization;

namespace OpenAITTSConsole.Models;

/// <summary>
///     Represents a request to generate 
///     text-to-speech audio.
/// </summary>
/// <param name="Model"> The model to use for generating the audio. </param>
/// <param name="Input"> The input text to generate audio for. </param>
/// <param name="Voice"> The voice to use for generating the audio. </param>
/// <param name="ResponseFormat"> The format of the response. </param>
/// <param name="Speed"> The speed of the generated audio. </param>
internal record TextToSpeechRequest(
    [property: JsonPropertyName("model")] string Model, 
    [property: JsonPropertyName("input")] string Input, 
    [property: JsonPropertyName("voice")] string Voice,
    [property: JsonPropertyName("response_format")] string ResponseFormat = "mp3",
    [property: JsonPropertyName("speed")] double Speed = 1.0);