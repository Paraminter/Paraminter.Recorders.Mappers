namespace SharpAttributeParser.Mappers.Logging.SemanticMapperLoggerComponents;

using Microsoft.CodeAnalysis;

using System;

/// <summary>Handles logging for <see cref="ISemanticMapper"/> when related to constructor parameters.</summary>
public interface IConstructorParameterLogger
{
    /// <summary>Begins a log scope describing an attempt to map a constructor parameter to a recorder.</summary>
    /// <typeparam name="TRecorder">The type of the mapped recorders.</typeparam>
    /// <param name="parameter">The constructor parameter.</param>
    /// <returns>The <see cref="IDisposable"/> used to close the log scope.</returns>
    public abstract IDisposable? BeginScopeMappingConstructorParameter<TRecorder>(IParameterSymbol parameter);
}
