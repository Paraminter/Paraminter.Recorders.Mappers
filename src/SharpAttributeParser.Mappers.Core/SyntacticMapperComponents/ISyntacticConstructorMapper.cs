namespace SharpAttributeParser.Mappers.SyntacticMapperComponents;

using SharpAttributeParser.Mappers.SyntacticMapperComponents.SyntacticConstructorMapperComponents;

/// <summary>Maps attribute constructor parameters to recorders, responsible for recording syntactic information about the arguments of that parameter.</summary>
public interface ISyntacticConstructorMapper
{
    /// <summary>Maps parameters to recorders responsible for recording syntactic information about normal arguments.</summary>
    public abstract ISyntacticNormalConstructorMapper Normal { get; }

    /// <summary>Maps parameters to recorders responsible for recording syntactic information about <see langword="params"/>-arguments.</summary>
    public abstract ISyntacticParamsConstructorMapper Params { get; }

    /// <summary>Maps optional parameters to recorders responsible for recording syntactic information about unspecified arguments.</summary>
    public abstract ISyntacticDefaultConstructorMapper Default { get; }
}
