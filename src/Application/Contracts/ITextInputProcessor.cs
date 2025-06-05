namespace Calastone.TextFilter.Application.Contracts;

public interface ITextInputProcessor {
    IEnumerable<string> ProcessTextInput(string path);
}