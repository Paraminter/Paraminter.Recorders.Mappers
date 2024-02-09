namespace SharpAttributeParser.Mappers.Logging;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers.Logging.SyntacticRecorderLoggerComponents;

using System;
using System.Collections.Generic;

/// <summary>A <see cref="ISyntacticRecorderLogger"/> with no behaviour.</summary>
/// <typeparam name="TCategoryName">The name of the logging category.</typeparam>
public sealed class NullSyntacticRecorderLogger<TCategoryName> : ISyntacticRecorderLogger<TCategoryName>
{
    /// <summary>A <see cref="ISyntacticRecorderLogger"/> with no behaviour.</summary>
    public static ISyntacticRecorderLogger<TCategoryName> Instance { get; } = new NullSyntacticRecorderLogger<TCategoryName>();

    ITypeArgumentsLogger ISyntacticRecorderLogger.TypeArgument { get; } = new NullTypeArgumentLogger();
    IConstructorArgumentsLogger ISyntacticRecorderLogger.ConstructorArgument { get; } = new NullConstructorArgumentLogger();
    INamedArgumentsLogger ISyntacticRecorderLogger.NamedArgument { get; } = new NullNamedArgumentLogger();

    private NullSyntacticRecorderLogger() { }

    private sealed class NullTypeArgumentLogger : ITypeArgumentsLogger
    {
        IDisposable? ITypeArgumentsLogger.BeginScopeRecordingTypeArgument(ITypeParameterSymbol parameter, ExpressionSyntax syntax) => null;

        void ITypeArgumentsLogger.FailedToMapTypeParameterToRecorder() { }
    }

    private sealed class NullConstructorArgumentLogger : IConstructorArgumentsLogger
    {
        IDisposable? IConstructorArgumentsLogger.BeginScopeRecordingNormalConstructorArgument(IParameterSymbol parameter, ExpressionSyntax syntax) => null;
        IDisposable? IConstructorArgumentsLogger.BeginScopeRecordingParamsConstructorArgument(IParameterSymbol parameter, IReadOnlyList<ExpressionSyntax> elementSyntax) => null;
        IDisposable? IConstructorArgumentsLogger.BeginScopeRecordingDefaultConstructorArgument(IParameterSymbol parameter) => null;

        void IConstructorArgumentsLogger.FailedToMapConstructorParameterToRecorder() { }
    }

    private sealed class NullNamedArgumentLogger : INamedArgumentsLogger
    {
        IDisposable? INamedArgumentsLogger.BeginScopeRecordingNamedArgument(string parameterName, ExpressionSyntax syntax) => null;

        void INamedArgumentsLogger.FailedToMapNamedParameterToRecorder() { }
    }
}
