namespace Attribinter.Mappers.MappedArgumentRecorderFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Create_Func
{
    private static IMappedArgumentRecorder<TRecord, TData> Target<TRecord, TData>(Func<TRecord, TData, bool> recorderDelegate) => Context.Factory.Create(recorderDelegate);
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
        var recorderDelegate = Mock.Of<Func<object, object, bool>>();

        Context.FactoryProviderMock.Setup(static (provider) => provider.BoolDelegateFactory.Create(It.IsAny<Func<object, object, bool>>())).Returns(recorder);

        var actual = Target(recorderDelegate);

        Assert.Same(recorder, actual);

        Context.FactoryProviderMock.Verify((provider) => provider.BoolDelegateFactory.Create(recorderDelegate), Times.Once());
        Context.FactoryProviderMock.VerifyNoOtherCalls();
    }
}
