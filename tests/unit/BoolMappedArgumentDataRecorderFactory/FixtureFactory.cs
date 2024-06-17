namespace Paraminter.Recorders.Mappers;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        BoolDelegateMappedArgumentDataRecorderFactory sut = new();

        return new Fixture(sut);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly IBoolDelegateMappedArgumentDataRecorderFactory Sut;

        public Fixture(
            IBoolDelegateMappedArgumentDataRecorderFactory sut)
        {
            Sut = sut;
        }

        IBoolDelegateMappedArgumentDataRecorderFactory IFixture.Sut => Sut;
    }
}
