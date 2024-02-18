namespace SharpAttributeParser.Mappers.MapperComponents.ConstructorMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.MappedRecorders.MappedConstructorRecorders;

/// <summary>Maps attribute constructor parameters to recorders, responsible for recording <see langword="params"/>-arguments of that parameter.</summary>
public interface IParamsConstructorMapper
{
    /// <summary>Maps a constructor parameter to a recorder.</summary>
    /// <param name="parameter">The constructor parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract IMappedParamsConstructorRecorder MapParameter(IParameterSymbol parameter);
}
