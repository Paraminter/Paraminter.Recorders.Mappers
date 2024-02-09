namespace SharpAttributeParser.Mappers.Logging;

using Microsoft.Extensions.Logging;

using System;

/// <inheritdoc cref="ISyntacticRecorderLoggerFactory"/>
public sealed class SyntacticRecorderLoggerFactory : ISyntacticRecorderLoggerFactory
{
    private readonly ILoggerFactory LoggerFactory;

    /// <summary>Instantiates a <see cref="SyntacticRecorderLoggerFactory"/>, handling creation of <see cref="ISyntacticRecorderLogger"/>.</summary>
    /// <param name="loggerFactory">Handles creation of loggers.</param>
    public SyntacticRecorderLoggerFactory(ILoggerFactory loggerFactory)
    {
        LoggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    ISyntacticRecorderLogger<TCategoryName> ISyntacticRecorderLoggerFactory.Create<TCategoryName>()
    {
        var logger = LoggerFactory.CreateLogger<TCategoryName>();

        return Create(logger);
    }

    ISyntacticRecorderLogger<TCategoryName> ISyntacticRecorderLoggerFactory.Create<TCategoryName>(ILogger<TCategoryName> logger)
    {
        if (logger is null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        return Create(logger);
    }

    private static ISyntacticRecorderLogger<TCategoryName> Create<TCategoryName>(ILogger<TCategoryName> logger) => new SyntacticRecorderLogger<TCategoryName>(logger);
}
