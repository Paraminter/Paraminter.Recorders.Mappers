namespace SharpAttributeParser.Mappers.SyntacticMapperComponents.SyntacticConstructorMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.SyntacticMappedRecorders.SyntacticMappedConstructorRecorders;

/// <summary>Maps attribute constructor parameters to recorders, responsible for recording syntactic information about <see langword="params"/>-arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record syntactic information.</typeparam>
public interface ISyntacticParamsConstructorMapper<in TRecord>
{
    /// <summary>Maps a constructor parameter to a recorder.</summary>
    /// <param name="parameter">The constructor parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract ISyntacticMappedParamsConstructorRecorder<TRecord> MapParameter(IParameterSymbol parameter);
}
