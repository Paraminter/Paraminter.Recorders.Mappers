namespace SharpAttributeParser.Mappers;

using SharpAttributeParser.Mappers.MapperComponents;

/// <summary>Maps attribute parameters to recorders, responsible for recording arguments of that parameter.</summary>
public interface IMapper
{
    /// <summary>Maps attribute type parameters to recorders.</summary>
    public abstract ITypeMapper Type { get; }

    /// <summary>Maps attribute constructor parameters to recorders.</summary>
    public abstract IConstructorMapper Constructor { get; }

    /// <summary>Maps named attribute parameters to recorders.</summary>
    public abstract INamedMapper Named { get; }
}
