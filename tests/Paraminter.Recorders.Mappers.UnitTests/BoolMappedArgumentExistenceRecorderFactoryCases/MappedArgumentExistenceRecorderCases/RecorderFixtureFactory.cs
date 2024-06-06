namespace Paraminter.Mappers.BoolDelegateMappedArgumentExistenceRecorderFactoryCases.MappedArgumentExistenceRecorderCases;

using Moq;

internal static class RecorderFixtureFactory
{
    public static IRecorderFixture<TRecord> Create<TRecord>()
    {
        IBoolDelegateMappedArgumentExistenceRecorderFactory factory = new BoolDelegateMappedArgumentExistenceRecorderFactory();

        Mock<DBoolArgumentExistenceRecorder<TRecord>> recorderDelegateMock = new(MockBehavior.Strict);

        var sut = factory.Create(recorderDelegateMock.Object);

        return new RecorderFixture<TRecord>(sut, recorderDelegateMock);
    }

    private sealed class RecorderFixture<TRecord>
        : IRecorderFixture<TRecord>
    {
        private readonly IMappedArgumentExistenceRecorder<TRecord> Sut;

        private readonly Mock<DBoolArgumentExistenceRecorder<TRecord>> RecorderDelegateMock;

        public RecorderFixture(
            IMappedArgumentExistenceRecorder<TRecord> sut,
            Mock<DBoolArgumentExistenceRecorder<TRecord>> recorderDelegateMock)
        {
            Sut = sut;

            RecorderDelegateMock = recorderDelegateMock;
        }

        IMappedArgumentExistenceRecorder<TRecord> IRecorderFixture<TRecord>.Sut => Sut;

        Mock<DBoolArgumentExistenceRecorder<TRecord>> IRecorderFixture<TRecord>.RecorderDelegateMock => RecorderDelegateMock;
    }
}
