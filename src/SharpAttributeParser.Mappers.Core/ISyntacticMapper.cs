namespace SharpAttributeParser.Mappers;

using SharpAttributeParser.Mappers.SyntacticMapperComponents;

/// <summary>Maps attribute parameters to recorders, responsible for recording syntactic information about arguments of that parameter.</summary>
public interface ISyntacticMapper
{
    /// <summary>Maps attribute type parameters to recorders.</summary>
    public abstract ISyntacticTypeMapper Type { get; }

    /// <summary>Maps attribute constructor parameters to recorders.</summary>
    public abstract ISyntacticConstructorMapper Constructor { get; }

    /// <summary>Maps named attribute parameters to recorders.</summary>
    public abstract ISyntacticNamedMapper Named { get; }
}
