namespace SharpAttributeParser.Mappers;

/// <summary>Maps attribute parameters to recorders, responsible for recording syntactic information about arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type to which syntactic information about arguments is recorded.</typeparam>
public interface ISyntacticMapper<in TRecord>
{
    /// <summary>Maps attribute type parameters to recorders.</summary>
    public abstract ISyntacticTypeMapper<TRecord> Type { get; }

    /// <summary>Maps attribute constructor parameters to recorders.</summary>
    public abstract ISyntacticConstructorMapper<TRecord> Constructor { get; }

    /// <summary>Maps named attribute parameters to recorders.</summary>
    public abstract ISyntacticNamedMapper<TRecord> Named { get; }
}
