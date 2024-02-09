namespace SharpAttributeParser.Mappers.Logging;

using Microsoft.Extensions.Logging;

/// <summary>A <see cref="IRecorderLoggerFactory"/> creating <see cref="IRecorderLogger"/> with no behaviour.</summary>
public sealed class NullRecorderLoggerFactory : IRecorderLoggerFactory
{
    /// <summary>A <see cref="IRecorderLoggerFactory"/> creating <see cref="IRecorderLogger"/> with no behaviour.</summary>
    public static IRecorderLoggerFactory Instance { get; } = new NullRecorderLoggerFactory();

    private NullRecorderLoggerFactory() { }

    IRecorderLogger<TCategoryName> IRecorderLoggerFactory.Create<TCategoryName>() => NullRecorderLogger<TCategoryName>.Instance;
    IRecorderLogger<TCategoryName> IRecorderLoggerFactory.Create<TCategoryName>(ILogger<TCategoryName> logger) => NullRecorderLogger<TCategoryName>.Instance;
}
