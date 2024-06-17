namespace Paraminter.Recorders.Mappers;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        VoidDelegateMappedArgumentDataRecorderFactory sut = new();

        return new Fixture(sut);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly IVoidDelegateMappedArgumentDataRecorderFactory Sut;

        public Fixture(
            IVoidDelegateMappedArgumentDataRecorderFactory sut)
        {
            Sut = sut;
        }

        IVoidDelegateMappedArgumentDataRecorderFactory IFixture.Sut => Sut;
    }
}
