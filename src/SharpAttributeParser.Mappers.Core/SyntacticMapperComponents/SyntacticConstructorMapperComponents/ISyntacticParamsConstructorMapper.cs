namespace SharpAttributeParser.Mappers.SyntacticMapperComponents.SyntacticConstructorMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.SyntacticMappedRecorders.SyntacticMappedConstructorRecorders;

/// <summary>Maps attribute constructor parameters to recorders, responsible for recording syntactic information about <see langword="params"/>-arguments of that parameter.</summary>
public interface ISyntacticParamsConstructorMapper
{
    /// <summary>Maps a constructor parameter to a recorder.</summary>
    /// <param name="parameter">The constructor parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract ISyntacticMappedParamsConstructorRecorder MapParameter(IParameterSymbol parameter);
}
