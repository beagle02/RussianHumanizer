using System.Globalization;
using Humanizer;

namespace RussianHumanizer.Extensions;

public static class DecimalExtensions
{
    public static string ToRussianMoney(this decimal money, ShowQuantityAs rubleShowQuantityAs = ShowQuantityAs.Numeric,
        ShowQuantityAs pennyShowQuantityAs = ShowQuantityAs.Numeric, WordForm rubleWordForm = WordForm.Normal,
        WordForm pennyWordForm = WordForm.Normal, string? rubleFormat = null, string? pennyFormat = null,
        CultureInfo? formatProvider = null)
    {
        var rubles = (int)money;
        var success = TryGetPenny(money, out var penny);
        if (!success)
            throw new ArgumentException("Должно быть 2 знака после запятой", nameof(money));

        var p1 = "рубль".ToRussianQuantity(rubles, rubleShowQuantityAs, rubleWordForm, rubleFormat, formatProvider);
        var p2 = "копейка".ToRussianQuantity(penny, pennyShowQuantityAs, pennyWordForm, pennyFormat, formatProvider);

        return $"{p1} {p2}";
    }

    public static bool TryGetPenny(decimal value, out int pennies)
    {
        var penniesValue = (value - (int)value) * 100;
        var p = (int)penniesValue;
        var isSuccess = p == 0 || penniesValue % p == 0;
        pennies = isSuccess ? p : 0;
        return isSuccess;
    }
}