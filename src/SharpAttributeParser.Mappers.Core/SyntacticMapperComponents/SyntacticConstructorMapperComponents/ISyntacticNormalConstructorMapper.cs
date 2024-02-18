namespace SharpAttributeParser.Mappers.SyntacticMapperComponents.SyntacticConstructorMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.SyntacticMappedRecorders.SyntacticMappedConstructorRecorders;

/// <summary>Maps attribute constructor parameters to recorders, responsible for recording syntactic information about normal arguments of that parameter.</summary>
public interface ISyntacticNormalConstructorMapper
{
    /// <summary>Maps a constructor parameter to a recorder.</summary>
    /// <param name="parameter">The constructor parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract ISyntacticMappedNormalConstructorRecorder MapParameter(IParameterSymbol parameter);
}
