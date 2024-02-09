namespace SharpAttributeParser.Mappers.Logging;

using Microsoft.Extensions.Logging;

/// <summary>A <see cref="ISyntacticRecorderLoggerFactory"/> creating <see cref="ISyntacticRecorderLogger"/> with no behaviour.</summary>
public sealed class NullSyntacticRecorderLoggerFactory : ISyntacticRecorderLoggerFactory
{
    /// <summary>A <see cref="ISyntacticRecorderLoggerFactory"/> creating <see cref="ISyntacticRecorderLogger"/> with no behaviour.</summary>
    public static ISyntacticRecorderLoggerFactory Instance { get; } = new NullSyntacticRecorderLoggerFactory();

    private NullSyntacticRecorderLoggerFactory() { }

    ISyntacticRecorderLogger<TCategoryName> ISyntacticRecorderLoggerFactory.Create<TCategoryName>() => NullSyntacticRecorderLogger<TCategoryName>.Instance;
    ISyntacticRecorderLogger<TCategoryName> ISyntacticRecorderLoggerFactory.Create<TCategoryName>(ILogger<TCategoryName> logger) => NullSyntacticRecorderLogger<TCategoryName>.Instance;
}
