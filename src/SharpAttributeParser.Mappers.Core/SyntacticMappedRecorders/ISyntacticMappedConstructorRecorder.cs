namespace SharpAttributeParser.Mappers.SyntacticMappedRecorders;

using SharpAttributeParser.Mappers.SyntacticMappedRecorders.SyntacticMappedConstructorRecorders;

/// <summary>Records syntactic information about the arguments of some constructor parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which syntactic information is recorded are recorded.</typeparam>
public interface ISyntacticMappedConstructorRecorder<in TRecord>
{
    /// <summary>Records syntactic information about the normal arguments of some constructor parameter.</summary>
    public abstract ISyntacticMappedNormalConstructorRecorder<TRecord> Normal { get; }

    /// <summary>Records syntactic information about the <see langword="params"/>-arguments of some constructor parameter.</summary>
    public abstract ISyntacticMappedParamsConstructorRecorder<TRecord> Params { get; }

    /// <summary>Records syntactic information about the unspecified arguments of some optional constructor parameter.</summary>
    public abstract ISyntacticMappedDefaultConstructorRecorder<TRecord> Default { get; }
}
