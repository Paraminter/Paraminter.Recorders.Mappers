namespace SharpAttributeParser.Mappers.SemanticRecorderFactoryCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private static ISemanticRecorder Target<TRecord>(ISemanticRecorderFactory factory, ISemanticMapper<TRecord> mapper, TRecord dataRecord) => factory.Create(mapper, dataRecord);

    [Fact]
    public void NullMapper_ArgumentNullException()
    {
        var context = FactoryContext.Create();

        var exception = Record.Exception(() => Target(context.Factory, null!, Mock.Of<object>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullRecord_ArgumentNullException()
    {
        var context = FactoryContext.Create();

        var exception = Record.Exception(() => Target(context.Factory, Mock.Of<ISemanticMapper<object>>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidMapperAndRecord_ConstructedRecorderUsesMapperAndRecord()
    {
        var typeParameter = Mock.Of<ITypeParameterSymbol>();
        var constructorParameter = Mock.Of<IParameterSymbol>();
        var namedParameterName = string.Empty;

        var typeArgument = Mock.Of<ITypeSymbol>();
        var constructorArgument = Mock.Of<object>();
        var namedArgument = Mock.Of<object>();

        var dataRecord = Mock.Of<object>();

        Mock<ISemanticMapper<object>> mapperMock = new() { DefaultValue = DefaultValue.Mock };

        var context = FactoryContext.Create();

        var recorder = Target(context.Factory, mapperMock.Object, dataRecord);

        recorder.Type.TryRecordArgument(typeParameter, typeArgument);
        recorder.Constructor.TryRecordArgument(constructorParameter, constructorArgument);
        recorder.Named.TryRecordArgument(namedParameterName, namedArgument);

        mapperMock.Verify((mapper) => mapper.Type.TryMapParameter(typeParameter, dataRecord)!.TryRecordArgument(typeArgument), Times.Once);
        mapperMock.Verify((mapper) => mapper.Constructor.TryMapParameter(constructorParameter, dataRecord)!.TryRecordArgument(constructorArgument), Times.Once);
        mapperMock.Verify((mapper) => mapper.Named.TryMapParameter(namedParameterName, dataRecord)!.TryRecordArgument(namedArgument), Times.Once);
    }
}
