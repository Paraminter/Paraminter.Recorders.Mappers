namespace SharpAttributeParser.Mappers.SyntacticMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.SyntacticMappedRecorders;

/// <summary>Maps attribute type parameters to recorders, responsible for recording syntactic information about the arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record syntactic information.</typeparam>
public interface ISyntacticTypeMapper<in TRecord>
{
    /// <summary>Maps a type parameter to a recorder.</summary>
    /// <param name="parameter">The type parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract ISyntacticMappedTypeRecorder<TRecord> MapParameter(ITypeParameterSymbol parameter);
}
