namespace RussianHumanizer.GrammaticalNumber;

public static class RussianNumberDetector
{
    public static RussianNumber Detect(long number)
    {
        var tens = number % 100 / 10;
        if (tens == 1) return RussianNumber.Plural;

        var unity = number % 10;
        return unity switch
        {
            // 1, 21, 31, 41 ... 91, 101, 121 ...
            1 => RussianNumber.Singular,
            // 2, 3, 4, 22, 23, 24 ...
            > 1 and < 5 => RussianNumber.Small,
            _ => RussianNumber.Plural
        };
    }
}