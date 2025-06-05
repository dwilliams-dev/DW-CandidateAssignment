namespace Calastone.TextFilter.Application.Services.Filters;

public class ShortWordTextFilterStrategy : TextFilterStrategyBase {
    private const int MinLength = 3; // Could be retrieved from configuration

    // In the interests of preserving whitespace, I have made the decision to ignore whitespace "words"
    protected override Func<string, bool> WordShouldBeFiltered => word =>
        IsWord(word) && word.Length < MinLength;
}