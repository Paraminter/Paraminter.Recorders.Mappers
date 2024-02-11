namespace SharpAttributeParser.Mappers;

/// <summary>Handles creation of <see cref="ISemanticRecorder"/> using <see cref="ISemanticMapper"/>.</summary>
public interface ISemanticRecorderFactory
{
    /// <summary>Creates a recorder which records the arguments of attributes to the provided record.</summary>
    /// <param name="mapper">Maps parameters of the attribute to recorders, responsible for recording arguments of that parameter.</param>
    /// <returns>The created recorder.</returns>
    public abstract ISemanticRecorder Create(ISemanticMapper mapper);
}
