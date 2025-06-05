using Calastone.TextFilter.Application.Services.Filters;
using Calastone.TextFilter.Application.Services.TextProcessing;
using Microsoft.Extensions.DependencyInjection;

namespace Calastone.TextFilter.Application.DependencyInjection;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddTextFiltering(this IServiceCollection services) => services
        .AddScoped<ITextFilterService, TextFilterService>()
        .AddTextFilterStrategies();

    private static IServiceCollection AddTextFilterStrategies(this IServiceCollection services) => services
        .AddScoped<ITextFilterStrategy, MiddleVowelTextFilterStrategy>()
        .AddScoped<ITextFilterStrategy, ShortWordTextFilterStrategy>()
        .AddScoped<ITextFilterStrategy, SpecificCharacterTextFilterStrategy>();
}