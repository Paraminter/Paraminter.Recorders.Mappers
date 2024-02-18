namespace SharpAttributeParser.Mappers.SemanticMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.SemanticMappedRecorders;

/// <summary>Maps attribute type parameters to recorders, responsible for recording arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record arguments.</typeparam>
public interface ISemanticTypeMapper<in TRecord>
{
    /// <summary>Maps a type parameter to a recorder.</summary>
    /// <param name="parameter">The type parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract ISemanticMappedTypeRecorder<TRecord> MapParameter(ITypeParameterSymbol parameter);
}
