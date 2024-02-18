namespace SharpAttributeParser.Mappers.MapperComponents;

using SharpAttributeParser.Mappers.MappedRecorders;

/// <summary>Maps named attribute parameters to recorders, responsible for recording arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record arguments.</typeparam>
public interface INamedMapper<in TRecord>
{
    /// <summary>Maps a named parameter to a recorder.</summary>
    /// <param name="parameterName">The name of the named parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract IMappedNamedRecorder<TRecord> MapParameter(string parameterName);
}
