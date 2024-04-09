namespace Attribinter.Mappers;

/// <summary>Handles creation of <see cref="IArgumentRecorder{TParameter, TData}"/>.</summary>
public interface IArgumentRecorderFactory
{
    /// <summary>Creates a recorder which records data about the arguments of parameters to the provided record.</summary>
    /// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
    /// <typeparam name="TRecord">The type of the data record to which data is recorded.</typeparam>
    /// <typeparam name="TData">The type of the data recorded by the mapped recorders.</typeparam>
    /// <param name="mapper">Maps type parameters to recorders, responsible for recording data about the arguments of that parameter.</param>
    /// <param name="dataRecord">The record to which the created recorder records data.</param>
    /// <returns>The created recorder.</returns>
    public abstract IArgumentRecorder<TParameter, TData> Create<TParameter, TRecord, TData>(IParameterMapper<TParameter, TRecord, TData> mapper, TRecord dataRecord);
}
