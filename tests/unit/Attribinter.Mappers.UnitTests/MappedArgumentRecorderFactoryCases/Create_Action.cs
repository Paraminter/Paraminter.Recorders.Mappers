namespace Attribinter.Mappers.MappedArgumentRecorderFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Create_Action
{
    private static IMappedArgumentRecorder<TRecord, TData> Target<TRecord, TData>(Action<TRecord, TData> recorderDelegate) => Context.Factory.Create(recorderDelegate);
    private static readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void NullDelegate_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target<object, object>(null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidDelegate_ReturnsRecorder()
    {
        var recorder = Mock.Of<IMappedArgumentRecorder<object, object>>();
        var recorderDelegate = Mock.Of<Action<object, object>>();

        Context.FactoryProviderMock.Setup(static (provider) => provider.VoidDelegateFactory.Create(It.IsAny<Action<object, object>>())).Returns(recorder);

        var actual = Target(recorderDelegate);

        Assert.Same(recorder, actual);

        Context.FactoryProviderMock.Verify((provider) => provider.VoidDelegateFactory.Create(recorderDelegate), Times.Once());
        Context.FactoryProviderMock.VerifyNoOtherCalls();
    }
}
