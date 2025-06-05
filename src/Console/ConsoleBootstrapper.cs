using Calastone.TextFilter.Application.DependencyInjection;
using Calastone.TextFilter.Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Calastone.TextFilter.Console;

public static class ConsoleBootstrapper {
    public static ServiceProvider ServiceProvider() => new ServiceCollection()
        .AddFileProcessing()
        .AddTextFiltering()
        .BuildServiceProvider();
}