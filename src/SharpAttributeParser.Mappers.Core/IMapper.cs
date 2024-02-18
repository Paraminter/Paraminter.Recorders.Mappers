namespace SharpAttributeParser.Mappers;

using SharpAttributeParser.Mappers.MapperComponents;

/// <summary>Maps attribute parameters to recorders, responsible for recording arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record arguments.</typeparam>
public interface IMapper<in TRecord>
{
    /// <summary>Maps attribute type parameters to recorders.</summary>
    public abstract ITypeMapper<TRecord> Type { get; }

    /// <summary>Maps attribute constructor parameters to recorders.</summary>
    public abstract IConstructorMapper<TRecord> Constructor { get; }

    /// <summary>Maps named attribute parameters to recorders.</summary>
    public abstract INamedMapper<TRecord> Named { get; }
}
