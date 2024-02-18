namespace SharpAttributeParser.Mappers.MappedRecorders.MappedConstructorRecorders;

/// <summary>Records the unspecified arguments of some optional constructor parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which arguments are recorded.</typeparam>
public interface IMappedDefaultConstructorRecorder<in TRecord>
{
    /// <summary>Attempts to record an unspecified argument of some optional constructor parameter.</summary>
    /// <param name="dataRecord">The data record to which arguments are recorded.</param>
    /// <param name="argument">The argument of the constructor parameter.</param>
    /// <returns>A <see cref="bool"/> indicating whether the argument was successfully recorded.</returns>
    public abstract bool TryRecordArgument(TRecord dataRecord, object? argument);
}
