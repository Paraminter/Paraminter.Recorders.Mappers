namespace Attribinter.Mappers;

/// <summary>Attempts to record data about the arguments of some parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which data is recorded.</typeparam>
/// <typeparam name="TData">The type of the recorded data.</typeparam>
/// <param name="dataRecord">The data record to which data is recorded.</param>
/// <param name="data">The recorded data, describing the argument.</param>
/// <returns>A <see cref="bool"/> indicating whether the data was successfully recorded.</returns>
public delegate bool DAttemptingArgumentRecorder<in TRecord, in TData>(TRecord dataRecord, TData data);
