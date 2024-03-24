namespace Attribinter.Mappers;

/// <summary>Provides a <see cref="IParameterMapper{TParameter, TRecord, TData}"/>.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record data.</typeparam>
/// <typeparam name="TData">The type of the data recorded by the mapped recorders.</typeparam>
public interface IParameterMapperProvider<TParameter, TRecord, TData>
{
    /// <summary>The provided <see cref="IParameterMapper{TParameter, TRecord, TData}"/>.</summary>
    public abstract IParameterMapper<TParameter, TRecord, TData> Mapper { get; }
}
