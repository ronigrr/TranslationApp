using System.Collections.Concurrent;
using TranslationApp.Abstractions.Entities;
using TranslationApp.Infrastructure;

namespace TranslationApp.Repositorys;

public class InMemoryTranslationRepository : ITranslationRepository
{
    private readonly ConcurrentDictionary<string, TranslationEntry> _translations;
    
    public InMemoryTranslationRepository()
    {
        _translations = new ConcurrentDictionary<string, TranslationEntry>();
    }

    public Task <TranslationEntry?> GetTranslation(string word)
    {
        return Task.FromResult(_translations.GetValueOrDefault(word.ToLower()));
    }

    public Task SaveTranslation(string word, string wordTranslation)
    {
       var entry = new TranslationEntry(word, wordTranslation);
       _translations[word.ToLower()] = entry;
       return Task.CompletedTask;
    }

    public Task<TranslationEntry?> FindMostSimilarWordAsync(string word)
    {
        if (_translations.IsEmpty)
        {
            return Task.FromResult<TranslationEntry?>(null);
        }
        
        TranslationEntry? mostSimilarEntry = null;
        var highestCommonCount = -1;
        
        foreach (var entry in _translations.Values)
        {
            var commonChars = entry.CalculateCommonCharacters(word);

            if (commonChars > highestCommonCount)
            {
                highestCommonCount = commonChars;
                mostSimilarEntry = entry;
            }
        }
        
        return Task.FromResult(mostSimilarEntry);
    }
}