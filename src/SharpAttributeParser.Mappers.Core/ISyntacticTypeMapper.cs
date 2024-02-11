namespace SharpAttributeParser.Mappers;

using Microsoft.CodeAnalysis;

/// <summary>Maps attribute type parameters to recorders, responsible for recording syntactic information about the arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type to which arguments are recorded.</typeparam>
public interface ISyntacticTypeMapper<in TRecord>
{
    /// <summary>Attempts to map a type parameter to a recorder.</summary>
    /// <param name="parameter">The type parameter.</param>
    /// <param name="dataRecord">The record to which syntactic information about arguments is recorded by the mapped recorder.</param>
    /// <returns>The mapped recorder, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract ISyntacticMappedTypeRecorder? TryMapParameter(ITypeParameterSymbol parameter, TRecord dataRecord);
}
