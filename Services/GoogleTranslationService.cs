using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;
using TranslationApp.Infrastructure;

namespace TranslationApp.Services;

public class GoogleTranslationService : ITranslationService
{
    private readonly ITranslationRepository _repository;
    private readonly TranslationClient _translationClient;
    private const bool UseTestValue = false;

    public GoogleTranslationService(ITranslationRepository repository)
    {
        _repository = repository;
        //Connect and create the client
        var credentialsPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
        try
        {
            var credential = GoogleCredential.FromFile(credentialsPath);

            if (credential == null)
            {
                throw new Exception("Failed to load Google credentials from file");
            }

            _translationClient = TranslationClient.Create(credential);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to translate", ex);
        }
    }

    public async Task<string> TranslateToHebrewAsync(string wordToTranslate)
    {
        //simple null and length validation 
        if (string.IsNullOrWhiteSpace(wordToTranslate) || wordToTranslate.Length > 50)
        {
            throw new ArgumentException("Word is empty or exceed 50 characters.");
        }
        
        //check if the word is in memory first
        var existingTranslation = await _repository.GetTranslation(wordToTranslate);

        if (existingTranslation != null)
        {
            Console.WriteLine($"Found {wordToTranslate} translation in cache");
            return existingTranslation.HebrewTranslation;
        }

        if (UseTestValue)
        {
            const string response = "בדיקה";
            await _repository.SaveTranslation(wordToTranslate, response);
            return response;
        }

        try
        {
            //try google api
            var response = await _translationClient.TranslateTextAsync(
                text: wordToTranslate,
                targetLanguage: "he",
                sourceLanguage: "en");

            if (response != null && !string.IsNullOrEmpty(response.TranslatedText))
            {
                await _repository.SaveTranslation(wordToTranslate, response.TranslatedText);
                Console.WriteLine($"Successfully translated '{wordToTranslate}' to '{response.TranslatedText}'");
                return response.TranslatedText;
            }


            var similarWord = await _repository.FindMostSimilarWordAsync(wordToTranslate);
            if (similarWord != null)
            {
                return similarWord.HebrewTranslation;
            }


            Console.WriteLine($"Google Translate returned null or empty response for '{wordToTranslate}'");
            throw new Exception("Translation API returned empty response");
        }
        catch (Exception ex)
        {
            throw new Exception($"Google Translate API error: {ex.Message} , {ex.StackTrace}");
        }
        
    }
}