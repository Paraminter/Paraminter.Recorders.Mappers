namespace Paraminter.Recorders.Mappers;

/// <summary>Handles creation of <see cref="IArgumentExistenceRecorder{TParameter}"/>.</summary>
public interface IArgumentExistenceRecorderFactory
{
    /// <summary>Creates a recorder which records the existence of arguments of parameters to the provided record.</summary>
    /// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
    /// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
    /// <param name="mapper">Maps type parameters to recorders, responsible for recording data about the arguments of that parameter.</param>
    /// <param name="dataRecord">The record to which data is recorded.</param>
    /// <returns>The created recorder.</returns>
    public abstract IArgumentExistenceRecorder<TParameter> Create<TParameter, TRecord>(
        IArgumentExistenceRecorderMapper<TParameter, TRecord> mapper,
        TRecord dataRecord);
}
