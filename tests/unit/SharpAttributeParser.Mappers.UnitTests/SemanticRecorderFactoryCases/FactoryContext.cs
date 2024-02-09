namespace SharpAttributeParser.Mappers.SemanticRecorderFactoryCases;

using Moq;

using SharpAttributeParser.Mappers.Logging;

internal sealed class FactoryContext
{
    public static FactoryContext Create()
    {
        Mock<ISemanticRecorderLoggerFactory> loggerFactoryMock = new() { DefaultValue = DefaultValue.Mock };

        SemanticRecorderFactory factory = new(loggerFactoryMock.Object);

        return new(factory, loggerFactoryMock);
    }

    public SemanticRecorderFactory Factory { get; }

    public Mock<ISemanticRecorderLoggerFactory> LoggerFactoryMock { get; }

    private FactoryContext(SemanticRecorderFactory factory, Mock<ISemanticRecorderLoggerFactory> loggerFactoryMock)
    {
        Factory = factory;

        LoggerFactoryMock = loggerFactoryMock;
    }
}
