namespace Paraminter.Recorders.Mappers;

/// <summary>Maps parameters to recorders, responsible for recording the existence of arguments of that parameter.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
public interface IArgumentExistenceRecorderMapper<in TParameter, in TRecord>
{
    /// <summary>Attempts to map a parameter to a recorder, responsible for recording the existence of arguments of that parameter.</summary>
    /// <param name="parameter">The parameter that is mapped to a recorder.</param>
    /// <returns>The mapped recorder, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract IMappedArgumentExistenceRecorder<TRecord>? TryMapParameter(
        TParameter parameter);
}
