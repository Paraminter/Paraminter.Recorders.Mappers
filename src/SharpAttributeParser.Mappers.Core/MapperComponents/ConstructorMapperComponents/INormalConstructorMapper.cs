namespace SharpAttributeParser.Mappers.MapperComponents.ConstructorMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.MappedRecorders.MappedConstructorRecorders;

/// <summary>Maps attribute constructor parameters to recorders, responsible for recording normal arguments of that parameter.</summary>
public interface INormalConstructorMapper
{
    /// <summary>Maps a constructor parameter to a recorder.</summary>
    /// <param name="parameter">The constructor parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract IMappedNormalConstructorRecorder MapParameter(IParameterSymbol parameter);
}
