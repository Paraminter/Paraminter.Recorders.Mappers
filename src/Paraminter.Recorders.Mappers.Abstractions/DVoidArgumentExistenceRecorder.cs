namespace Paraminter.Mappers;

/// <summary>Records the existence of arguments of some parameter.</summary>
/// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
/// <param name="dataRecord">The record to which data is recorded.</param>
public delegate void DVoidArgumentExistenceRecorder<in TRecord>(
    TRecord dataRecord);
