using Humanizer;

namespace RussianHumanizer.Store;

public class RussianWordDto
{
    public GrammaticalGender Gender { get; set; }
    public string? Abbreviation { get; set; }
    public string[] SingularByCases { get; set; } = null!;
    public string[] PluralByCases { get; set; } = null!;
}