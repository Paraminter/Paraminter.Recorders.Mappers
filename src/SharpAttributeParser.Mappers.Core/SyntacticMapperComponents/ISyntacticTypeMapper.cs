namespace SharpAttributeParser.Mappers.SyntacticMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.SyntacticMappedRecorders;

/// <summary>Maps attribute type parameters to recorders, responsible for recording syntactic information about the arguments of that parameter.</summary>
public interface ISyntacticTypeMapper
{
    /// <summary>Maps a type parameter to a recorder.</summary>
    /// <param name="parameter">The type parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract ISyntacticMappedTypeRecorder MapParameter(ITypeParameterSymbol parameter);
}
