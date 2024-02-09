namespace SharpAttributeParser.Mappers.Logging;

using Microsoft.Extensions.DependencyInjection;

using System;

/// <summary>Allows the services of <i>SharpAttributeParser.Mappers.Logging</i> to be registered with a <see cref="IServiceCollection"/>.</summary>
public static class SharpAttributeParserMappersLoggingServices
{
    /// <summary>Registers the services of <i>SharpAttributeParser.Mappers.Logging</i> with the provided <see cref="IServiceCollection"/>.</summary>
    /// <param name="services">The <see cref="IServiceCollection"/> with which services are registered.</param>
    /// <returns>The provided <see cref="IServiceCollection"/>, so that calls can be chained.</returns>
    public static IServiceCollection AddSharpAttributeParserMappersLogging(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddSingleton(typeof(IRecorderLogger<>), typeof(RecorderLogger<>));
        services.AddSingleton(typeof(ISemanticRecorderLogger<>), typeof(SemanticRecorderLogger<>));
        services.AddSingleton(typeof(ISyntacticRecorderLogger<>), typeof(SyntacticRecorderLogger<>));

        services.AddSingleton<IRecorderLoggerFactory, RecorderLoggerFactory>();
        services.AddSingleton<ISemanticRecorderLoggerFactory, SemanticRecorderLoggerFactory>();
        services.AddSingleton<ISyntacticRecorderLoggerFactory, SyntacticRecorderLoggerFactory>();

        services.AddSingleton(typeof(IMapperLogger<>), typeof(MapperLogger<>));
        services.AddSingleton(typeof(ISemanticMapperLogger<>), typeof(SemanticMapperLogger<>));
        services.AddSingleton(typeof(ISyntacticMapperLogger<>), typeof(SyntacticMapperLogger<>));

        return services;
    }
}
