namespace SharpAttributeParser.Mappers.SemanticMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.SemanticMappedRecorders;

/// <summary>Maps attribute type parameters to recorders, responsible for recording arguments of that parameter.</summary>
public interface ISemanticTypeMapper
{
    /// <summary>Maps a type parameter to a recorder.</summary>
    /// <param name="parameter">The type parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract ISemanticMappedTypeRecorder MapParameter(ITypeParameterSymbol parameter);
}
