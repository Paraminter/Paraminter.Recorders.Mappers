namespace SharpAttributeParser.Mappers;

using SharpAttributeParser.Mappers.SyntacticMapperComponents;

/// <summary>Maps attribute parameters to recorders, responsible for recording syntactic information about arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record syntactic information.</typeparam>
public interface ISyntacticMapper<in TRecord>
{
    /// <summary>Maps attribute type parameters to recorders.</summary>
    public abstract ISyntacticTypeMapper<TRecord> Type { get; }

    /// <summary>Maps attribute constructor parameters to recorders.</summary>
    public abstract ISyntacticConstructorMapper<TRecord> Constructor { get; }

    /// <summary>Maps named attribute parameters to recorders.</summary>
    public abstract ISyntacticNamedMapper<TRecord> Named { get; }
}
