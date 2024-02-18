namespace SharpAttributeParser.Mappers.MapperComponents.ConstructorMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.MappedRecorders.MappedConstructorRecorders;

/// <summary>Maps optional attribute constructor parameters to recorders, responsible for recording unspecified arguments of that parameter.</summary>
public interface IDefaultConstructorMapper
{
    /// <summary>Maps a constructor parameter to a recorder.</summary>
    /// <param name="parameter">The constructor parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract IMappedDefaultConstructorRecorder MapParameter(IParameterSymbol parameter);
}
