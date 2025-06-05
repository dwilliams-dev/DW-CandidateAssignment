using Calastone.TextFilter.Application.Contracts;
using Calastone.TextFilter.Application.Services.TextProcessing;

namespace Calastone.TextFilter.Infrastructure;

public class FileProcessor(ITextFilterService textFilterService) : ITextInputProcessor {
    public IEnumerable<string> ProcessTextInput(string path) {
        var fileStream = new FileStream(path, FileMode.Open);
        var streamReader = new StreamReader(fileStream);

        string? line;
        while ((line = streamReader.ReadLine()) is not null) {
            var filteredLine = textFilterService.Process(line);
            yield return filteredLine;
        }
    }
}