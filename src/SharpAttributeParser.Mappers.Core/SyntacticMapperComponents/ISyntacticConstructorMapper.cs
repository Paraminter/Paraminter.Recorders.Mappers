namespace SharpAttributeParser.Mappers.SyntacticMapperComponents;

using SharpAttributeParser.Mappers.SyntacticMapperComponents.SyntacticConstructorMapperComponents;

/// <summary>Maps attribute constructor parameters to recorders, responsible for recording syntactic information about the arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record syntactic information.</typeparam>
public interface ISyntacticConstructorMapper<in TRecord>
{
    /// <summary>Maps parameters to recorders responsible for recording syntactic information about normal arguments.</summary>
    public abstract ISyntacticNormalConstructorMapper<TRecord> Normal { get; }

    /// <summary>Maps parameters to recorders responsible for recording syntactic information about <see langword="params"/>-arguments.</summary>
    public abstract ISyntacticParamsConstructorMapper<TRecord> Params { get; }

    /// <summary>Maps optional parameters to recorders responsible for recording syntactic information about unspecified arguments.</summary>
    public abstract ISyntacticDefaultConstructorMapper<TRecord> Default { get; }
}
