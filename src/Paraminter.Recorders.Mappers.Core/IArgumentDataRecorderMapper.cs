namespace Paraminter.Recorders.Mappers;

/// <summary>Maps parameters to recorders, responsible for recording data about the arguments of that parameter.</summary>
/// <typeparam name="TParameter">The type of the mapped parameters.</typeparam>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
/// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
public interface IArgumentDataRecorderMapper<in TParameter, in TRecord, in TArgumentData>
{
    /// <summary>Attempts to map a parameter to a recorder, responsible for recording data about the arguments of that parameter.</summary>
    /// <param name="parameter">The parameter that is mapped to a recorder.</param>
    /// <returns>The mapped recorder, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract IMappedArgumentDataRecorder<TRecord, TArgumentData>? TryMapParameter(
        TParameter parameter);
}
