namespace SharpAttributeParser.Mappers.SemanticMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.SemanticMappedRecorders;

/// <summary>Maps attribute constructor parameters to recorders, responsible for recording arguments of that parameter.</summary>
public interface ISemanticConstructorMapper
{
    /// <summary>Attempts to map a constructor parameter to a recorder.</summary>
    /// <param name="parameter">The constructor parameter.</param>
    /// <returns>The mapped recorder, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract ISemanticMappedConstructorRecorder? TryMapParameter(IParameterSymbol parameter);
}
