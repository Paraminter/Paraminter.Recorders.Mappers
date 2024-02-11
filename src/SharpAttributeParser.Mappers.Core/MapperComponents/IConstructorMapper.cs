namespace SharpAttributeParser.Mappers.MapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.MappedRecorders;

/// <summary>Maps attribute constructor parameters to recorders, responsible for recording arguments of that parameter.</summary>
public interface IConstructorMapper
{
    /// <summary>Attempts to map a constructor parameter to a recorder.</summary>
    /// <param name="parameter">The constructor parameter.</param>
    /// <returns>The mapped recorder, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract IMappedConstructorRecorder? TryMapParameter(IParameterSymbol parameter);
}
