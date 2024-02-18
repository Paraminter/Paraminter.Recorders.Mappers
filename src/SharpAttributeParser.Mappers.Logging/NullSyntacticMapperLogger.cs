namespace SharpAttributeParser.Mappers.Logging;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.Logging.SyntacticMapperLoggerComponents;

using System;

/// <summary>A <see cref="ISyntacticMapperLogger"/> with no behaviour.</summary>
/// <typeparam name="TCategoryName">The name of the logging category.</typeparam>
public sealed class NullSyntacticMapperLogger<TCategoryName> : ISyntacticMapperLogger<TCategoryName>
{
    /// <summary>A <see cref="ISyntacticMapperLogger"/> with no behaviour.</summary>
    public static ISyntacticMapperLogger<TCategoryName> Instance { get; } = new NullSyntacticMapperLogger<TCategoryName>();

    ITypeParameterLogger ISyntacticMapperLogger.TypeParameter { get; } = new NullTypeParameterLogger();
    IConstructorParameterLogger ISyntacticMapperLogger.ConstructorParameter { get; } = new NullConstructorParameterLogger();
    INamedParameterLogger ISyntacticMapperLogger.NamedParameter { get; } = new NullNamedParameterLogger();

    private NullSyntacticMapperLogger() { }

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
