using Calastone.TextFilter.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Calastone.TextFilter.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddFileProcessing(this IServiceCollection services) =>
        services.AddScoped<ITextInputProcessor, FileProcessor>();
}