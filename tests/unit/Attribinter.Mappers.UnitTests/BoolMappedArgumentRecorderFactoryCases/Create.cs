namespace Attribinter.Mappers.BoolDelegateMappedArgumentRecorderFactoryCases;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private IMappedArgumentRecorder<TRecord, TData> Target<TRecord, TData>(Func<TRecord, TData, bool> recorderDelegate) => Target(Context.Factory, recorderDelegate);
    private static IMappedArgumentRecorder<TRecord, TData> Target<TRecord, TData>(IBoolDelegateMappedArgumentRecorderFactory factory, Func<TRecord, TData, bool> recorderDelegate) => factory.Create(recorderDelegate);

    private readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void NullRecorderDelegate_ThrowsArgumentNullException()
    {
        var exception = Record.Exception(() => Target<object, object>(null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidRecorderDelegate_ConstructedRecorderUsesDelegate()
    {
        Mock<Func<object, object, bool>> recorderDelegateMock = new();

        var parameter = Mock.Of<object>();
        var argument = Mock.Of<object>();

        var recorder = Target(recorderDelegateMock.Object);

        recorder.TryRecordData(parameter, argument);

        recorderDelegateMock.Verify((recorder) => recorder.Invoke(parameter, argument), Times.Once());
    }
}
