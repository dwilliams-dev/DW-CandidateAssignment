using Calastone.TextFilter.Application.Services.Filters;
using FluentAssertions;

namespace Calastone.TextFilter.Application.Tests.Unit.Services.Filters;

public class ShortWordTextFilterStrategyTests {
    private readonly ShortWordTextFilterStrategy _shortWordTextFilterStrategy = new();

    [Fact]
    public void should_return_no_changes_when_the_are_no_short_words() {
        // Arrange
        const string source = "Officiis quia qui.";

        // Act
        var filteredText = _shortWordTextFilterStrategy.FilterWords(source);

        // Assert
        filteredText.Should().Be(source);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void should_return_empty_string_for_empty_text(string emptyText) {
        // Act
        var filteredText = _shortWordTextFilterStrategy.FilterWords(emptyText);

        // Assert
        filteredText.Should().Be(emptyText);
    }

    [Theory]
    [MemberData(nameof(ShortWordData))]
    public void should_return_text_with_no_words_less_than_three_characters_long(string source, string expected) {
        // Act
        var filteredText = _shortWordTextFilterStrategy.FilterWords(source);

        // Assert
        filteredText.Should().Be(expected);
    }

    [Fact]
    public void should_preserve_punctuation_when_all_words_are_filtered() {
        // Arrange
        const string text = "to put it.";
        const string expected = "put.";

        // Act
        var filteredText = _shortWordTextFilterStrategy.FilterWords(text);

        // Assert
        filteredText.Should().Be(expected);
    }

    public static IEnumerable<TheoryDataRow<string, string>> ShortWordData() {
        yield return new TheoryDataRow<string, string>(
            "Vel rerum et qui aperiam.",
            "Vel rerum qui aperiam.");

        yield return new TheoryDataRow<string, string>(
            "Maxime libero velit rem quisquam id sint qui aut.",
            "Maxime libero velit rem quisquam sint qui aut.");

        yield return new TheoryDataRow<string, string>(
            "Alice was beginning to get very tired of sitting by her sister on the bank",
            "Alice was beginning get very tired sitting her sister the bank");
    }
}