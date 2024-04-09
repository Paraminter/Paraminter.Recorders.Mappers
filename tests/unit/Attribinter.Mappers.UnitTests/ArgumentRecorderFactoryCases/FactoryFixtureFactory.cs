namespace Attribinter.Mappers.ArgumentRecorderFactoryCases;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        ArgumentRecorderFactory sut = new();

        return new FactoryFixture(sut);
    }

    private sealed class FactoryFixture : IFactoryFixture
    {
        private readonly IArgumentRecorderFactory Sut;

        public FactoryFixture(IArgumentRecorderFactory sut)
        {
            Sut = sut;
        }

        IArgumentRecorderFactory IFactoryFixture.Sut => Sut;
    }
}
