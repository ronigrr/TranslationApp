namespace TranslationApp.Infrastructure;

public interface ITranslationService
{ 
    Task<string> TranslateToHebrewAsync(string wordToTranslate);
}