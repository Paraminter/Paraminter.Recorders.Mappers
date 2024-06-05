namespace Paraminter.Mappers.VoidDelegateMappedArgumentExistenceRecorderFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private readonly IFactoryFixture Fixture = FactoryFixtureFactory.Create();

    [Fact]
    public void NullRecorderDelegate_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<object>(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidRecorderDelegate_ReturnsRecorder()
    {
        var result = Target(Mock.Of<DVoidArgumentExistenceRecorder<object>>());

        Assert.NotNull(result);
    }

    private IMappedArgumentExistenceRecorder<TRecord> Target<TRecord>(
        DVoidArgumentExistenceRecorder<TRecord> recorderDelegate)
    {
        return Fixture.Sut.Create(recorderDelegate);
    }
}
