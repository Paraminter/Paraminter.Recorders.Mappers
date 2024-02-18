namespace SharpAttributeParser.Mappers.SyntacticRecorderFactoryCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private static ISyntacticRecorder Target<TRecord>(ISyntacticRecorderFactory factory, ISyntacticMapper<TRecord> mapper, TRecord dataRecord) => factory.Create(mapper, dataRecord);

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
        var exception = Record.Exception(() => Target(Context.Factory, Mock.Of<ISyntacticMapper<object>>(), null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void ValidMapperAndRecord_ConstructedRecorderUsesMapperAndRecord()
    {
        var dataRecord = Mock.Of<object>();

        var typeParameter = Mock.Of<ITypeParameterSymbol>();
        var normalConstructorParameter = Mock.Of<IParameterSymbol>();
        var paramsConstructorParameter = Mock.Of<IParameterSymbol>();
        var defaultConstructorParameter = Mock.Of<IParameterSymbol>();
        var namedParameterName = string.Empty;

        var typeSyntax = ExpressionSyntaxFactory.Create();
        var normalConstructorSyntax = ExpressionSyntaxFactory.Create();
        var paramsConstructorSyntax = new[] { ExpressionSyntaxFactory.Create() };
        var namedSyntax = ExpressionSyntaxFactory.Create();

        Mock<ISyntacticMapper<object>> mapperMock = new() { DefaultValue = DefaultValue.Mock };

        var recorder = Target(Context.Factory, mapperMock.Object, dataRecord);

        recorder.Type.TryRecordArgument(typeParameter, typeSyntax);
        recorder.Constructor.Normal.TryRecordArgument(normalConstructorParameter, normalConstructorSyntax);
        recorder.Constructor.Params.TryRecordArgument(paramsConstructorParameter, paramsConstructorSyntax);
        recorder.Constructor.Default.TryRecordArgument(defaultConstructorParameter);
        recorder.Named.TryRecordArgument(namedParameterName, namedSyntax);

        mapperMock.Verify((mapper) => mapper.Type.MapParameter(typeParameter).TryRecordArgument(dataRecord, typeSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.Constructor.Normal.MapParameter(normalConstructorParameter).TryRecordArgument(dataRecord, normalConstructorSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.Constructor.Params.MapParameter(paramsConstructorParameter).TryRecordArgument(dataRecord, paramsConstructorSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.Constructor.Default.MapParameter(defaultConstructorParameter).TryRecordArgument(dataRecord), Times.Once);
        mapperMock.Verify((mapper) => mapper.Named.MapParameter(namedParameterName).TryRecordArgument(dataRecord, namedSyntax), Times.Once);
    }
}
