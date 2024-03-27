namespace Attribinter.Mappers;

/// <summary>Handles creation of <see cref="IArgumentRecorder{TParameter, TPayload}"/> using <see cref="IParameterMapper{TParameter, TRecord, TData}"/>.</summary>
public interface IArgumentRecorderFactory
{
    /// <summary>Specifies the <see cref="IParameterMapper{TParameter, TRecord, TData}"/> that is used when creating <see cref="IArgumentRecorder{TParameter, TData}"/>.</summary>
    /// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
    /// <typeparam name="TRecord">The type of the data record to which data is recorded.</typeparam>
    /// <typeparam name="TData">The type of the data recorded by the mapped recorders.</typeparam>
    /// <param name="mapper">Maps type parameters to recorders, responsible for recording data about the arguments of that parameter.</param>
    /// <returns>A <see cref="IArgumentRecorderFactory{TParameter, TRecord, TData}"/>, handling creation of <see cref="IArgumentRecorder{TParameter, TData}"/>.</returns>
    public abstract IArgumentRecorderFactory<TParameter, TRecord, TData> WithMapper<TParameter, TRecord, TData>(IParameterMapper<TParameter, TRecord, TData> mapper);
}
