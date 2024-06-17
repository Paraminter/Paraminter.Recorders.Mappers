namespace Paraminter.Recorders.Mappers;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        BoolDelegateMappedArgumentExistenceRecorderFactory sut = new();

        return new Fixture(sut);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly IBoolDelegateMappedArgumentExistenceRecorderFactory Sut;

        public Fixture(
            IBoolDelegateMappedArgumentExistenceRecorderFactory sut)
        {
            Sut = sut;
        }

        IBoolDelegateMappedArgumentExistenceRecorderFactory IFixture.Sut => Sut;
    }
}
