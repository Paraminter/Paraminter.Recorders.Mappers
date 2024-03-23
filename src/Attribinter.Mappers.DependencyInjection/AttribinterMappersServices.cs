namespace Attribinter.Mappers;

using Microsoft.Extensions.DependencyInjection;

using System;

/// <summary>Allows the services of <i>Attribinter.Mappers</i> to be registered with a <see cref="IServiceCollection"/>.</summary>
public static class AttribinterMappersServices
{
    /// <summary>Registers the services of <i>Attribinter.Mappers</i> with the provided <see cref="IServiceCollection"/>.</summary>
    /// <param name="services">The <see cref="IServiceCollection"/> with which services are registered.</param>
    /// <returns>The provided <see cref="IServiceCollection"/>, so that calls can be chained.</returns>
    public static IServiceCollection AddAttribinterMappers(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddSingleton<IArgumentRecorderFactory, ArgumentRecorderFactory>();

        services.AddSingleton<IMappedArgumentRecorderFactory, MappedArgumentRecorderFactory>();

        return services;
    }
}
