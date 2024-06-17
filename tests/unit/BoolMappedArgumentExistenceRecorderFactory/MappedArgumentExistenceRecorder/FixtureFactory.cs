namespace Paraminter.Recorders.Mappers.MappedArgumentExistenceRecorder;

using Moq;

internal static class FixtureFactory
{
    public static IFixture<TRecord> Create<TRecord>()
    {
        IBoolDelegateMappedArgumentExistenceRecorderFactory factory = new BoolDelegateMappedArgumentExistenceRecorderFactory();

        Mock<DBoolArgumentExistenceRecorder<TRecord>> recorderDelegateMock = new() { DefaultValue = DefaultValue.Mock };

        var sut = factory.Create(recorderDelegateMock.Object);

        return new Fixture<TRecord>(sut, recorderDelegateMock);
    }

    private sealed class Fixture<TRecord>
        : IFixture<TRecord>
    {
        private readonly IMappedArgumentExistenceRecorder<TRecord> Sut;

        private readonly Mock<DBoolArgumentExistenceRecorder<TRecord>> RecorderDelegateMock;

        public Fixture(
            IMappedArgumentExistenceRecorder<TRecord> sut,
            Mock<DBoolArgumentExistenceRecorder<TRecord>> recorderDelegateMock)
        {
            Sut = sut;

            RecorderDelegateMock = recorderDelegateMock;
        }

        IMappedArgumentExistenceRecorder<TRecord> IFixture<TRecord>.Sut => Sut;

        Mock<DBoolArgumentExistenceRecorder<TRecord>> IFixture<TRecord>.RecorderDelegateMock => RecorderDelegateMock;
    }
}
