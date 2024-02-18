namespace SharpAttributeParser.Mappers.SemanticMapperComponents;

using SharpAttributeParser.Mappers.SemanticMappedRecorders;

/// <summary>Maps named attribute parameters to recorders, responsible for recording arguments of that parameter.</summary>
public interface ISemanticNamedMapper
{
    /// <summary>Maps a named parameter to a recorder.</summary>
    /// <param name="parameterName">The name of the named parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract ISemanticMappedNamedRecorder MapParameter(string parameterName);
}
