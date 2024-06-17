namespace Paraminter.Recorders.Mappers;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        ArgumentExistenceRecorderFactory sut = new();

        return new Fixture(sut);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly IArgumentExistenceRecorderFactory Sut;

        public Fixture(
            IArgumentExistenceRecorderFactory sut)
        {
            Sut = sut;
        }

        IArgumentExistenceRecorderFactory IFixture.Sut => Sut;
    }
}
