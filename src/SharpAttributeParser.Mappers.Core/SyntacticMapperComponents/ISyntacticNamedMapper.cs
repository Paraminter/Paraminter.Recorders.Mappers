namespace SharpAttributeParser.Mappers.SyntacticMapperComponents;

using SharpAttributeParser.Mappers.SyntacticMappedRecorders;

/// <summary>Maps named attribute parameters to recorders, responsible for recording syntactic information about the arguments of that parameter.</summary>
public interface ISyntacticNamedMapper
{
    /// <summary>Attempts to map a named parameter to a recorder.</summary>
    /// <param name="parameterName">The name of the named parameter.</param>
    /// <returns>The mapped recorder, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract ISyntacticMappedNamedRecorder? TryMapParameter(string parameterName);
}
