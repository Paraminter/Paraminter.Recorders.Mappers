namespace SharpAttributeParser.Mappers.Logging.SemanticMapperLoggerComponents;

using Microsoft.CodeAnalysis;

using System;

/// <summary>Handles logging for <see cref="ISemanticMapper{TRecord}"/> when related to type parameters.</summary>
public interface ITypeParameterLogger
{
    /// <summary>Begins a log scope describing an attempt to map a type parameter to a recorder.</summary>
    /// <typeparam name="TRecorder">The type of the mapped recorders.</typeparam>
    /// <param name="parameter">The type parameter.</param>
    /// <returns>The <see cref="IDisposable"/> used to close the log scope.</returns>
    public abstract IDisposable? BeginScopeMappingTypeParameter<TRecorder>(ITypeParameterSymbol parameter);
}
