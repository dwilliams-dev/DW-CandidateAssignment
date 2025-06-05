using Calastone.TextFilter.Application.Services.Filters;
using FluentAssertions;

namespace Calastone.TextFilter.Application.Tests.Unit.Services.Filters;

public class MiddleVowelTextFilterStrategyTests {
    private readonly MiddleVowelTextFilterStrategy _middleVowelTextFilterStrategy = new();

    [Fact]
    public void should_return_no_changes_when_the_character_is_not_present() {
        // Arrange
        const string text = "rather the";

        // Act
        var filteredText = _middleVowelTextFilterStrategy.FilterWords(text);

        // Assert
        filteredText.Should().Be(text);
    }

    [Theory]
    [InlineData("clean")]
    [InlineData("what")]
    [InlineData("currently")]
    public void should_return_empty_string_when_a_single_word_is_filtered(string text) {
        // Act
        var filteredText = _middleVowelTextFilterStrategy.FilterWords(text);

        // Assert
        filteredText.Should().BeEmpty();
    }

    [Fact]
    public void should_return_empty_string_when_all_when_words_are_filtered() {
        // Arrange
        const string text = "clean what currently";

        // Act
        var filteredText = _middleVowelTextFilterStrategy.FilterWords(text);

        // Assert
        filteredText.Should().BeEmpty();
    }

    [Fact]
    public void should_handle_consecutive_filtered_words_at_the_start() {
        // Arrange
        const string text = "Alice was beginning to get very tired";
        const string expected = "beginning tired";

        // Act
        var filteredText = _middleVowelTextFilterStrategy.FilterWords(text);

        // Assert
        filteredText.Should().Be(expected);
    }
}