namespace SharpAttributeParser.Mappers.MapperComponents;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.MappedRecorders;

/// <summary>Maps attribute type parameters to recorders, responsible for recording arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which the mapped recorders record arguments.</typeparam>
public interface ITypeMapper<in TRecord>
{
    /// <summary>Maps a type parameter to a recorder.</summary>
    /// <param name="parameter">The type parameter.</param>
    /// <returns>The mapped recorder.</returns>
    public abstract IMappedTypeRecorder<TRecord> MapParameter(ITypeParameterSymbol parameter);
}
