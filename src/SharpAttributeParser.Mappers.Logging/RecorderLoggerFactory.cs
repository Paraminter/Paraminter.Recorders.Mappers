namespace SharpAttributeParser.Mappers.Logging;

using Microsoft.Extensions.Logging;

using System;

/// <inheritdoc cref="IRecorderLoggerFactory"/>
public sealed class RecorderLoggerFactory : IRecorderLoggerFactory
{
    private readonly ILoggerFactory LoggerFactory;

    /// <summary>Instantiates a <see cref="RecorderLoggerFactory"/>, handling creation of <see cref="IRecorderLogger"/>.</summary>
    /// <param name="loggerFactory">Handles creation of loggers.</param>
    public RecorderLoggerFactory(ILoggerFactory loggerFactory)
    {
        LoggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    IRecorderLogger<TCategoryName> IRecorderLoggerFactory.Create<TCategoryName>()
    {
        var logger = LoggerFactory.CreateLogger<TCategoryName>();

        return Create(logger);
    }

    IRecorderLogger<TCategoryName> IRecorderLoggerFactory.Create<TCategoryName>(ILogger<TCategoryName> logger)
    {
        if (logger is null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        return Create(logger);
    }

    private static IRecorderLogger<TCategoryName> Create<TCategoryName>(ILogger<TCategoryName> logger) => new RecorderLogger<TCategoryName>(logger);
}
