namespace SharpAttributeParser.Mappers.MapperComponents.ConstructorMapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.MappedRecorders.MappedConstructorRecorders;

/// <summary>Maps attribute constructor parameters to recorders, responsible for recording <see langword="params"/>-arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record arguments.</typeparam>
public interface IParamsConstructorMapper<in TRecord>
{
    /// <summary>Maps a constructor parameter to a recorder.</summary>
    /// <param name="parameter">The constructor parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract IMappedParamsConstructorRecorder<TRecord> MapParameter(IParameterSymbol parameter);
}
