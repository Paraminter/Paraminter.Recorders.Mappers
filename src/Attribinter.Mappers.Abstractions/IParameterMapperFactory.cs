namespace Attribinter.Mappers;

/// <summary>Handles creation of <see cref="IParameterMapper{TParameter, TRecord, TData}"/>.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record data.</typeparam>
/// <typeparam name="TData">The type of the data recorded by the mapped recorders.</typeparam>
public interface IParameterMapperFactory<TParameter, TRecord, TData>
{
    /// <summary>Creates a <see cref="IParameterMapper{TParameter, TRecord, TData}"/>, mapping parameters to recorders.</summary>
    /// <returns>The created <see cref="IParameterMapper{TParameter, TRecord, TData}"/>.</returns>
    public abstract IParameterMapper<TParameter, TRecord, TData> Create();
}
