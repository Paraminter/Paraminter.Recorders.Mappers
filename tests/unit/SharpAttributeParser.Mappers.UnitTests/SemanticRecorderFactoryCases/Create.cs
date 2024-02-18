namespace SharpAttributeParser.Mappers.SemanticRecorderFactoryCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private static ISemanticRecorder Target<TRecord>(ISemanticRecorderFactory factory, ISemanticMapper<TRecord> mapper, TRecord dataRecord) => factory.Create(mapper, dataRecord);

    private readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void NullMapper_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Factory, null!, Mock.Of<object>()));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void NullDataRecord_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Factory, Mock.Of<ISemanticMapper<object>>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidMapperAndRecord_ConstructedRecorderUsesMapperAndRecord()
    {
        var dataRecord = Mock.Of<object>();

        var typeParameter = Mock.Of<ITypeParameterSymbol>();
        var constructorParameter = Mock.Of<IParameterSymbol>();
        var namedParameterName = string.Empty;

        var typeArgument = Mock.Of<ITypeSymbol>();
        var constructorArgument = Mock.Of<object>();
        var namedArgument = Mock.Of<object>();

        Mock<ISemanticMapper<object>> mapperMock = new() { DefaultValue = DefaultValue.Mock };

        var recorder = Target(Context.Factory, mapperMock.Object, dataRecord);

        recorder.Type.TryRecordArgument(typeParameter, typeArgument);
        recorder.Constructor.TryRecordArgument(constructorParameter, constructorArgument);
        recorder.Named.TryRecordArgument(namedParameterName, namedArgument);

        mapperMock.Verify((mapper) => mapper.Type.MapParameter(typeParameter).TryRecordArgument(dataRecord, typeArgument), Times.Once);
        mapperMock.Verify((mapper) => mapper.Constructor.MapParameter(constructorParameter).TryRecordArgument(dataRecord, constructorArgument), Times.Once);
        mapperMock.Verify((mapper) => mapper.Named.MapParameter(namedParameterName).TryRecordArgument(dataRecord, namedArgument), Times.Once);
    }
}
