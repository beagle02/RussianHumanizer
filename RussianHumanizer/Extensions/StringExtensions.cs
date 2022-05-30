using System.Globalization;
using Humanizer;
using RussianHumanizer.GrammaticalNumber;
using RussianHumanizer.Inflections;

namespace RussianHumanizer.Extensions;

public static class StringExtensions
{
    public static string ToRussianQuantity(this string word, long value,
        ShowQuantityAs showQuantityAs = ShowQuantityAs.Numeric, WordForm wordForm = WordForm.Normal,
        string? format = null, CultureInfo? formatProvider = null)
    {
        var (plurality, @case) = GetWordProperty(value);
        var response = RussianVocabularies.Default.Word(word, wordForm, plurality, @case);

        return showQuantityAs switch
        {
            ShowQuantityAs.None => response,
            ShowQuantityAs.Numeric => $"{value.ToString(format, formatProvider)} {response}",
            ShowQuantityAs.Words => $"{value.ToWords(formatProvider)} {response}",
            _ => throw new ArgumentOutOfRangeException(nameof(showQuantityAs), showQuantityAs, null)
        };
    }

    public static (Plurality plurality, RussianCase @case) GetWordProperty(long value)
    {
        return RussianNumberDetector.Detect(value) switch
        {
            RussianNumber.Singular => (Plurality.Singular, RussianCase.Nominative),
            RussianNumber.Small => (Plurality.Singular, RussianCase.Genetive),
            RussianNumber.Plural => (Plurality.Plural, RussianCase.Genetive),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}