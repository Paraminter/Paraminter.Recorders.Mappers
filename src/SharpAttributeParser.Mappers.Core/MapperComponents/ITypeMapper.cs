namespace SharpAttributeParser.Mappers.MapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.MappedRecorders;

/// <summary>Maps attribute type parameters to recorders, responsible for recording arguments of that parameter.</summary>
public interface ITypeMapper
{
    /// <summary>Attempts to map a type parameter to a recorder.</summary>
    /// <param name="parameter">The type parameter.</param>
    /// <returns>The mapped recorder, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract IMappedTypeRecorder? TryMapParameter(ITypeParameterSymbol parameter);
}
