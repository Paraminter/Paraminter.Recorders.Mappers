namespace Paraminter.Mappers;

/// <summary>Handles creation of <see cref="IArgumentDataRecorder{TParameter, TArgumentData}"/>.</summary>
public interface IArgumentDataRecorderFactory
{
    /// <summary>Creates a recorder which records data about the arguments of parameters to the provided record.</summary>
    /// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
    /// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
    /// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
    /// <param name="mapper">Maps type parameters to recorders, responsible for recording data about the arguments of that parameter.</param>
    /// <param name="dataRecord">The record to which data is recorded.</param>
    /// <returns>The created recorder.</returns>
    public abstract IArgumentDataRecorder<TParameter, TArgumentData> Create<TParameter, TRecord, TArgumentData>(
        IParameterMapper<TParameter, TRecord, TArgumentData> mapper,
        TRecord dataRecord);
}
