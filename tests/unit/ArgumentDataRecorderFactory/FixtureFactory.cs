namespace Paraminter.Recorders.Mappers;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        ArgumentDataRecorderFactory sut = new();

        return new Fixture(sut);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly IArgumentDataRecorderFactory Sut;

        public Fixture(
            IArgumentDataRecorderFactory sut)
        {
            Sut = sut;
        }

        IArgumentDataRecorderFactory IFixture.Sut => Sut;
    }
}
