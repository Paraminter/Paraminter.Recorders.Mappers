namespace SharpAttributeParser.Mappers;

using SharpAttributeParser.Mappers.SemanticMapperComponents;

/// <summary>Maps attribute parameters to recorders, responsible for recording arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record arguments.</typeparam>
public interface ISemanticMapper<in TRecord>
{
    /// <summary>Maps attribute type parameters to recorders.</summary>
    public abstract ISemanticTypeMapper<TRecord> Type { get; }

    /// <summary>Maps attribute constructor parameters to recorders.</summary>
    public abstract ISemanticConstructorMapper<TRecord> Constructor { get; }

    /// <summary>Maps named attribute parameters to recorders.</summary>
    public abstract ISemanticNamedMapper<TRecord> Named { get; }
}
