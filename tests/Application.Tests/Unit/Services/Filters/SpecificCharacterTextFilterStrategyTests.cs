using Calastone.TextFilter.Application.Services.Filters;
using FluentAssertions;

namespace Calastone.TextFilter.Application.Tests.Unit.Services.Filters;

public class SpecificCharacterTextFilterStrategyTests {
    private readonly SpecificCharacterTextFilterStrategy _specificCharacterTextFilterStrategy = new();

    [Fact]
    public void should_return_no_changes_when_the_character_is_not_present() {
        // Arrange
        const string source = "Officiis quia qui.";

        // Act
        var filteredText = _specificCharacterTextFilterStrategy.FilterWords(source);

        // Assert
        filteredText.Should().Be(source);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void should_return_empty_string_for_empty_text(string emptyText) {
        // Act
        var filteredText = _specificCharacterTextFilterStrategy.FilterWords(emptyText);

        // Assert
        filteredText.Should().Be(emptyText);
    }

    [Theory]
    [MemberData(nameof(LetterTData))]
    public void should_filter_words_containing_the_letter_t(string source, string expected) {
        // Act
        var filteredText = _specificCharacterTextFilterStrategy.FilterWords(source);

        // Assert
        filteredText.Should().Be(expected);
    }

    [Fact]
    public void should_handle_text_ending_with_punctuation() {
        // Arrange
        const string text = "so managed to put it into one of the cupboards as she fell past it.";
        const string expected = "so managed one of cupboards as she fell.";

        // Act
        var filteredText = _specificCharacterTextFilterStrategy.FilterWords(text);

        // Assert
        filteredText.Should().Be(expected);
    }

    [Fact]
    public void should_return_an_empty_string_when_all_words_are_filtered() {
        // Arrange
        const string text = "to put it";

        // Act
        var filteredText = _specificCharacterTextFilterStrategy.FilterWords(text);

        // Assert
        filteredText.Should().BeEmpty();
    }

    [Fact]
    public void should_preserve_punctuation_when_all_words_are_filtered() {
        // Arrange
        const string text = "to put it.";
        const string expected = ".";

        // Act
        var filteredText = _specificCharacterTextFilterStrategy.FilterWords(text);

        // Assert
        filteredText.Should().Be(expected);
    }

    [Fact]
    public void should_handle_words_with_leading_spaces_correctly() {
        // Arrange
        const string text = "so managed to put it";
        const string expected = "so managed";

        // Act
        var filteredText = _specificCharacterTextFilterStrategy.FilterWords(text);

        // Assert
        filteredText.Should().Be(expected);
    }

    [Fact]
    public void should_preserve_existing_whitespace() {
        // Arrange
        const string text = "'Oh dear! Oh dear! I shall be   late!'";
        const string expected = "'Oh dear! Oh dear! I shall be   !'";

        // Act
        var filteredText = _specificCharacterTextFilterStrategy.FilterWords(text);

        // Assert
        filteredText.Should().Be(expected);
    }

    public static IEnumerable<TheoryDataRow<string, string>> LetterTData() {
        yield return new TheoryDataRow<string, string>(
            "Vel rerum et qui aperiam",
            "Vel rerum qui aperiam");

        yield return new TheoryDataRow<string, string>(
            "Maxime libero velit rem quisquam id sint qui aut.",
            "Maxime libero rem quisquam id qui.");

        yield return new TheoryDataRow<string, string>(
            "Alice was beginning to get very tired of sitting by her sister on the bank",
            "Alice was beginning very of by her on bank");
    }
}