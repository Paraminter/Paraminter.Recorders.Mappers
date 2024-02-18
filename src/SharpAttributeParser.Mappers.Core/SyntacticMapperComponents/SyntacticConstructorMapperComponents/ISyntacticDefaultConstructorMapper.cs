namespace SharpAttributeParser.Mappers.SyntacticMapperComponents.SyntacticConstructorMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.SyntacticMappedRecorders.SyntacticMappedConstructorRecorders;

/// <summary>Maps optional attribute constructor parameters to recorders, responsible for recording syntactic information about unspecified arguments of that parameter.</summary>
public interface ISyntacticDefaultConstructorMapper
{
    /// <summary>Maps a constructor parameter to a recorder.</summary>
    /// <param name="parameter">The constructor parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract ISyntacticMappedDefaultConstructorRecorder MapParameter(IParameterSymbol parameter);
}
