namespace SharpAttributeParser.Mappers.SyntacticMappedRecorders.SyntacticMappedConstructorRecorders;

/// <summary>Records syntactic information about the unspecified arguments of some optional constructor parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which syntactic information is recorded are recorded.</typeparam>
public interface ISyntacticMappedDefaultConstructorRecorder<in TRecord>
{
    /// <summary>Attempts to record syntactic information about an unspecified argument of an optional constructor parameter.</summary>
    /// <param name="dataRecord">The data record to which syntactic information is recorded.</param>
    /// <returns>A <see cref="bool"/> indicating whether syntactic information was successfully recorded.</returns>
    public abstract bool TryRecordArgument(TRecord dataRecord);
}
