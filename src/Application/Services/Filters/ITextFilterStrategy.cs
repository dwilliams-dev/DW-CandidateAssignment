namespace Calastone.TextFilter.Application.Services.Filters;

public interface ITextFilterStrategy {
    string FilterWords(string text);
}