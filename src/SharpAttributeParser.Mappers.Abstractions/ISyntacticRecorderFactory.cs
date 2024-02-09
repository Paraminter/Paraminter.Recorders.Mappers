namespace SharpAttributeParser.Mappers;

/// <summary>Handles creation of <see cref="ISyntacticRecorder"/> using <see cref="ISyntacticMapper{TRecord}"/>.</summary>
public interface ISyntacticRecorderFactory
{
    /// <summary>Creates a recorder which records syntactic information about the arguments of attributes to the provided record.</summary>
    /// <typeparam name="TRecord">The type to which syntactic information about arguments is recorded.</typeparam>
    /// <param name="mapper">Maps parameters of the attribute to recorders, responsible for recording syntactic information about arguments of that parameter.</param>
    /// <param name="dataRecord">The record to which syntactic information about arguments is recorded by the created recorder.</param>
    /// <returns>The created recorder.</returns>
    public abstract ISyntacticRecorder<TRecord> Create<TRecord>(ISyntacticMapper<TRecord> mapper, TRecord dataRecord);
}
