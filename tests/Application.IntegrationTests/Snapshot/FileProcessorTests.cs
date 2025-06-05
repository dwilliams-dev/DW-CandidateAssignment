using Calastone.TextFilter.Application.Contracts;
using Calastone.TextFilter.Application.IntegrationTests.Fixtures;

// ReSharper disable NullableWarningSuppressionIsUsed

namespace Calastone.TextFilter.Application.IntegrationTests.Snapshot;

/// <summary>
/// This test uses the Verify library to verify the output of the file processor.
/// See here for details: https://github.com/VerifyTests/Verify
/// </summary>
public class FileProcessorTests : IClassFixture<AppFixture> {
    private readonly VerifySettings _verifySettings = new();

    private const string SampleDirectoryPath = @"Snapshot\SampleFiles";
    private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();
    private static readonly string SampleFilesDirectory = $@"{CurrentDirectory}\{SampleDirectoryPath}";

    private readonly ITextInputProcessor _fileProcessor;

    public FileProcessorTests(AppFixture appFixture) {
        _verifySettings.UseDirectory("Snapshots");

        _fileProcessor = appFixture.GetService<ITextInputProcessor>();
    }

    [Fact]
    public async Task should_filter_sample_file_correctly() {
        // Arrange
        var file = Path.Combine(SampleFilesDirectory, "AliceInWonderland.txt");

        // Act
        var processTextInput = _fileProcessor.ProcessTextInput(file);

        // Assert
        await Verify(processTextInput.ToList(), _verifySettings);
    }
}