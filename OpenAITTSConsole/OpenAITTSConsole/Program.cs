using OpenAITTSConsole.Models;
using Spectre.Console;
using System.Net.Http.Headers;
using System.Net.Http.Json;

var isRunning = true;

while (isRunning)
{
    AnsiConsole.Clear();

    // Create header
    CreateHeader();

    // Get Token
    string token = GetToken();

    AnsiConsole.Clear();
    CreateHeader();

    // Get input
    string input = GetInput();

    AnsiConsole.Clear();
    CreateHeader();

    // Get model
    string model = GetModel().ToLower();

    // Get voice
    string voice = GetVoice().ToLower();

    // Get file path
    string filePath = GetFilePath();

    AnsiConsole.Clear();
    CreateHeader();

    // LOGIC
    await AnsiConsole.Status()
        .StartAsync("Generating your MP3 file...", async ctx =>
        {
            string textToSpeechEndpoint = "https://api.openai.com/v1/audio/speech";

            HttpClient client = new();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            TextToSpeechRequest textToSpeechRequest = new(model, input, voice);

            HttpResponseMessage result = await client.PostAsJsonAsync(textToSpeechEndpoint, textToSpeechRequest);

            try
            {
                var mp3Bytes = await result.Content.ReadAsByteArrayAsync();
                File.WriteAllBytes(filePath, mp3Bytes);

                AnsiConsole.MarkupLine($"You're MP3 file was created successfully. You will find it here: [yellow]{filePath}[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"Something went wrong: [red]{ex.Message}[/]");
            }
        });

    AnsiConsole.WriteLine();
    isRunning = AnsiConsole.Confirm("Do you want to create another mp3 file?", false);
}


/// <summary>
///     Creates the header for the console application.
/// </summary>
static void CreateHeader()
{
    // Create a grid for the header text
    Grid grid = new();
    grid.AddColumn();
    grid.AddRow(new FigletText("Text-To-Speech").Centered().Color(Color.Red));
    grid.AddRow(Align.Center(new Panel("[red]Sample by Thomas Sebastian Jensen ([link]https://www.tsjdev-apps.de[/])[/]")));

    // Write the grid to the console
    AnsiConsole.Write(grid);
    AnsiConsole.WriteLine();
}

/// <summary>
///     Prompts the user for their OpenAI API key.
/// </summary>
/// <returns>The user's OpenAI API key.</returns>
static string GetToken()
    => AnsiConsole.Prompt(
        new TextPrompt<string>("Please insert your [yellow]OpenAI API key[/]:")
        .PromptStyle("white")
        .ValidationErrorMessage("[red]Invalid prompt[/]")
        .Validate(prompt =>
        {
            if (prompt.Length < 3)
            {
                return ValidationResult.Error("[red]API key too short[/]");
            }

            if (prompt.Length > 200)
            {
                return ValidationResult.Error("[red]API key too long[/]");
            }

            return ValidationResult.Success();
        }));

/// <summary>
///     Prompts the user for the input to use for text-to-speech.
/// </summary>
/// <returns>The user's input for text-to-speech.</returns>
static string GetInput()
    => AnsiConsole.Prompt(
        new TextPrompt<string>("Please insert your [yellow]input[/]:")
        .PromptStyle("white")
        .ValidationErrorMessage("[red]Invalid input[/]")
        .Validate(prompt =>
        {
            if (prompt.Length < 3)
            {
                return ValidationResult.Error("[red]Input too short[/]");
            }

            if (prompt.Length > 4096)
            {
                return ValidationResult.Error("[red]Input too long[/]");
            }

            return ValidationResult.Success();
        }));

/// <summary>
///     Prompts the user for the model used for text-to-speech
/// </summary>
/// <returns>The user's selected model for text-to-speech.</returns>
static string GetModel()
    => AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("Please select the [yellow]model[/].")
        .PageSize(10)
        .AddChoices(new[] {
            "TTS-1", "TTS-1-HD"
        }));

/// <summary>
///     Prompts the user for the voice for text-to-speech.
/// </summary>
/// <returns>The user's selected voice for the text-to-speech.</returns>
static string GetVoice()
    => AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("Please select the [yellow]voice[/].")
        .PageSize(10)
        .AddChoices(new[] {
            "Alloy", "Echo", "Fable",
            "Onyx", "Nova", "Shimmer",
        }));

/// <summary>
///     Prompts the user for the file path to store the mp3 file.
/// </summary>
/// <returns>The user's selected file path for the mp3 file.</returns>
static string GetFilePath()
    => AnsiConsole.Prompt(
        new TextPrompt<string>("Please insert the [yellow]path[/] for the MP3 file:")
        .PromptStyle("white")
        .ValidationErrorMessage("[red]Invalid input[/]")
        .Validate(prompt =>
        {
            if (prompt.Length < 3)
            {
                return ValidationResult.Error("[red]File path too short[/]");
            }

            if (prompt.Length > 256)
            {
                return ValidationResult.Error("[red]File path too long[/]");
            }

            return ValidationResult.Success();
        }));