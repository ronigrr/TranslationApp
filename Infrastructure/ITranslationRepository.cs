using TranslationApp.Abstractions.Entities;

namespace TranslationApp.Infrastructure;

public interface ITranslationRepository
{
    Task<TranslationEntry?> GetTranslation(string englishWord);
    Task SaveTranslation(string word, string wordTranslation);
    Task<TranslationEntry?> FindMostSimilarWordAsync(string englishWord);
}