namespace SharpAttributeParser.Mappers.Logging;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers.Logging.RecorderLoggerComponents;

using System;
using System.Collections.Generic;

/// <summary>A <see cref="IRecorderLogger"/> with no behaviour.</summary>
/// <typeparam name="TCategoryName">The name of the logging category.</typeparam>
public sealed class NullRecorderLogger<TCategoryName> : IRecorderLogger<TCategoryName>
{
    /// <summary>A <see cref="IRecorderLogger"/> with no behaviour.</summary>
    public static IRecorderLogger<TCategoryName> Instance { get; } = new NullRecorderLogger<TCategoryName>();

    ITypeArgumentLogger IRecorderLogger.TypeArgument { get; } = new NullTypeArgumentLogger();
    IConstructorArgumentLogger IRecorderLogger.ConstructorArgument { get; } = new NullConstructorArgumentLogger();
    INamedArgumentLogger IRecorderLogger.NamedArgument { get; } = new NullNamedArgumentLogger();

    private NullRecorderLogger() { }

    private sealed class NullTypeArgumentLogger : ITypeArgumentLogger
    {
        IDisposable? ITypeArgumentLogger.BeginScopeRecordingTypeArgument(ITypeParameterSymbol parameter, ITypeSymbol argument, ExpressionSyntax syntax) => null;

        void ITypeArgumentLogger.FailedToMapTypeParameterToRecorder() { }
    }

    private sealed class NullConstructorArgumentLogger : IConstructorArgumentLogger
    {
        IDisposable? IConstructorArgumentLogger.BeginScopeRecordingNormalConstructorlArgument(IParameterSymbol parameter, object? argument, ExpressionSyntax syntax) => null;
        IDisposable? IConstructorArgumentLogger.BeginScopeRecordingParamsConstructorArgument(IParameterSymbol parameter, object? argument, IReadOnlyList<ExpressionSyntax> elementSyntax) => null;
        IDisposable? IConstructorArgumentLogger.BeginScopeRecordingDefaultConstructorArgument(IParameterSymbol parameter, object? argument) => null;

        void IConstructorArgumentLogger.FailedToMapConstructorParameterToRecorder() { }
    }

    private sealed class NullNamedArgumentLogger : INamedArgumentLogger
    {
        IDisposable? INamedArgumentLogger.BeginScopeRecordingNamedArgument(string parameterName, object? argument, ExpressionSyntax syntax) => null;

        void INamedArgumentLogger.FailedToMapNamedParameterToRecorder() { }
    }
}
