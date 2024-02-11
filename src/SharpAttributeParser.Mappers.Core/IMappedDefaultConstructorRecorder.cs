namespace SharpAttributeParser.Mappers;

/// <summary>Records the unspecified arguments of some optional constructor parameter.</summary>
public interface IMappedDefaultConstructorRecorder
{
    /// <summary>Attempts to record an unspecified argument of some optional constructor parameter.</summary>
    /// <param name="argument">The argument of the constructor parameter.</param>
    /// <returns>A <see cref="bool"/> indicating whether the argument was successfully recorded.</returns>
    public abstract bool TryRecordArgument(object? argument);
}
