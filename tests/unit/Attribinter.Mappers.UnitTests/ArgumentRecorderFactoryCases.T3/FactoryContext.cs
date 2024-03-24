namespace Attribinter.Mappers.ArgumentRecorderFactoryCases.T3;

using Moq;

internal sealed class FactoryContext<TParameter, TRecord, TData>
{
    public static FactoryContext<TParameter, TRecord, TData> Create()
    {
        Mock<IArgumentRecorderFactory> innerFactoryMock = new() { DefaultValue = DefaultValue.Mock };
        var mapper = Mock.Of<IParameterMapper<TParameter, TRecord, TData>>();

        ArgumentRecorderFactory<TParameter, TRecord, TData> factory = new(innerFactoryMock.Object, mapper);

        return new(factory, innerFactoryMock, mapper);
    }

    public IArgumentRecorderFactory<TParameter, TRecord, TData> Factory { get; }

    public Mock<IArgumentRecorderFactory> InnerFactoryMock { get; }
    public IParameterMapper<TParameter, TRecord, TData> Mapper { get; }

    private FactoryContext(IArgumentRecorderFactory<TParameter, TRecord, TData> factory, Mock<IArgumentRecorderFactory> innerFactoryMock, IParameterMapper<TParameter, TRecord, TData> mapper)
    {
        Factory = factory;

        InnerFactoryMock = innerFactoryMock;
        Mapper = mapper;
    }
}
