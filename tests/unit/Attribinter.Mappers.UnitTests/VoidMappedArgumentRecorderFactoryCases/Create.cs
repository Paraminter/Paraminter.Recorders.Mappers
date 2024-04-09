namespace Attribinter.Mappers.VoidDelegateMappedArgumentRecorderFactoryCases;

using System;

using Xunit;

public sealed class Create
{
    private IMappedArgumentRecorder<TRecord, TData> Target<TRecord, TData>(DCertainArgumentRecorder<TRecord, TData> recorderDelegate) => Fixture.Sut.Create(recorderDelegate);

    private readonly IFactoryFixture Fixture = FactoryFixtureFactory.Create();

    [Fact]
    public void NullRecorderDelegate_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<object, object>(null!));

        Assert.IsType<ArgumentNullException>(result);
    }
}
