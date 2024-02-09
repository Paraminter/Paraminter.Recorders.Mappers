namespace SharpAttributeParser.Mappers.Logging;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.Logging.SemanticRecorderLoggerComponents;

using System;

/// <summary>A <see cref="ISemanticRecorderLogger"/> with no behaviour.</summary>
/// <typeparam name="TCategoryName">The name of the logging category.</typeparam>
public sealed class NullSemanticRecorderLogger<TCategoryName> : ISemanticRecorderLogger<TCategoryName>
{
    /// <summary>A <see cref="ISemanticRecorderLogger"/> with no behaviour.</summary>
    public static ISemanticRecorderLogger<TCategoryName> Instance { get; } = new NullSemanticRecorderLogger<TCategoryName>();

    ITypeArgumentLogger ISemanticRecorderLogger.TypeArgument { get; } = new NullTypeArgumentLogger();
    IConstructorArgumentLogger ISemanticRecorderLogger.ConstructorArgument { get; } = new NullConstructorArgumentLogger();
    INamedArgumentLogger ISemanticRecorderLogger.NamedArgument { get; } = new NullNamedArgumentLogger();

    private NullSemanticRecorderLogger() { }

    private sealed class NullTypeArgumentLogger : ITypeArgumentLogger
    {
        IDisposable? ITypeArgumentLogger.BeginScopeRecordingTypeArgument(ITypeParameterSymbol parameter, ITypeSymbol argument) => null;

        void ITypeArgumentLogger.FailedToMapTypeParameterToRecorder() { }
    }

    private sealed class NullConstructorArgumentLogger : IConstructorArgumentLogger
    {
        IDisposable? IConstructorArgumentLogger.BeginScopeRecordingConstructorArgument(IParameterSymbol parameter, object? argument) => null;

        void IConstructorArgumentLogger.FailedToMapConstructorParameterToRecorder() { }
    }

    private sealed class NullNamedArgumentLogger : INamedArgumentLogger
    {
        IDisposable? INamedArgumentLogger.BeginScopeRecordingNamedArgument(string parameterName, object? argument) => null;

        void INamedArgumentLogger.FailedToMapNamedParameterToRecorder() { }
    }
}
