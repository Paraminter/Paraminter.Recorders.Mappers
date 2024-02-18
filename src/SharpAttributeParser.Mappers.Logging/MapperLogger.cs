namespace SharpAttributeParser.Mappers.Logging;

using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;

using SharpAttributeParser.Mappers.Logging.MapperLoggerComponents;

using System;

/// <inheritdoc cref="IMapperLogger{TCategoryName}"/>
public sealed class MapperLogger<TCategoryName> : IMapperLogger<TCategoryName>
{
    private readonly ITypeParameterLogger TypeParameter;
    private readonly IConstructorParameterLogger ConstructorParameter;
    private readonly INamedParameterLogger NamedParameter;

    /// <summary>Instantiates a <see cref="MapperLogger{TCategoryName}"/>, handling logging for <see cref="IMapper"/>.</summary>
    /// <param name="logger">The logger used to log messages.</param>
    public MapperLogger(ILogger<TCategoryName> logger)
    {
        if (logger is null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        TypeParameter = new TypeParameterLogger(logger);
        ConstructorParameter = new ConstructorParameterLogger(logger);
        NamedParameter = new NamedParameterLogger(logger);
    }

    ITypeParameterLogger IMapperLogger.TypeParameter => TypeParameter;
    IConstructorParameterLogger IMapperLogger.ConstructorParameter => ConstructorParameter;
    INamedParameterLogger IMapperLogger.NamedParameter => NamedParameter;

    private sealed class TypeParameterLogger : ITypeParameterLogger
    {
        private readonly ILogger Logger;

        public TypeParameterLogger(ILogger logger)
        {
            Logger = logger;
        }

        IDisposable? ITypeParameterLogger.BeginScopeMappingTypeParameter<TRecorder>(ITypeParameterSymbol parameter)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            return ScopeDefinitions.MappingTypeParameter(Logger, parameter.Name, parameter.Ordinal);
        }
    }

    private sealed class ConstructorParameterLogger : IConstructorParameterLogger
    {
        private readonly ILogger Logger;

        public ConstructorParameterLogger(ILogger logger)
        {
            Logger = logger;
        }

        IDisposable? IConstructorParameterLogger.BeginScopeMappingConstructorParameter<TRecorder>(IParameterSymbol parameter)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            return ScopeDefinitions.MappingConstructorParameter(Logger, parameter.Name);
        }
    }

    private sealed class NamedParameterLogger : INamedParameterLogger
    {
        private readonly ILogger Logger;

        public NamedParameterLogger(ILogger logger)
        {
            Logger = logger;
        }

        IDisposable? INamedParameterLogger.BeginScopeMappingNamedParameter<TRecorder>(string parameterName)
        {
            if (parameterName is null)
            {
                throw new ArgumentNullException(nameof(parameterName));
            }

            return ScopeDefinitions.MappingNamedParameter(Logger, parameterName);
        }
    }

    private static class ScopeDefinitions
    {
        public static Func<ILogger, string, int, IDisposable?> MappingTypeParameter { get; }
        public static Func<ILogger, string, IDisposable?> MappingConstructorParameter { get; }
        public static Func<ILogger, string, IDisposable?> MappingNamedParameter { get; }

        static ScopeDefinitions()
        {
            MappingTypeParameter = LoggerMessage.DefineScope<string, int>("[TypeParameterName: {TypeParameterName}, TypeParameterIndex: {TypeParameterIndex}]");
            MappingConstructorParameter = LoggerMessage.DefineScope<string>("ConstructorParameterName: {ConstructorParameterName}");
            MappingNamedParameter = LoggerMessage.DefineScope<string>("NamedParameterName: {NamedParameterName}");
        }
    }
}
