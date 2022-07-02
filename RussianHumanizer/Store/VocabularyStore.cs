using Humanizer;

namespace RussianHumanizer.Store;

public abstract class VocabularyStore : IVocabularyStore
{
    protected abstract Task SaveData(IEnumerable<RussianWordDto> words);
    protected abstract Task<IEnumerable<RussianWordDto>?> LoadData();

    public Task Save(IEnumerable<RussianWord> words)
    {
        return SaveData(words.Select(ToDataTransferObject));
    }

    public async Task<IEnumerable<RussianWord>> Load()
    {
        var words = await LoadData();
        return words == null ? Array.Empty<RussianWord>() : words.Select(ToRussianWord);
    }

    protected static RussianWordDto ToDataTransferObject(RussianWord word)
    {
        var dto = new RussianWordDto
        {
            Abbreviation = word.Abbreviation,
            Gender = word.Gender,
            SingularByCases = new string[6],
            PluralByCases = new string[6]
        };

        var cases = Enum.GetValues<RussianCase>();

        var i = 0;
        foreach (var @case in cases)
            dto.SingularByCases[i++] = word.GetWord(Plurality.Singular, @case);

        i = 0;
        foreach (var @case in cases)
            dto.PluralByCases[i++] = word.GetWord(Plurality.Plural, @case);

        return dto;
    }

    protected static RussianWord ToRussianWord(RussianWordDto dto)
    {
        return new RussianWord(dto.Gender, dto.SingularByCases, dto.PluralByCases, dto.Abbreviation);
    }
}