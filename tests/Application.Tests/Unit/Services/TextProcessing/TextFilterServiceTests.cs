using AutoFixture;
using Calastone.TextFilter.Application.Services.Filters;
using Calastone.TextFilter.Application.Services.TextProcessing;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

// ReSharper disable NullableWarningSuppressionIsUsed

namespace Calastone.TextFilter.Application.Tests.Unit.Services.TextProcessing;

public class TextFilterServiceTests {
    private readonly Fixture _fixture = new();

    private TextFilterService _textFilterService;
    private readonly ITextFilterStrategy _filterStrategyStrategy1 = Substitute.For<ITextFilterStrategy>();
    private readonly ITextFilterStrategy _filterStrategyStrategy2 = Substitute.For<ITextFilterStrategy>();
    private readonly ITextFilterStrategy _filterStrategyStrategy3 = Substitute.For<ITextFilterStrategy>();

    private readonly string _textInput;
    private readonly string _filter1Result;
    private readonly string _filter2Result;
    private readonly string _filter3Result;

    public TextFilterServiceTests() {
        // Set up test data
        _textInput = _fixture.Create<string>();
        _filter1Result = _fixture.Create<string>();
        _filter2Result = _fixture.Create<string>();
        _filter3Result = _fixture.Create<string>();

        // Set up mocks
        _filterStrategyStrategy1.FilterWords(_textInput).Returns(_filter1Result);
        _filterStrategyStrategy2.FilterWords(_filter1Result).Returns(_filter2Result);
        _filterStrategyStrategy3.FilterWords(_filter2Result).Returns(_filter3Result);

        // Set up sut
        _textFilterService = new TextFilterService([
            _filterStrategyStrategy1, _filterStrategyStrategy2, _filterStrategyStrategy3
        ]);
    }

    [Fact]
    public void should_apply_single_text_filter() {
        // Arrange
        _textFilterService = new TextFilterService([_filterStrategyStrategy1]);

        // Act
        _textFilterService.Process(_textInput);

        // Assert
        _filterStrategyStrategy1.Received(Quantity.Exactly(1)).FilterWords(_textInput);
    }

    [Fact]
    public void should_apply_multiple_text_filters() {
        // Act
        _textFilterService.Process(_textInput);

        // Assert
        _filterStrategyStrategy1.Received(Quantity.Exactly(1)).FilterWords(_textInput);
        _filterStrategyStrategy2.Received(Quantity.Exactly(1)).FilterWords(_filter1Result);
        _filterStrategyStrategy3.Received(Quantity.Exactly(1)).FilterWords(_filter2Result);
    }

    [Fact]
    public void should_return_final_filtered_result() {
        // Act
        var result = _textFilterService.Process(_textInput);

        // Assert
        result.Should().Be(_filter3Result);
    }
}