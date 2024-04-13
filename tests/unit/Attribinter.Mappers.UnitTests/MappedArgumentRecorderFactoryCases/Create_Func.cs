namespace Attribinter.Mappers.MappedArgumentRecorderFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Create_Func
{
    private IMappedArgumentRecorder<TRecord, TData> Target<TRecord, TData>(DAttemptingArgumentRecorder<TRecord, TData> recorderDelegate) => Fixture.Sut.Create(recorderDelegate);
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
        var recorderDelegate = Mock.Of<DAttemptingArgumentRecorder<object, object>>();

        Fixture.FactoryProviderMock.Setup(static (provider) => provider.BoolDelegateFactory.Create(It.IsAny<DAttemptingArgumentRecorder<object, object>>())).Returns(recorder);

        var result = Target(recorderDelegate);

        Assert.Same(recorder, result);
    }
}
