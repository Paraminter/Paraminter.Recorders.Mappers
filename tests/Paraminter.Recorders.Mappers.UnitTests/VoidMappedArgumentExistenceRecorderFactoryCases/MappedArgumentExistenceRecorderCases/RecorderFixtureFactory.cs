namespace Paraminter.Recorders.Mappers.VoidDelegateMappedArgumentExistenceRecorderFactoryCases.MappedArgumentExistenceRecorderCases;

using Moq;

internal static class RecorderFixtureFactory
{
    public static IRecorderFixture<TRecord> Create<TRecord>()
    {
        IVoidDelegateMappedArgumentExistenceRecorderFactory factory = new VoidDelegateMappedArgumentExistenceRecorderFactory();

        Mock<DVoidArgumentExistenceRecorder<TRecord>> recorderDelegateMock = new() { DefaultValue = DefaultValue.Mock };

        var sut = factory.Create(recorderDelegateMock.Object);

        return new RecorderFixture<TRecord>(sut, recorderDelegateMock);
    }

    private sealed class RecorderFixture<TRecord>
        : IRecorderFixture<TRecord>
    {
        private readonly IMappedArgumentExistenceRecorder<TRecord> Sut;

        private readonly Mock<DVoidArgumentExistenceRecorder<TRecord>> RecorderDelegateMock;

        public RecorderFixture(
            IMappedArgumentExistenceRecorder<TRecord> sut,
            Mock<DVoidArgumentExistenceRecorder<TRecord>> recorderDelegateMock)
        {
            Sut = sut;

            RecorderDelegateMock = recorderDelegateMock;
        }

        IMappedArgumentExistenceRecorder<TRecord> IRecorderFixture<TRecord>.Sut => Sut;

        Mock<DVoidArgumentExistenceRecorder<TRecord>> IRecorderFixture<TRecord>.RecorderDelegateMock => RecorderDelegateMock;
    }
}
