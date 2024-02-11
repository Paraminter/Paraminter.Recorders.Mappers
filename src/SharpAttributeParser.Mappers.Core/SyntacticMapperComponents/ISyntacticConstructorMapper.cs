namespace SharpAttributeParser.Mappers.SyntacticMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.SyntacticMappedRecorders;

/// <summary>Maps attribute constructor parameters to recorders, responsible for recording syntactic information about the arguments of that parameter.</summary>
public interface ISyntacticConstructorMapper
{
    /// <summary>Attempts to map a constructor parameter to a recorder.</summary>
    /// <param name="parameter">The constructor parameter.</param>
    /// <returns>The mapped recorder, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract ISyntacticMappedConstructorRecorder? TryMapParameter(IParameterSymbol parameter);
}
