namespace SharpAttributeParser.Mappers;

/// <summary>Maps named attribute parameters to recorders, responsible for recording syntactic information about the arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type to which arguments are recorded.</typeparam>
public interface ISyntacticNamedMapper<in TRecord>
{
    /// <summary>Attempts to map a named parameter to a recorder.</summary>
    /// <param name="parameterName">The name of the named parameter.</param>
    /// <param name="dataRecord">The record to which syntactic information about arguments is recorded by the mapped recorder.</param>
    /// <returns>The mapped recorder, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract ISyntacticMappedNamedRecorder? TryMapParameter(string parameterName, TRecord dataRecord);
}
