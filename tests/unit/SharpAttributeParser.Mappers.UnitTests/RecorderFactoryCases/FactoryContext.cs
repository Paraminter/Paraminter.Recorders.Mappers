namespace SharpAttributeParser.Mappers.RecorderFactoryCases;

using Moq;

using SharpAttributeParser.Mappers.Logging;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        Mock<IRecorderLoggerFactory> loggerFactoryMock = new() { DefaultValue = DefaultValue.Mock };

        RecorderFactory factory = new(loggerFactoryMock.Object);

        return new(factory, loggerFactoryMock);
    }

    public RecorderFactory Factory { get; }

    public Mock<IRecorderLoggerFactory> LoggerFactoryMock { get; }

    private FactoryContext(RecorderFactory factory, Mock<IRecorderLoggerFactory> loggerFactoryMock)
    {
        Factory = factory;

        LoggerFactoryMock = loggerFactoryMock;
    }
}
