namespace Attribinter.Mappers.BoolDelegateMappedArgumentRecorderFactoryCases.MappedArgumentRecorderCases;

using Moq;

internal static class RecorderFixtureFactory
{
    public static IRecorderFixture<TRecord, TData> Create<TRecord, TData>()
    {
        IBoolDelegateMappedArgumentRecorderFactory factory = new BoolDelegateMappedArgumentRecorderFactory();

        Mock<DAttemptingArgumentRecorder<TRecord, TData>> recorderDelegateMock = new();

        var sut = factory.Create(recorderDelegateMock.Object);

        return new RecorderFixture<TRecord, TData>(sut, recorderDelegateMock);
    }

    private sealed class RecorderFixture<TRecord, TData> : IRecorderFixture<TRecord, TData>
    {
        private readonly IMappedArgumentRecorder<TRecord, TData> Sut;

        private readonly Mock<DAttemptingArgumentRecorder<TRecord, TData>> RecorderDelegateMock;

        public RecorderFixture(IMappedArgumentRecorder<TRecord, TData> sut, Mock<DAttemptingArgumentRecorder<TRecord, TData>> recorderDelegateMock)
        {
            Sut = sut;

            RecorderDelegateMock = recorderDelegateMock;
        }

        IMappedArgumentRecorder<TRecord, TData> IRecorderFixture<TRecord, TData>.Sut => Sut;

        Mock<DAttemptingArgumentRecorder<TRecord, TData>> IRecorderFixture<TRecord, TData>.RecorderDelegateMock => RecorderDelegateMock;
    }
}
