using Humanizer;
using RussianHumanizer;
using RussianHumanizer.Inflections;
using RussianHumanizer.NounDecline;
using RussianHumanizer.Store;

RussianVocabularies.Default.Register(GrammaticalGender.Masculine,
    new[] { "день", "дня", "дню", "день", "днем", "дне" },
    new[] { "дни", "дней", "дням", "дни", "днями", "днях" });

var store = new JsonVocabularyStore("vocabulary.json");
await RussianVocabularies.Default.Save(store);
await RussianVocabularies.Default.Load(store, true);

var words = new[]
{
    new Noun("дирижёр", GrammaticalGender.Masculine, Animation.Animated),
    new Noun("майор", GrammaticalGender.Masculine, Animation.Animated),
    new Noun("полковник", GrammaticalGender.Masculine, Animation.Animated),
    new Noun("шар", GrammaticalGender.Masculine, Animation.Inanimated),
    new Noun("рубль", GrammaticalGender.Masculine, Animation.Inanimated),
    new Noun("копейка", GrammaticalGender.Feminine, Animation.Inanimated),
    new Noun("кулебяка", GrammaticalGender.Feminine, Animation.Inanimated),
    new Noun("день", GrammaticalGender.Masculine, Animation.Inanimated),
};

foreach(var word in words)
    ShowWordDeclines(word);

void ShowWordDeclines(Noun word)
{
    Console.WriteLine();
    Console.WriteLine($"{word.Decline(RussianCase.Nominative)}");
    Console.WriteLine($"без {word.Decline(RussianCase.Genetive)}");
    Console.WriteLine($"дать {word.Decline(RussianCase.Dative)}");
    Console.WriteLine($"винить {word.Decline(RussianCase.Accusative)}");
    Console.WriteLine($"гордиться {word.Decline(RussianCase.Instrumental)}");
    Console.WriteLine($"говорить о {word.Decline(RussianCase.Prepositional)}");
}