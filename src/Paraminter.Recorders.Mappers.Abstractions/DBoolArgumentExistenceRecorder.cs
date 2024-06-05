namespace Paraminter.Mappers;

/// <summary>Attempts to record the existence of arguments of some parameter.</summary>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
/// <param name="dataRecord">The record to which data is recorded.</param>
/// <returns>A <see cref="bool"/> indicating whether the attempt was successful.</returns>
public delegate bool DBoolArgumentExistenceRecorder<in TRecord>(
    TRecord dataRecord);
