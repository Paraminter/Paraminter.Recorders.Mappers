namespace Attribinter.Mappers.ArgumentRecorderFactoryCases.ArgumentRecorderCases;

using Moq;

internal interface IRecorderFixture<TParameter, TRecord, TData>
    where TRecord : class
{
    public abstract IArgumentRecorder<TParameter, TData> Sut { get; }

    public abstract Mock<IParameterMapper<TParameter, TRecord, TData>> MapperMock { get; }
    public abstract Mock<TRecord> DataRecordMock { get; }
}
