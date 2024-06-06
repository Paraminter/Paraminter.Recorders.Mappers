namespace Paraminter.Mappers;

/// <summary>Records the existence of arguments of some parameter.</summary>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
public interface IMappedArgumentExistenceRecorder<in TRecord>
{
    /// <summary>Attempts to record the existence of the argument of some parameter.</summary>
    /// <param name="dataRecord">The record to which data is recorded.</param>
    /// <returns>A <see cref="bool"/> indicating whether the attempt was successful.</returns>
    public abstract bool TryRecordExistence(
        TRecord dataRecord);
}
