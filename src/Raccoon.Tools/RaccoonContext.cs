using LiteDB;
using Microsoft.Extensions.DependencyInjection;

namespace Raccoon.Tools;

/// <summary>
///
/// </summary>
public class RaccoonContext
{
    private static IServiceCollection _serviceCollection;
    private static IServiceProvider _serviceProvider;

    public static RaccoonBuilder Create()
    {
        _serviceCollection = new ServiceCollection();

        return RaccoonBuilder.Create(_serviceCollection);
    }

    public static IServiceProvider Build(RaccoonBuilder builder)
    {
        return _serviceProvider = builder.Build();
    }

    public static T GetService<T>()
    {
        return _serviceProvider.GetRequiredService<T>();
    }

    public static AsyncServiceScope CreateAsyncScope()
    {
        return _serviceProvider.CreateAsyncScope();
    }

    public static T GetService<T>(Type type)
    {
        return (T)_serviceProvider.GetRequiredService(type);
    }

    public static object GetService(Type type)
    {
        return _serviceProvider.GetRequiredService(type);
    }

    public static HttpClient GetHttpClient()
    {
        var httpClientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();

        return httpClientFactory.CreateClient();
    }

    public static HttpClient GetHttpClient(string name)
    {
        var httpClientFactory = _serviceProvider.GetRequiredService<IHttpClientFactory>();

        return httpClientFactory.CreateClient(name);
    }

    public static ILiteDatabase LiteDatabase()
    {
        return _serviceProvider.GetRequiredService<ILiteDatabase>();
    }
}