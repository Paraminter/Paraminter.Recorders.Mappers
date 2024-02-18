namespace SharpAttributeParser.Mappers.SyntacticMapperComponents;

using SharpAttributeParser.Mappers.SyntacticMappedRecorders;

/// <summary>Maps named attribute parameters to recorders, responsible for recording syntactic information about the arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record syntactic information.</typeparam>
public interface ISyntacticNamedMapper<in TRecord>
{
    /// <summary>Maps a named parameter to a recorder.</summary>
    /// <param name="parameterName">The name of the named parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract ISyntacticMappedNamedRecorder<TRecord> MapParameter(string parameterName);
}
