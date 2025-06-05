using System.Text.RegularExpressions;

namespace Calastone.TextFilter.Application.Services.Filters;

public class MiddleVowelTextFilterStrategy : TextFilterStrategyBase {
    protected override Func<string, bool> WordShouldBeFiltered =>
        word => {
            if (!IsWord(word)) {
                return false;
            }

            var middleOfWord = GetMiddleOfWord(word);

            return Regex.Match(middleOfWord, "[aeiouAEIOU]").Success;
        };

    private static string GetMiddleOfWord(string word) {
        var lengthIsEven = word.Length % 2 == 0;

        var middleStart = word.Length / 2;
        var startIndex = lengthIsEven ? middleStart - 1 : middleStart;

        var charsToTake = lengthIsEven ? 2 : 1;

        return word.Substring(startIndex, charsToTake);
    }
}