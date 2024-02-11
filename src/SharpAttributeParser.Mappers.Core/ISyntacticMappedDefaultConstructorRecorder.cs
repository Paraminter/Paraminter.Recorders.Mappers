namespace SharpAttributeParser.Mappers;

/// <summary>Records syntactic information about the unspecified arguments of some optional constructor parameter.</summary>
public interface ISyntacticMappedDefaultConstructorRecorder
{
    /// <summary>Attempts to record syntactic information about an unspecified argument of an optional constructor parameter.</summary>
    /// <returns>A <see cref="bool"/> indicating whether syntactic information was successfully recorded.</returns>
    public abstract bool TryRecordArgument();
}
