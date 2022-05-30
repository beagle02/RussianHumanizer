using Humanizer;

namespace RussianHumanizer;

public class RussianWord
{
    public GrammaticalGender Gender { get; }
    public string Word => SingularByCases[RussianCase.Nominative];
    public string Abbreviation { get; } = string.Empty;

    protected Dictionary<RussianCase, string> SingularByCases = new();
    protected Dictionary<RussianCase, string> PluralByCases = new();
    protected Dictionary<Plurality, Dictionary<RussianCase, string>> Vocabulary;

    public RussianWord()
    {
        Vocabulary = new Dictionary<Plurality, Dictionary<RussianCase, string>>
        {
            { Plurality.Singular, SingularByCases },
            { Plurality.Plural, PluralByCases }
        };
    }

    public RussianWord(GrammaticalGender gender, string singularWord, string pluralWord,
        string? abbreviation = null) : this()
    {
        Gender = gender;
        Abbreviation = abbreviation ?? string.Empty;

        foreach (var @case in Enum.GetValues<RussianCase>())
        {
            SingularByCases.Add(@case, singularWord);
            PluralByCases.Add(@case, pluralWord);
        }
    }

    public RussianWord(GrammaticalGender gender, string[] singularWords, string[] pluralWords,
        string? abbreviation = null) : this()
    {
        if (singularWords == null || singularWords.Length < 6)
            throw new ArgumentException("Array length should be 6", nameof(singularWords));

        if (pluralWords == null || pluralWords.Length < 6)
            throw new ArgumentException("Array length should be 6", nameof(pluralWords));

        Gender = gender;
        Abbreviation = abbreviation ?? string.Empty;

        var idx = 0;
        foreach (var @case in Enum.GetValues<RussianCase>())
        {
            SingularByCases.Add(@case, singularWords[idx]);
            PluralByCases.Add(@case, pluralWords[idx]);
            idx++;
        }
    }

    public void AddWord(Plurality plurality, RussianCase @case, string word)
    {
        Vocabulary[plurality][@case] = word;
    }

    public string GetWord(Plurality plurality, RussianCase @case)
    {
        return Vocabulary[plurality][@case];
    }
}