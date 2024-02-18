namespace SharpAttributeParser.Mappers.Logging;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.Logging.SemanticMapperLoggerComponents;

using System;

/// <summary>A <see cref="ISemanticMapperLogger"/> with no behaviour.</summary>
/// <typeparam name="TCategoryName">The name of the logging category.</typeparam>
public sealed class NullSemanticMapperLogger<TCategoryName> : ISemanticMapperLogger<TCategoryName>
{
    /// <summary>A <see cref="ISemanticMapperLogger"/> with no behaviour.</summary>
    public static ISemanticMapperLogger<TCategoryName> Instance { get; } = new NullSemanticMapperLogger<TCategoryName>();

    ITypeParameterLogger ISemanticMapperLogger.TypeParameter { get; } = new NullTypeParameterLogger();
    IConstructorParameterLogger ISemanticMapperLogger.ConstructorParameter { get; } = new NullConstructorParameterLogger();
    INamedParameterLogger ISemanticMapperLogger.NamedParameter { get; } = new NullNamedParameterLogger();

    private NullSemanticMapperLogger() { }

    private sealed class NullTypeParameterLogger : ITypeParameterLogger
    {
        IDisposable? ITypeParameterLogger.BeginScopeMappingTypeParameter<TRecorder>(ITypeParameterSymbol parameter) => null;
    }

    private sealed class NullConstructorParameterLogger : IConstructorParameterLogger
    {
        IDisposable? IConstructorParameterLogger.BeginScopeMappingConstructorParameter<TRecorder>(IParameterSymbol parameter) => null;
    }

    private sealed class NullNamedParameterLogger : INamedParameterLogger
    {
        IDisposable? INamedParameterLogger.BeginScopeMappingNamedParameter<TRecorder>(string parameterName) => null;
    }
}
