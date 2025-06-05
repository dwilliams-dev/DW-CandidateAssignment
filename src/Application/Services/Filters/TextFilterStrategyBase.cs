using System.Text.RegularExpressions;

namespace Calastone.TextFilter.Application.Services.Filters;

public abstract class TextFilterStrategyBase : ITextFilterStrategy {
    protected abstract Func<string, bool> WordShouldBeFiltered { get; }

    public string FilterWords(string text) {
        if (string.IsNullOrWhiteSpace(text)) {
            return text;
        }

        //var filteredTextBuilder = new StringBuilder();
        var results = new List<string>();

        var words = SplitIntoWords(text);

        for (var index = 0; index < words.Length; index++) {
            index = ProcessCurrentWord(words, index, results);
        }

        return string.Concat(results).TrimEnd();
    }

    private int ProcessCurrentWord(string[] words, int index, List<string> resultList) {
        var currentWord = words[index];
        var isFinalWord = index >= words.Length - 1;
        var previousWord = resultList.LastOrDefault();
        var nextWord = !isFinalWord ? words[index + 1] : null;
        var nextIndex = index;

        var ignoringLeadingSpace = IgnoringSingleLeadingSpace(currentWord, nextWord, previousWord);

        if (!ignoringLeadingSpace && !WordShouldBeFiltered(currentWord)) {
            resultList.Add(currentWord);
        }
        else if (resultList.Count == 0) {
            // If no words have been included yet, and the next word is a space, then skip over it
            if (nextWord == " ") {
                nextIndex++;
            }
        }

        return nextIndex;
    }

    /// <summary>
    /// Will return true if the current "word" is a single space and should be ignored.
    /// This is the case if the previous word ends with a space, i.e.: punctuation with a trailing space,
    /// or if the next word is to be filtered.
    /// </summary>
    private bool IgnoringSingleLeadingSpace(string currentWord, string? nextWord, string? previousWord) {
        if (currentWord != " ") return false;

        return previousWord is not null && previousWord.EndsWith(' ') ||
               nextWord is not null && WordShouldBeFiltered(nextWord);
    }

    private static string[] SplitIntoWords(string text) {
        const string wordBoundaryPattern = "\\b";

        var words = Regex.Split(text, wordBoundaryPattern);

        // The first entry is an empty string, unless the text starts with a punctuation character
        return words[0] == string.Empty ? words[1..] : words;
    }

    protected static bool IsWord(string word) => Regex.Matches(word, "[a-zA-Z]+").Any();
}