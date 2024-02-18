namespace SharpAttributeParser.Mappers.Logging;

using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;

using SharpAttributeParser.Mappers.Logging.SyntacticMapperLoggerComponents;

using System;

/// <inheritdoc cref="ISyntacticMapperLogger{TCategoryName}"/>
public sealed class SyntacticMapperLogger<TCategoryName> : ISyntacticMapperLogger<TCategoryName>
{
    private readonly ITypeParameterLogger TypeParameter;
    private readonly IConstructorParameterLogger ConstructorParameter;
    private readonly INamedParameterLogger NamedParameter;

    /// <summary>Instantiates a <see cref="SyntacticMapperLogger{TCategoryName}"/>, handling logging for <see cref="ISyntacticMapper{TRecord}"/>.</summary>
    /// <param name="logger">The logger used to log messages.</param>
    public SyntacticMapperLogger(ILogger<TCategoryName> logger)
    {
        if (logger is null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        TypeParameter = new TypeParameterLogger(logger);
        ConstructorParameter = new ConstructorParameterLogger(logger);
        NamedParameter = new NamedParameterLogger(logger);
    }

    ITypeParameterLogger ISyntacticMapperLogger.TypeParameter => TypeParameter;
    IConstructorParameterLogger ISyntacticMapperLogger.ConstructorParameter => ConstructorParameter;
    INamedParameterLogger ISyntacticMapperLogger.NamedParameter => NamedParameter;

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
