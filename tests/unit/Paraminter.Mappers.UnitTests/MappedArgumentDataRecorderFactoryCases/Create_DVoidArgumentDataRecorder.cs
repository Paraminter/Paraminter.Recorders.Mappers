namespace Paraminter.Mappers.MappedArgumentDataRecorderFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Create_DVoidArgumentDataRecorder
{
    private IMappedArgumentDataRecorder<TRecord, TArgumentData> Target<TRecord, TArgumentData>(DVoidArgumentDataRecorder<TRecord, TArgumentData> recorderDelegate) => Fixture.Sut.Create(recorderDelegate);

    private readonly IFactoryFixture Fixture = FactoryFixtureFactory.Create();

    [Fact]
    public void NullDelegate_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<object, object>(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidDelegate_ReturnsRecorder()
    {
        var recorder = Mock.Of<IMappedArgumentDataRecorder<object, object>>();
        var recorderDelegate = Mock.Of<DVoidArgumentDataRecorder<object, object>>();

        Fixture.FactoryProviderMock.Setup((provider) => provider.VoidDelegateFactory.Create(recorderDelegate)).Returns(recorder);

        var result = Target(recorderDelegate);

        Assert.Same(recorder, result);
    }
}
