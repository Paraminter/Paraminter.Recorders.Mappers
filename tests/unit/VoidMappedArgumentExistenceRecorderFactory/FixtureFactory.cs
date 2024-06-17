namespace Paraminter.Recorders.Mappers;

internal static class FixtureFactory
{
    public static IFixture Create()
    {
        VoidDelegateMappedArgumentExistenceRecorderFactory sut = new();

        return new Fixture(sut);
    }

    private sealed class Fixture
        : IFixture
    {
        private readonly IVoidDelegateMappedArgumentExistenceRecorderFactory Sut;

        public Fixture(
            IVoidDelegateMappedArgumentExistenceRecorderFactory sut)
        {
            Sut = sut;
        }

        IVoidDelegateMappedArgumentExistenceRecorderFactory IFixture.Sut => Sut;
    }
}
