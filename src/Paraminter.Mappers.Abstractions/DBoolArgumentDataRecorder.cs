namespace Paraminter.Mappers;

/// <summary>Attempts to record data about the arguments of some parameter.</summary>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
/// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
/// <param name="dataRecord">The record to which data is recorded.</param>
/// <param name="argumentData">The data about the argument.</param>
/// <returns>A <see cref="bool"/> indicating whether the attempt was successful.</returns>
public delegate bool DBoolArgumentDataRecorder<in TRecord, in TArgumentData>(TRecord dataRecord, TArgumentData argumentData);
