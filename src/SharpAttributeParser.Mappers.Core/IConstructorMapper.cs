namespace SharpAttributeParser.Mappers;

using Microsoft.CodeAnalysis;

/// <summary>Maps attribute constructor parameters to recorders, responsible for recording arguments of that parameter.</summary>
/// <typeparam name="TRecord">The type to which arguments are recorded.</typeparam>
public interface IConstructorMapper<in TRecord>
{
    /// <summary>Attempts to map a constructor parameter to a recorder.</summary>
    /// <param name="parameter">The constructor parameter.</param>
    /// <param name="dataRecord">The record to which arguments are recorded by the mapped recorder.</param>
    /// <returns>The mapped recorder, or <see langword="null"/> if the attempt was unsuccessful.</returns>
    public abstract IMappedConstructorRecorder? TryMapParameter(IParameterSymbol parameter, TRecord dataRecord);
}
