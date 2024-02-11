namespace SharpAttributeParser.Mappers.SemanticRecorderFactoryCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private static ISemanticRecorder Target(ISemanticRecorderFactory factory, ISemanticMapper mapper) => factory.Create(mapper);

    private readonly FactoryContext Context = FactoryContext.Create();

    [Fact]
    public void NullMapper_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.Factory, null!));

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

        Mock<ISemanticMapper> mapperMock = new() { DefaultValue = DefaultValue.Mock };

        var recorder = Target(Context.Factory, mapperMock.Object);

        recorder.Type.TryRecordArgument(typeParameter, typeArgument);
        recorder.Constructor.TryRecordArgument(constructorParameter, constructorArgument);
        recorder.Named.TryRecordArgument(namedParameterName, namedArgument);

        mapperMock.Verify((mapper) => mapper.Type.TryMapParameter(typeParameter)!.TryRecordArgument(typeArgument), Times.Once);
        mapperMock.Verify((mapper) => mapper.Constructor.TryMapParameter(constructorParameter)!.TryRecordArgument(constructorArgument), Times.Once);
        mapperMock.Verify((mapper) => mapper.Named.TryMapParameter(namedParameterName)!.TryRecordArgument(namedArgument), Times.Once);
    }
}
