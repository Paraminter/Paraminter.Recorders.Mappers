namespace SharpAttributeParser.Mappers.MappedRecorders;

using SharpAttributeParser.Mappers.MappedRecorders.MappedConstructorRecorders;

/// <summary>Records the arguments of some constructor parameter.</summary>
public interface IMappedConstructorRecorder
{
    /// <summary>Records the normal arguments of some constructor parameter.</summary>
    public abstract IMappedNormalConstructorRecorder Normal { get; }

    /// <summary>Records the <see langword="params"/>-arguments of some constructor parameter.</summary>
    public abstract IMappedParamsConstructorRecorder Params { get; }

    /// <summary>Records the unspecified arguments of some optional constructor parameter.</summary>
    public abstract IMappedDefaultConstructorRecorder Default { get; }
}
