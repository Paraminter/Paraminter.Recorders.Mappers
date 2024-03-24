namespace Attribinter.Mappers.ArgumentRecorderFactoryCases.T3;

using Moq;

internal sealed class FactoryContext<TParameter, TRecord, TData>
{
    public static FactoryContext<TParameter, TRecord, TData> Create()
    {
        Mock<IArgumentRecorderFactory> innerFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<IParameterMapperProvider<TParameter, TRecord, TData>> mapperProviderMock = new() { DefaultValue = DefaultValue.Mock };

        ArgumentRecorderFactory<TParameter, TRecord, TData> factory = new(innerFactoryMock.Object, mapperProviderMock.Object);

        return new(factory, innerFactoryMock, mapperProviderMock);
    }

    public IArgumentRecorderFactory<TParameter, TRecord, TData> Factory { get; }

    public Mock<IArgumentRecorderFactory> InnerFactoryMock { get; }
    public Mock<IParameterMapperProvider<TParameter, TRecord, TData>> MapperProviderMock { get; }

    private FactoryContext(IArgumentRecorderFactory<TParameter, TRecord, TData> factory, Mock<IArgumentRecorderFactory> innerFactoryMock, Mock<IParameterMapperProvider<TParameter, TRecord, TData>> mapperProviderMock)
    {
        Factory = factory;

        InnerFactoryMock = innerFactoryMock;
        MapperProviderMock = mapperProviderMock;
    }
}
