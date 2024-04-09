namespace Attribinter.Mappers.MappedArgumentRecorderFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Create_DCertainArgumentRecorder
{
    private IMappedArgumentRecorder<TRecord, TData> Target<TRecord, TData>(DCertainArgumentRecorder<TRecord, TData> recorderDelegate) => Fixture.Sut.Create(recorderDelegate);
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
        var recorder = Mock.Of<IMappedArgumentRecorder<object, object>>();
        var recorderDelegate = Mock.Of<DCertainArgumentRecorder<object, object>>();

        Fixture.FactoryProviderMock.Setup(static (provider) => provider.VoidDelegateFactory.Create(It.IsAny<DCertainArgumentRecorder<object, object>>())).Returns(recorder);

        var result = Target(recorderDelegate);

        Assert.Same(recorder, result);

        Fixture.FactoryProviderMock.Verify((provider) => provider.VoidDelegateFactory.Create(recorderDelegate), Times.Once());
    }
}
