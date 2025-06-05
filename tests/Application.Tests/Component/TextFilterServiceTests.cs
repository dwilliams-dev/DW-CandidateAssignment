using Calastone.TextFilter.Application.Services.TextProcessing;
using Calastone.TextFilter.Application.Tests.Fixtures;
using FluentAssertions;

namespace Calastone.TextFilter.Application.Tests.Component;

public class TextFilterServiceTests(AppFixture appFixture) : IClassFixture<AppFixture> {
    private readonly ITextFilterService _textFilterService = appFixture.GetService<ITextFilterService>();

    [Fact]
    public void should_apply_all_three_filters_correctly() {
        // Arrange
        const string text =
            "Alice was beginning to get very tired of sitting by her sister on the bank, and of having nothing to do: once or twice";

        const string expected = "beginning, and: once";

        // Act
        var filteredText = _textFilterService.Process(text);

        // Assert
        filteredText.Should().Be(expected);
    }

    [Fact]
    public void should_apply_all_filters_correctly_where_all_words_surrounded_by_punctuation_are_removed() {
        // Arrange
        const string text =
            "she had peeped into the book her sister was reading, but it had no pictures or conversations in it, 'and what is the";

        const string expected = "she reading, , 'and";

        // Act
        var filteredText = _textFilterService.Process(text);

        // Assert
        filteredText.Should().Be(expected);
    }

    [Fact]
    public void should_trim_trailing_spaces_after_punctuation_found_at_the_end_of_a_line() {
        // Arrange
        const string text = "She took down a jar from one of the shelves as she passed; it was ";

        const string expected = "She one shelves she passed;";

        // Act
        var filteredText = _textFilterService.Process(text);

        // Assert
        filteredText.Should().Be(expected);
    }
}