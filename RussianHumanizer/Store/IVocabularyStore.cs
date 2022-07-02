namespace RussianHumanizer.Store;

public interface IVocabularyStore
{
    Task Save(IEnumerable<RussianWord> words);
    Task<IEnumerable<RussianWord>> Load();
}