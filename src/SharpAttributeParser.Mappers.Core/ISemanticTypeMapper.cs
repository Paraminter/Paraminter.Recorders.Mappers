namespace SharpAttributeParser.Mappers;

using Microsoft.CodeAnalysis;

/// <summary>Maps attribute type parameters to recorders, responsible for recording arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type to which arguments are recorded.</typeparam>
public interface ISemanticTypeMapper<in TRecord>
{
    /// <summary>Attempts to map a type parameter to a recorder.</summary>
    /// <param name="parameter">The type parameter.</param>
    /// <param name="dataRecord">The record to which arguments are recorded by the mapped recorder.</param>
    /// <returns>The mapped recorder, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract ISemanticMappedTypeRecorder? TryMapParameter(ITypeParameterSymbol parameter, TRecord dataRecord);
}
