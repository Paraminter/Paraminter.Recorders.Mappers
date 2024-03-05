namespace Attribinter.Mappers;

/// <summary>Maps parameters to recorders, responsible for recording data about the arguments of that parameter.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record data.</typeparam>
/// <typeparam name="TData">The type of the data recorded by the mapped recorders.</typeparam>
public interface IParameterMapper<in TParameter, in TRecord, in TData>
{
    /// <summary>Attempts to map a parameter to a recorder, responsible for recording data about the arguments of that parameter.</summary>
    /// <param name="parameter">The parameter that is mapped to a recorder.</param>
    /// <returns>The mapped recorder, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract IMappedArgumentRecorder<TRecord, TData>? TryMapParameter(TParameter parameter);
}
