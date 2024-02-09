namespace SharpAttributeParser.Mappers.Logging;

using Microsoft.Extensions.Logging;

/// <summary>Handles construction of <see cref="ISyntacticRecorderLogger"/>.</summary>
public interface ISyntacticRecorderLoggerFactory
{
    /// <summary>Creates a logger.</summary>
    /// <typeparam name="TCategoryName">The name of the logging category.</typeparam>
    /// <returns>The created logger.</returns>
    public abstract ISyntacticRecorderLogger<TCategoryName> Create<TCategoryName>();

    /// <summary>Creates a logger.</summary>
    /// <typeparam name="TCategoryName">The name of the logging category.</typeparam>
    /// <param name="logger">The logger used to log messages.</param>
    /// <returns>The created logger.</returns>
    public abstract ISyntacticRecorderLogger<TCategoryName> Create<TCategoryName>(ILogger<TCategoryName> logger);
}
