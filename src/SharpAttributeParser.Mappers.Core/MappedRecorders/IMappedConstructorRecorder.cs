namespace SharpAttributeParser.Mappers.MappedRecorders;

using SharpAttributeParser.Mappers.MappedRecorders.MappedConstructorRecorders;

/// <summary>Records the arguments of some constructor parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which arguments are recorded.</typeparam>
public interface IMappedConstructorRecorder<in TRecord>
{
    /// <summary>Records the normal arguments of some constructor parameter.</summary>
    public abstract IMappedNormalConstructorRecorder<TRecord> Normal { get; }

    /// <summary>Records the <see langword="params"/>-arguments of some constructor parameter.</summary>
    public abstract IMappedParamsConstructorRecorder<TRecord> Params { get; }

    /// <summary>Records the unspecified arguments of some optional constructor parameter.</summary>
    public abstract IMappedDefaultConstructorRecorder<TRecord> Default { get; }
}
