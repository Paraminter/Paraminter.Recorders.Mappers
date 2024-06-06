namespace Paraminter.Mappers;

/// <summary>Records data about the arguments of some parameter.</summary>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
/// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
/// <param name="dataRecord">The record to which data is recorded.</param>
/// <param name="argumentData">The data about the argument.</param>
public delegate void DVoidArgumentDataRecorder<in TRecord, in TArgumentData>(
    TRecord dataRecord,
    TArgumentData argumentData);
