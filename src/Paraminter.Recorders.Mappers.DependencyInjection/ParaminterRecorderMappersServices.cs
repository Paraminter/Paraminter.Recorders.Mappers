namespace Paraminter.Recorders.Mappers;

using Microsoft.Extensions.DependencyInjection;

using System;

/// <summary>Allows the services provided by <i>Paraminter.Recorders.Mappers</i> to be registered with <see cref="IServiceCollection"/>.</summary>
public static class ParaminterRecorderMappersServices
{
    /// <summary>Registers the services provided by <i>Paraminter.Recorders.Mappers</i> with the provided <see cref="IServiceCollection"/>.</summary>
    /// <param name="services">The <see cref="IServiceCollection"/> with which services are registered.</param>
    /// <returns>The provided <see cref="IServiceCollection"/>, so that calls can be chained.</returns>
    public static IServiceCollection AddParaminterRecorderMappers(
        this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddTransient<IArgumentDataRecorderFactory, ArgumentDataRecorderFactory>();
        services.AddTransient<IArgumentExistenceRecorderFactory, ArgumentExistenceRecorderFactory>();

        services.AddTransient<IMappedArgumentDataRecorderFactoryProvider, MappedArgumentDataRecorderFactoryProvider>();
        services.AddTransient<IBoolDelegateMappedArgumentDataRecorderFactory, BoolDelegateMappedArgumentDataRecorderFactory>();
        services.AddTransient<IVoidDelegateMappedArgumentDataRecorderFactory, VoidDelegateMappedArgumentDataRecorderFactory>();

        services.AddTransient<IMappedArgumentExistenceRecorderFactoryProvider, MappedArgumentExistenceRecorderFactoryProvider>();
        services.AddTransient<IBoolDelegateMappedArgumentExistenceRecorderFactory, BoolDelegateMappedArgumentExistenceRecorderFactory>();
        services.AddTransient<IVoidDelegateMappedArgumentExistenceRecorderFactory, VoidDelegateMappedArgumentExistenceRecorderFactory>();

        return services;
    }
}
