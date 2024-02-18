namespace SharpAttributeParser.Mappers.SyntacticMapperComponents.SyntacticConstructorMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.SyntacticMappedRecorders.SyntacticMappedConstructorRecorders;

/// <summary>Maps optional attribute constructor parameters to recorders, responsible for recording syntactic information about unspecified arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record syntactic information.</typeparam>
public interface ISyntacticDefaultConstructorMapper<in TRecord>
{
    /// <summary>Maps a constructor parameter to a recorder.</summary>
    /// <param name="parameter">The constructor parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract ISyntacticMappedDefaultConstructorRecorder<TRecord> MapParameter(IParameterSymbol parameter);
}
