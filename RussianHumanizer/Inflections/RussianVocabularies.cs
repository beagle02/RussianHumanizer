using Humanizer;

namespace RussianHumanizer.Inflections;

public static class RussianVocabularies
{
    public static RussianVocabulary Default => Instance.Value;

    private static readonly Lazy<RussianVocabulary> Instance;

    static RussianVocabularies()
    {
        Instance = new Lazy<RussianVocabulary>(BuildDefault, LazyThreadSafetyMode.PublicationOnly);
    }

    private static RussianVocabulary BuildDefault()
    {
        var dic = new RussianVocabulary();

        dic.Register(GrammaticalGender.Masculine,
            new[] { "рубль", "рубля", "рублю", "рубль", "рублём", "рубле" },
            new[] { "рубли", "рублей", "рублям", "рубли", "рублями", "рублях" },
            "руб.");
        dic.Register(GrammaticalGender.Feminine,
            new[] { "копейка", "копейки", "копейке", "копейку", "копейкой", "копейке" },
            new[] { "копейки", "копеек", "копейкам", "копейки", "копейками", "копейках" },
            "коп.");

        return dic;
    }
}