namespace Paraminter.Mappers;

using Microsoft.Extensions.DependencyInjection;

using System;

/// <summary>Allows the services provided by <i>Paraminter.Mappers</i> to be registered with <see cref="IServiceCollection"/>.</summary>
public static class ParaminterMappersServices
{
    /// <summary>Registers the services provided by <i>Paraminter.Mappers</i> with the provided <see cref="IServiceCollection"/>.</summary>
    /// <param name="services">The <see cref="IServiceCollection"/> with which services are registered.</param>
    /// <returns>The provided <see cref="IServiceCollection"/>, so that calls can be chained.</returns>
    public static IServiceCollection AddParaminterMappers(
        this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddTransient<IArgumentDataRecorderFactory, ArgumentDataRecorderFactory>();

        services.AddTransient<IMappedArgumentDataRecorderFactoryProvider, MappedArgumentDataRecorderFactoryProvider>();
        services.AddTransient<IBoolDelegateMappedArgumentDataRecorderFactory, BoolDelegateMappedArgumentDataRecorderFactory>();
        services.AddTransient<IVoidDelegateMappedArgumentDataRecorderFactory, VoidDelegateMappedArgumentDataRecorderFactory>();

        return services;
    }
}
