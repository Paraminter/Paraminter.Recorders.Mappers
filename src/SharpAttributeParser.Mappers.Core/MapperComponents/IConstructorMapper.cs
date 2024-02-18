namespace SharpAttributeParser.Mappers.MapperComponents;

using SharpAttributeParser.Mappers.MapperComponents.ConstructorMapperComponents;

/// <summary>Maps attribute constructor parameters to recorders, responsible for recording arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record arguments.</typeparam>
public interface IConstructorMapper<in TRecord>
{
    /// <summary>Maps parameters to recorders responsible for recording normal arguments.</summary>
    public abstract INormalConstructorMapper<TRecord> Normal { get; }

    /// <summary>Maps parameters to recorders responsible for recording <see langword="params"/>-arguments.</summary>
    public abstract IParamsConstructorMapper<TRecord> Params { get; }

    /// <summary>Maps optional parameters to recorders responsible for recording unspecified arguments.</summary>
    public abstract IDefaultConstructorMapper<TRecord> Default { get; }
}
