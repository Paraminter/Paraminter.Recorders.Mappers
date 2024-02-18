namespace SharpAttributeParser.Mappers.MapperComponents;

using SharpAttributeParser.Mappers.MapperComponents.ConstructorMapperComponents;

/// <summary>Maps attribute constructor parameters to recorders, responsible for recording arguments of that parameter.</summary>
public interface IConstructorMapper
{
    /// <summary>Maps parameters to recorders responsible for recording normal arguments.</summary>
    public abstract INormalConstructorMapper Normal { get; }

    /// <summary>Maps parameters to recorders responsible for recording <see langword="params"/>-arguments.</summary>
    public abstract IParamsConstructorMapper Params { get; }

    /// <summary>Maps optional parameters to recorders responsible for recording unspecified arguments.</summary>
    public abstract IDefaultConstructorMapper Default { get; }
}
