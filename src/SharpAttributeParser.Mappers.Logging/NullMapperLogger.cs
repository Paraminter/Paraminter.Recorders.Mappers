namespace SharpAttributeParser.Mappers.Logging;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.Logging.MapperLoggerComponents;

using System;

/// <summary>A <see cref="IMapperLogger"/> with no behaviour.</summary>
/// <typeparam name="TCategoryName">The name of the logging category.</typeparam>
public sealed class NullMapperLogger<TCategoryName> : IMapperLogger<TCategoryName>
{
    /// <summary>A <see cref="IMapperLogger"/> with no behaviour.</summary>
    public static IMapperLogger<TCategoryName> Instance { get; } = new NullMapperLogger<TCategoryName>();

    ITypeParameterLogger IMapperLogger.TypeParameter { get; } = new NullTypeParameterLogger();
    IConstructorParameterLogger IMapperLogger.ConstructorParameter { get; } = new NullConstructorParameterLogger();
    INamedParameterLogger IMapperLogger.NamedParameter { get; } = new NullNamedParameterLogger();

    private NullMapperLogger() { }

    private sealed class NullTypeParameterLogger : ITypeParameterLogger
    {
        IDisposable? ITypeParameterLogger.BeginScopeMappingTypeParameter<TRecorder>(ITypeParameterSymbol parameter) => null;

        void ITypeParameterLogger.FailedToMapTypeParameter() { }
    }

    private sealed class NullConstructorParameterLogger : IConstructorParameterLogger
    {
        IDisposable? IConstructorParameterLogger.BeginScopeMappingConstructorParameter<TRecorder>(IParameterSymbol parameter) => null;

        void IConstructorParameterLogger.FailedToMapConstructorParameter() { }
    }

    private sealed class NullNamedParameterLogger : INamedParameterLogger
    {
        IDisposable? INamedParameterLogger.BeginScopeMappingNamedParameter<TRecorder>(string parameterName) => null;

        void INamedParameterLogger.FailedToMapNamedParameter() { }
    }
}
