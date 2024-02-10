namespace SharpAttributeParser.Mappers.RecorderFactoryCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private static IRecorder Target<TRecord>(IRecorderFactory factory, IMapper<TRecord> mapper, TRecord dataRecord) => factory.Create(mapper, dataRecord);

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

        var exception = Record.Exception(() => Target(context.Factory, Mock.Of<IMapper<object>>(), null!));

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

        var typeSyntax = ExpressionSyntaxFactory.Create();
        var constructorSyntax = ExpressionSyntaxFactory.Create();
        var namedSyntax = ExpressionSyntaxFactory.Create();

        var dataRecord = Mock.Of<object>();

        Mock<IMapper<object>> mapperMock = new() { DefaultValue = DefaultValue.Mock };

        var context = FactoryContext.Create();

        var recorder = Target(context.Factory, mapperMock.Object, dataRecord);

        recorder.Type.TryRecordArgument(typeParameter, typeArgument, typeSyntax);
        recorder.Constructor.TryRecordArgument(constructorParameter, constructorArgument, constructorSyntax);
        recorder.Named.TryRecordArgument(namedParameterName, namedArgument, namedSyntax);

        mapperMock.Verify((mapper) => mapper.TryMapTypeParameter(typeParameter, dataRecord)!.TryRecordArgument(typeArgument, typeSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.TryMapConstructorParameter(constructorParameter, dataRecord)!.TryRecordArgument(constructorArgument, constructorSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.TryMapNamedParameter(namedParameterName, dataRecord)!.TryRecordArgument(namedArgument, namedSyntax), Times.Once);
    }
}
