namespace TranslationApp.Abstractions.Entities;

public class TranslationEntry
{
    public TranslationEntry(string englishWord, string hebrewTranslation)
    {
        EnglishWord = englishWord;
        HebrewTranslation = hebrewTranslation;
    }

    private string EnglishWord { get; set; }
    public string HebrewTranslation { get; set; }
    
    public int CalculateCommonCharacters(string otherWord)
    {
        var translationEntryWordSet = new HashSet<char>(EnglishWord.ToLower()); 
        var otherWordSet = new HashSet<char>(otherWord.ToLower()); 
        return translationEntryWordSet.Intersect(otherWordSet).Count();
    }

}