namespace SharpAttributeParser.Mappers.SyntacticMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.SyntacticMappedRecorders;

/// <summary>Maps attribute type parameters to recorders, responsible for recording syntactic information about the arguments of that parameter.</summary>
public interface ISyntacticTypeMapper
{
    /// <summary>Attempts to map a type parameter to a recorder.</summary>
    /// <param name="parameter">The type parameter.</param>
    /// <returns>The mapped recorder, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract ISyntacticMappedTypeRecorder? TryMapParameter(ITypeParameterSymbol parameter);
}
