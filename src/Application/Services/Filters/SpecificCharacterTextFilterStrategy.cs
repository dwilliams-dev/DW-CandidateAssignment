namespace Calastone.TextFilter.Application.Services.Filters;

public class SpecificCharacterTextFilterStrategy : TextFilterStrategyBase {
    private const char CharacterToFilter = 't';

    protected override Func<string, bool> WordShouldBeFiltered => word =>
        word.Contains(CharacterToFilter, StringComparison.CurrentCultureIgnoreCase);
}