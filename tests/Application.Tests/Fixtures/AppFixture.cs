using Calastone.TextFilter.Application.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Calastone.TextFilter.Application.Tests.Fixtures;

public class AppFixture {
    private readonly ServiceProvider _serviceProvider = new ServiceCollection()
        .AddTextFiltering()
        .BuildServiceProvider();

    public T GetService<T>() where T : notnull => _serviceProvider.GetRequiredService<T>();
}