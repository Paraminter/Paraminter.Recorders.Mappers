namespace SharpAttributeParser.Mappers;

using SharpAttributeParser.Mappers.SemanticMapperComponents;

/// <summary>Maps attribute parameters to recorders, responsible for recording arguments of that parameter.</summary>
public interface ISemanticMapper
{
    /// <summary>Maps attribute type parameters to recorders.</summary>
    public abstract ISemanticTypeMapper Type { get; }

    /// <summary>Maps attribute constructor parameters to recorders.</summary>
    public abstract ISemanticConstructorMapper Constructor { get; }

    /// <summary>Maps named attribute parameters to recorders.</summary>
    public abstract ISemanticNamedMapper Named { get; }
}
