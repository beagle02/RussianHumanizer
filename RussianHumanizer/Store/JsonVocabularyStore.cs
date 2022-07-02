using System.Text.Json;

namespace RussianHumanizer.Store;

public class JsonVocabularyStore : VocabularyStore
{
    private readonly string _filename;

    public JsonVocabularyStore(string filename)
    {
        _filename = filename;
    }

    protected override async Task SaveData(IEnumerable<RussianWordDto> words)
    {
        await using var writer = File.Create(_filename);
        await JsonSerializer.SerializeAsync(writer, words);
    }

    protected override async Task<IEnumerable<RussianWordDto>?> LoadData()
    {
        await using var reader = File.OpenRead(_filename);
        return await JsonSerializer.DeserializeAsync<IEnumerable<RussianWordDto>>(reader);
    }
}