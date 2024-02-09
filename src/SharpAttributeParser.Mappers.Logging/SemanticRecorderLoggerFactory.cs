namespace SharpAttributeParser.Mappers.Logging;

using Microsoft.Extensions.Logging;

using System;

/// <inheritdoc cref="ISemanticRecorderLoggerFactory"/>
public sealed class SemanticRecorderLoggerFactory : ISemanticRecorderLoggerFactory
{
    private readonly ILoggerFactory LoggerFactory;

    /// <summary>Instantiates a <see cref="SemanticRecorderLoggerFactory"/>, handling creation of <see cref="ISemanticRecorderLogger"/>.</summary>
    /// <param name="loggerFactory">Handles creation of loggers.</param>
    public SemanticRecorderLoggerFactory(ILoggerFactory loggerFactory)
    {
        LoggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    ISemanticRecorderLogger<TCategoryName> ISemanticRecorderLoggerFactory.Create<TCategoryName>()
    {
        var logger = LoggerFactory.CreateLogger<TCategoryName>();

        return Create(logger);
    }

    ISemanticRecorderLogger<TCategoryName> ISemanticRecorderLoggerFactory.Create<TCategoryName>(ILogger<TCategoryName> logger)
    {
        if (logger is null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        return Create(logger);
    }

    private static ISemanticRecorderLogger<TCategoryName> Create<TCategoryName>(ILogger<TCategoryName> logger) => new SemanticRecorderLogger<TCategoryName>(logger);
}
