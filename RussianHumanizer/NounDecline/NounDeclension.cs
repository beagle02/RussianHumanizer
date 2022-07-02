using Humanizer;
using RussianHumanizer.Inflections;

namespace RussianHumanizer.NounDecline;

public static class NounDeclension
{
    private record NounDeclineRule(int CountToRemove, string ReplaceWith);

    public static char[] Vowels = { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' };

    public static string Decline(this Noun noun, RussianCase @case)
    {
        var result = RussianVocabularies.Default.Word(noun.Word, WordForm.Normal, Plurality.Singular, @case);
        return string.IsNullOrEmpty(result) ? noun.Word.DeclineNoun(noun.Gender, noun.Animation, @case) : result;
    }

    public static string DeclineNoun(this string word, GrammaticalGender gender, Animation animation, RussianCase @case)
    {
        var firstEnding = new[] { 'а', 'я' };
        var secondEnding = new[] { 'о', 'е' };

        var normalize = word.ToLower().Trim().Replace('ё', 'е');

        var (prevChar, wordEnding) = GetEnding(normalize);
        var lastLetter = wordEnding.LastOrDefault();

        var rule = gender switch
        {
            GrammaticalGender.Masculine or GrammaticalGender.Feminine when firstEnding.Contains(lastLetter) =>
                SecondDeclension(wordEnding, prevChar, @case),
            GrammaticalGender.Masculine or GrammaticalGender.Neuter when (wordEnding == string.Empty ||
                                                                          secondEnding.Contains(lastLetter)) =>
                FirstDeclension(wordEnding, prevChar, gender, animation, @case),
            GrammaticalGender.Feminine when wordEnding == string.Empty => ThirdDeclension(@case),
            _ => throw new Exception($"Неизвестное склонение существительного '{word}'.")
        };

        return normalize.ApplyRule(rule);
    }

    private static string ApplyRule(this string word, NounDeclineRule rule)
    {
        var str = word[..^rule.CountToRemove];
        return $"{str}{rule.ReplaceWith}";
    }

    private static (char prevChar, string ending) GetEnding(string word)
    {
        var chars = word.ToCharArray();

        var idx = chars.Length - 1;
        while (idx >= 0)
        {
            if (!Vowels.Contains(chars[idx]))
                break;
            idx--;
        }

        if (idx == -1)
            return ('\0', word);

        return (chars[idx], idx + 1 == word.Length
            ? string.Empty
            : word[(idx + 1)..]);
    }

    private static NounDeclineRule FirstDeclension(string wordEnding, char prevChar,
        GrammaticalGender gender, Animation animation, RussianCase @case)
    {
        var len = wordEnding.Length;
        var hasSpecLastLetter = new[] { 'ь', 'й' }.Contains(prevChar);
        var countToRemove = len == 0 && !hasSpecLastLetter ? 0 : 1;

        string Genetive()
        {
            string replaceWith;
            if (gender == GrammaticalGender.Neuter)
                replaceWith = wordEnding == "е" ? "я" : "а";
            else
                replaceWith = hasSpecLastLetter ? "я" : "а";
            return replaceWith;
        }

        string Dative()
        {
            string replaceWith;
            if (gender == GrammaticalGender.Neuter)
                replaceWith = wordEnding == "е" ? "ю" : "у";
            else
                replaceWith = hasSpecLastLetter ? "ю" : "у";
            return replaceWith;
        }

        string Accusative()
        {
            string replaceWith;
            if (gender == GrammaticalGender.Neuter)
                replaceWith = wordEnding == "е" ? "я" : "а";
            else
                replaceWith = hasSpecLastLetter ? "я" : "а";
            return replaceWith;
        }

        string Instrumental()
        {
            string replaceWith;
            if (gender == GrammaticalGender.Neuter)
                replaceWith = wordEnding == "е" ? "ем" : "ом";
            else
                replaceWith = hasSpecLastLetter ? "ем" : "ом";
            return replaceWith;
        }

        return @case switch
        {
            RussianCase.Nominative => new NounDeclineRule(0, string.Empty),
            RussianCase.Genetive => new NounDeclineRule(countToRemove, Genetive()),
            RussianCase.Dative => new NounDeclineRule(countToRemove, Dative()),
            RussianCase.Accusative => (animation == Animation.Inanimated ||
                                       (animation == Animation.Animated && len != 0))
                ? new NounDeclineRule(0, string.Empty)
                : new NounDeclineRule(countToRemove, Accusative()),
            RussianCase.Instrumental => new NounDeclineRule(countToRemove, Instrumental()),
            RussianCase.Prepositional => new NounDeclineRule(countToRemove, "е"),
            _ => throw new ArgumentOutOfRangeException(nameof(@case), @case, null)
        };
    }

    private static NounDeclineRule SecondDeclension(string wordEnding, char prevChar, RussianCase @case)
    {
        var consonants = new[] { 'г', 'к', 'х', 'ж', 'ш', 'щ', 'ч' };
        var consonants2 = new[] { 'ж', 'ш', 'щ', 'ч', 'ц' };

        string Genetive()
        {
            var replaceWith = wordEnding == "а" ? "ы" : "и";
            if (consonants.Contains(prevChar))
                replaceWith = "и";
            return replaceWith;
        }

        string Instrumental()
        {
            var replaceWith = wordEnding == "а" ? "ой" : "ей";
            if (consonants2.Contains(prevChar))
                replaceWith = "ей";
            return replaceWith;
        }

        return @case switch
        {
            RussianCase.Nominative => new NounDeclineRule(0, string.Empty),
            RussianCase.Genetive => new NounDeclineRule(1, Genetive()),
            RussianCase.Dative => new NounDeclineRule(1, wordEnding == "ия" ? "и" : "е"),
            RussianCase.Accusative => new NounDeclineRule(1, wordEnding == "а" ? "у" : "ю"),
            RussianCase.Instrumental => new NounDeclineRule(1, Instrumental()),
            RussianCase.Prepositional => new NounDeclineRule(1, wordEnding == "ия" ? "и" : "е"),
            _ => throw new ArgumentOutOfRangeException(nameof(@case), @case, null)
        };
    }

    private static NounDeclineRule ThirdDeclension(RussianCase @case)
    {
        return @case switch
        {
            RussianCase.Nominative => new NounDeclineRule(0, string.Empty),
            RussianCase.Genetive => new NounDeclineRule(1, "и"),
            RussianCase.Dative => new NounDeclineRule(1, "и"),
            RussianCase.Accusative => new NounDeclineRule(0, string.Empty),
            RussianCase.Instrumental => new NounDeclineRule(0, "ю"),
            RussianCase.Prepositional => new NounDeclineRule(1, "и"),
            _ => throw new ArgumentOutOfRangeException(nameof(@case), @case, null)
        };
    }
}