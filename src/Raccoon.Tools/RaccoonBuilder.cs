using Microsoft.Extensions.DependencyInjection;

namespace Raccoon.Tools;

public class RaccoonBuilder(IServiceCollection service)
{
    public readonly IServiceCollection Service = service;

    public IServiceProvider Build()
    {
        return Service.BuildServiceProvider();
    }

    public static RaccoonBuilder Create(IServiceCollection serviceCollection)
    {
        return new RaccoonBuilder(serviceCollection);
    }

    public RaccoonBuilder AddSingleton<TService, TImplementation>()
        where TService : class where TImplementation : class, TService
    {
        Service.AddSingleton<TService, TImplementation>();
        return this;
    }

    public RaccoonBuilder AddSingleton<TService>(TService implementationInstance) where TService : class
    {
        Service.AddSingleton(implementationInstance);
        return this;
    }

    public RaccoonBuilder AddSingleton<TService>(Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        Service.AddSingleton(implementationFactory);
        return this;
    }

    public RaccoonBuilder AddSingleton<TService>() where TService : class
    {
        Service.AddSingleton<TService>();
        return this;
    }

    public RaccoonBuilder AddSingleton<TService, TImplementation>(ServiceLifetime lifetime)
        where TService : class where TImplementation : class, TService
    {
        Service.AddSingleton<TService, TImplementation>();
        return this;
    }

    public RaccoonBuilder AddSingleton<TService>(TService implementationInstance, ServiceLifetime lifetime)
        where TService : class
    {
        Service.AddSingleton(implementationInstance);
        return this;
    }

    public RaccoonBuilder AddSingleton<TService>(Func<IServiceProvider, TService> implementationFactory,
        ServiceLifetime lifetime) where TService : class
    {
        Service.AddSingleton(implementationFactory);
        return this;
    }

    public RaccoonBuilder AddSingleton<TService>(ServiceLifetime lifetime) where TService : class
    {
        Service.AddSingleton<TService>();
        return this;
    }

    public RaccoonBuilder AddTransient<TService, TImplementation>()
        where TService : class where TImplementation : class, TService
    {
        Service.AddTransient<TService, TImplementation>();
        return this;
    }

    public RaccoonBuilder AddTransient<TService>(Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        Service.AddTransient(implementationFactory);
        return this;
    }

    public RaccoonBuilder AddTransient<TService>(ServiceLifetime lifetime) where TService : class
    {
        Service.AddTransient<TService>();
        return this;
    }
}