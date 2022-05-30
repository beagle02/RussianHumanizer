using Humanizer;

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
        return wordForm == WordForm.Abbreviation
            ? _wordDic[word].Abbreviation
            : _wordDic[word].GetWord(plurality, @case);
    }
}