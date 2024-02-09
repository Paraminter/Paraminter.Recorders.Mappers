namespace SharpAttributeParser.Mappers;

/// <summary>Handles creation of <see cref="IRecorder"/> using mappers.</summary>
public interface IRecorderFactory
{
    /// <summary>Creates a recorder which records the arguments of attributes to the provided record.</summary>
    /// <typeparam name="TRecord">The type to which arguments are recorded.</typeparam>
    /// <param name="mapper">Maps parameters of the attribute to recorders, responsible for recording arguments of that parameter.</param>
    /// <param name="dataRecord">The record to which arguments are recorded by the created recorder.</param>
    /// <returns>The created recorder.</returns>
    public abstract IRecorder<TRecord> Create<TRecord>(IMapper<TRecord> mapper, TRecord dataRecord);
}
