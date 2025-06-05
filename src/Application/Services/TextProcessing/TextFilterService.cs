using Calastone.TextFilter.Application.Services.Filters;

namespace Calastone.TextFilter.Application.Services.TextProcessing;

public class TextFilterService(IEnumerable<ITextFilterStrategy> filters) : ITextFilterService {
    public string Process(string text) {
        var result = text;

        foreach (var textFilter in filters) {
            result = textFilter.FilterWords(result);
        }

        return result;
    }
}