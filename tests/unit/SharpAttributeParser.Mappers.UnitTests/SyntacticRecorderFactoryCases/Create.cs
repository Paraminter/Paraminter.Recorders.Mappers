namespace SharpAttributeParser.Mappers.SyntacticRecorderFactoryCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private static ISyntacticRecorder<TRecord> Target<TRecord>(ISyntacticRecorderFactory factory, ISyntacticMapper<TRecord> mapper, TRecord dataRecord) => factory.Create(mapper, dataRecord);

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

        var exception = Record.Exception(() => Target(context.Factory, Mock.Of<ISyntacticMapper<object>>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidMapperAndRecord_ConstructedRecorderUsesMapperAndRecord()
    {
        var typeParameter = Mock.Of<ITypeParameterSymbol>();
        var constructorParameter = Mock.Of<IParameterSymbol>();
        var namedParameterName = string.Empty;

        var typeSyntax = ExpressionSyntaxFactory.Create();
        var constructorSyntax = ExpressionSyntaxFactory.Create();
        var namedSyntax = ExpressionSyntaxFactory.Create();

        var dataRecord = Mock.Of<object>();

        Mock<ISyntacticMapper<object>> mapperMock = new() { DefaultValue = DefaultValue.Mock };

        var context = FactoryContext.Create();

        var recorder = Target(context.Factory, mapperMock.Object, dataRecord);

        recorder.Type.TryRecordArgument(typeParameter, typeSyntax);
        recorder.Constructor.TryRecordArgument(constructorParameter, constructorSyntax);
        recorder.Named.TryRecordArgument(namedParameterName, namedSyntax);

        var builtDataRecord = recorder.BuildRecord();

        mapperMock.Verify((mapper) => mapper.TryMapTypeParameter(typeParameter, dataRecord)!.TryRecordArgument(typeSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.TryMapConstructorParameter(constructorParameter, dataRecord)!.TryRecordArgument(constructorSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.TryMapNamedParameter(namedParameterName, dataRecord)!.TryRecordArgument(namedSyntax), Times.Once);

        Assert.Same(dataRecord, builtDataRecord);
    }
}
