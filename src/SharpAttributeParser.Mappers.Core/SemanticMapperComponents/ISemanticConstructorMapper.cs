namespace SharpAttributeParser.Mappers.SemanticMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.SemanticMappedRecorders;

/// <summary>Maps attribute constructor parameters to recorders, responsible for recording arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record arguments.</typeparam>
public interface ISemanticConstructorMapper<in TRecord>
{
    /// <summary>Maps a constructor parameter to a recorder.</summary>
    /// <param name="parameter">The constructor parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract ISemanticMappedConstructorRecorder<TRecord> MapParameter(IParameterSymbol parameter);
}
