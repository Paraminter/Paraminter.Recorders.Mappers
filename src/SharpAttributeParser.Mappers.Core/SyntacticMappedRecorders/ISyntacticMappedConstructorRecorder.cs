namespace SharpAttributeParser.Mappers.SyntacticMappedRecorders;

using SharpAttributeParser.Mappers.SyntacticMappedRecorders.SyntacticMappedConstructorRecorders;

/// <summary>Records syntactic information about the arguments of some constructor parameter.</summary>
public interface ISyntacticMappedConstructorRecorder
{
    /// <summary>Records syntactic information about the normal arguments of some constructor parameter.</summary>
    public abstract ISyntacticMappedNormalConstructorRecorder Normal { get; }

    /// <summary>Records syntactic information about the <see langword="params"/>-arguments of some constructor parameter.</summary>
    public abstract ISyntacticMappedParamsConstructorRecorder Params { get; }

    /// <summary>Records syntactic information about the unspecified arguments of some optional constructor parameter.</summary>
    public abstract ISyntacticMappedDefaultConstructorRecorder Default { get; }
}
