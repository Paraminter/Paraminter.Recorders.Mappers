namespace Attribinter.Mappers.VoidDelegateMappedArgumentRecorderFactoryCases.MappedArgumentRecorderCases;

using Moq;

internal static class RecorderFixtureFactory
{
    public static IRecorderFixture<TRecord, TData> Create<TRecord, TData>()
    {
        IVoidDelegateMappedArgumentRecorderFactory factory = new VoidDelegateMappedArgumentRecorderFactory();

        Mock<DCertainArgumentRecorder<TRecord, TData>> recorderDelegateMock = new();

        var sut = factory.Create(recorderDelegateMock.Object);

        return new RecorderFixture<TRecord, TData>(sut, recorderDelegateMock);
    }

    private sealed class RecorderFixture<TRecord, TData> : IRecorderFixture<TRecord, TData>
    {
        private readonly IMappedArgumentRecorder<TRecord, TData> Sut;

        private readonly Mock<DCertainArgumentRecorder<TRecord, TData>> RecorderDelegateMock;

        public RecorderFixture(IMappedArgumentRecorder<TRecord, TData> sut, Mock<DCertainArgumentRecorder<TRecord, TData>> recorderDelegateMock)
        {
            Sut = sut;

            RecorderDelegateMock = recorderDelegateMock;
        }

        IMappedArgumentRecorder<TRecord, TData> IRecorderFixture<TRecord, TData>.Sut => Sut;

        Mock<DCertainArgumentRecorder<TRecord, TData>> IRecorderFixture<TRecord, TData>.RecorderDelegateMock => RecorderDelegateMock;
    }
}
