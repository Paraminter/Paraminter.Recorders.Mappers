namespace SharpAttributeParser.Mappers.SyntacticRecorderFactoryCases;

using Moq;

using SharpAttributeParser.Mappers.Logging;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        Mock<ISyntacticRecorderLoggerFactory> loggerFactoryMock = new() { DefaultValue = DefaultValue.Mock };

        SyntacticRecorderFactory factory = new(loggerFactoryMock.Object);

        return new(factory, loggerFactoryMock);
    }

    public SyntacticRecorderFactory Factory { get; }

    public Mock<ISyntacticRecorderLoggerFactory> LoggerFactoryMock { get; }

    private FactoryContext(SyntacticRecorderFactory factory, Mock<ISyntacticRecorderLoggerFactory> loggerFactoryMock)
    {
        Factory = factory;

        LoggerFactoryMock = loggerFactoryMock;
    }
}
