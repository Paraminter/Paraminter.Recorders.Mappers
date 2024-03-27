namespace Attribinter.Mappers.VoidDelegateMappedArgumentRecorderFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private static IMappedArgumentRecorder<TRecord, TData> Target<TRecord, TData>(Action<TRecord, TData> recorderDelegate) => Context.Factory.Create(recorderDelegate);

    private static readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void NullRecorderDelegate_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target<object, object>(null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidRecorderDelegate_ConstructedRecorderUsesDelegate()
    {
        Mock<Action<object, object>> recorderDelegateMock = new();

        var parameter = Mock.Of<object>();
        var argument = Mock.Of<object>();

        var recorder = Target(recorderDelegateMock.Object);

        recorder.TryRecordData(parameter, argument);

        recorderDelegateMock.Verify((recorder) => recorder.Invoke(parameter, argument), Times.Once());
    }
}
