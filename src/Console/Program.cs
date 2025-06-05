using Calastone.TextFilter.Application.Contracts;
using Calastone.TextFilter.Console;
using Microsoft.Extensions.DependencyInjection;

var textInputProcessor = BuildTextInputProcessor();

Console.WriteLine("Welcome to the Calastone Text Transformer");

while (true) {
    Console.WriteLine();
    Console.WriteLine("Please choose an option:");
    Console.WriteLine("1) Process a text file");
    Console.WriteLine("2) Exit the program");
    var choice = Console.ReadLine();
    switch (choice) {
        case "1":
            ProcessFile();
            break;

        case "2":
            Console.WriteLine("Exiting the program. Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}

void ProcessFile() {
    try {
        Console.WriteLine("Please enter the path of a text file to process:");
        var filepath = Console.ReadLine();
        if (filepath == null) Console.WriteLine("Invalid file path. Please try again.");
        else {
            var processFileResult = textInputProcessor.ProcessTextInput(filepath);

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("The filtered contents of the file:");
            Console.WriteLine();
            Console.ResetColor();

            foreach (var line in processFileResult) {
                Console.WriteLine(line);
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Filtering complete.");
            Console.ResetColor();
        }
    }
    catch (Exception exception) {
        Console.WriteLine();
        Console.WriteLine("There was an error processing the file. Please try again:");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(exception.Message);
        Console.ResetColor();
        Console.WriteLine();
    }
}

ITextInputProcessor BuildTextInputProcessor() =>
    ConsoleBootstrapper.ServiceProvider().GetRequiredService<ITextInputProcessor>();