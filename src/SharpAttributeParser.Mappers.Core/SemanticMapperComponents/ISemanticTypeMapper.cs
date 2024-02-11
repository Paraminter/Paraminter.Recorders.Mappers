namespace SharpAttributeParser.Mappers.SemanticMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.SemanticMappedRecorders;

/// <summary>Maps attribute type parameters to recorders, responsible for recording arguments of that parameter.</summary>
public interface ISemanticTypeMapper
{
    /// <summary>Attempts to map a type parameter to a recorder.</summary>
    /// <param name="parameter">The type parameter.</param>
    /// <returns>The mapped recorder, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract ISemanticMappedTypeRecorder? TryMapParameter(ITypeParameterSymbol parameter);
}
