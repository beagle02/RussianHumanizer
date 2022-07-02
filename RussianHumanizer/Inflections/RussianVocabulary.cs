using Humanizer;
using RussianHumanizer.Store;

namespace RussianHumanizer.Inflections;

public class RussianVocabulary
{
    private readonly Dictionary<string, RussianWord> _wordDic = new ();

    public void Register(GrammaticalGender gender, string[] singularWords, string[] pluralWords,
        string? abbreviation = null)
    {
        var ruWord = new RussianWord(gender, singularWords, pluralWords, abbreviation);

        if (_wordDic.ContainsKey(ruWord.Word))
            _wordDic[ruWord.Word] = ruWord;
        else
            _wordDic.Add(ruWord.Word, ruWord);
    }

    public string Word(string word, WordForm wordForm, Plurality plurality = Plurality.Singular,
        RussianCase @case = RussianCase.Nominative)
    {
        if (!_wordDic.ContainsKey(word))
            return string.Empty;

        return wordForm == WordForm.Abbreviation
            ? _wordDic[word].Abbreviation
            : _wordDic[word].GetWord(plurality, @case);
    }

    public Task Save(IVocabularyStore store)
    {
        return store.Save(_wordDic.Values.AsEnumerable());
    }

    public async Task Load(IVocabularyStore store, bool clear = false)
    {
        if (clear)
            _wordDic.Clear();

        var loaded = await store.Load();
        foreach (var item in loaded)
            _wordDic.Add(item.Word, item);
    }
}