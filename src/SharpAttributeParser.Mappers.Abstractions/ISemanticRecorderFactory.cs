namespace SharpAttributeParser.Mappers;

/// <summary>Handles creation of <see cref="ISemanticRecorder"/> using <see cref="ISemanticMapper{TRecord}"/>.</summary>
public interface ISemanticRecorderFactory
{
    /// <summary>Creates a recorder which records the arguments of attributes to the provided record.</summary>
    /// <typeparam name="TRecord">The type of the data record to which the created recorder records arguments.</typeparam>
    /// <param name="mapper">Maps parameters of the attribute to recorders, responsible for recording arguments of that parameter.</param>
    /// <param name="dataRecord">The data record to which the created recorder records arguments.</param>
    /// <returns>The created recorder.</returns>
    public abstract ISemanticRecorder Create<TRecord>(ISemanticMapper<TRecord> mapper, TRecord dataRecord);
}
