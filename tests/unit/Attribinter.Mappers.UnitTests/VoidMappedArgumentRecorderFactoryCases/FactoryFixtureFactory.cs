namespace Attribinter.Mappers.VoidDelegateMappedArgumentRecorderFactoryCases;

internal static class FactoryFixtureFactory
{
    public static IFactoryFixture Create()
    {
        VoidDelegateMappedArgumentRecorderFactory sut = new();

        return new FactoryFixture(sut);
    }

    private sealed class FactoryFixture : IFactoryFixture
    {
        private readonly IVoidDelegateMappedArgumentRecorderFactory Sut;

        public FactoryFixture(IVoidDelegateMappedArgumentRecorderFactory sut)
        {
            Sut = sut;
        }

        IVoidDelegateMappedArgumentRecorderFactory IFactoryFixture.Sut => Sut;
    }
}
