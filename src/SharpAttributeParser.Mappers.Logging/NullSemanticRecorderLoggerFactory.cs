namespace SharpAttributeParser.Mappers.Logging;

using Microsoft.Extensions.Logging;

/// <summary>A <see cref="ISemanticRecorderLoggerFactory"/> creating <see cref="ISemanticRecorderLogger"/> with no behaviour.</summary>
public sealed class NullSemanticRecorderLoggerFactory : ISemanticRecorderLoggerFactory
{
    /// <summary>A <see cref="ISemanticRecorderLoggerFactory"/> creating <see cref="ISemanticRecorderLogger"/> with no behaviour.</summary>
    public static ISemanticRecorderLoggerFactory Instance { get; } = new NullSemanticRecorderLoggerFactory();

    private NullSemanticRecorderLoggerFactory() { }

    ISemanticRecorderLogger<TCategoryName> ISemanticRecorderLoggerFactory.Create<TCategoryName>() => NullSemanticRecorderLogger<TCategoryName>.Instance;
    ISemanticRecorderLogger<TCategoryName> ISemanticRecorderLoggerFactory.Create<TCategoryName>(ILogger<TCategoryName> logger) => NullSemanticRecorderLogger<TCategoryName>.Instance;
}
