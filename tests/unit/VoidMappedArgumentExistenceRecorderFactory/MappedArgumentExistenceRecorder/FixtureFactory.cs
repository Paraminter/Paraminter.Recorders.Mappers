namespace Paraminter.Recorders.Mappers.MappedArgumentExistenceRecorder;

using Moq;

internal static class FixtureFactory
{
    public static IFixture<TRecord> Create<TRecord>()
    {
        IVoidDelegateMappedArgumentExistenceRecorderFactory factory = new VoidDelegateMappedArgumentExistenceRecorderFactory();

        Mock<DVoidArgumentExistenceRecorder<TRecord>> recorderDelegateMock = new() { DefaultValue = DefaultValue.Mock };

        var sut = factory.Create(recorderDelegateMock.Object);

        return new Fixture<TRecord>(sut, recorderDelegateMock);
    }

    private sealed class Fixture<TRecord>
        : IFixture<TRecord>
    {
        private readonly IMappedArgumentExistenceRecorder<TRecord> Sut;

        private readonly Mock<DVoidArgumentExistenceRecorder<TRecord>> RecorderDelegateMock;

        public Fixture(
            IMappedArgumentExistenceRecorder<TRecord> sut,
            Mock<DVoidArgumentExistenceRecorder<TRecord>> recorderDelegateMock)
        {
            Sut = sut;

            RecorderDelegateMock = recorderDelegateMock;
        }

        IMappedArgumentExistenceRecorder<TRecord> IFixture<TRecord>.Sut => Sut;

        Mock<DVoidArgumentExistenceRecorder<TRecord>> IFixture<TRecord>.RecorderDelegateMock => RecorderDelegateMock;
    }
}
