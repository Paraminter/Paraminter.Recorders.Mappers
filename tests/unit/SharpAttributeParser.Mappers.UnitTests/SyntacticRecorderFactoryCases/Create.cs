namespace SharpAttributeParser.Mappers.SyntacticRecorderFactoryCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private static ISyntacticRecorder Target(ISyntacticRecorderFactory factory, ISyntacticMapper mapper) => factory.Create(mapper);

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
        var normalConstructorParameter = Mock.Of<IParameterSymbol>();
        var paramsConstructorParameter = Mock.Of<IParameterSymbol>();
        var defaultConstructorParameter = Mock.Of<IParameterSymbol>();
        var namedParameterName = string.Empty;

        var typeSyntax = ExpressionSyntaxFactory.Create();
        var normalConstructorSyntax = ExpressionSyntaxFactory.Create();
        var paramsConstructorSyntax = new[] { ExpressionSyntaxFactory.Create() };
        var namedSyntax = ExpressionSyntaxFactory.Create();

        Mock<ISyntacticMapper> mapperMock = new() { DefaultValue = DefaultValue.Mock };

        var recorder = Target(Context.Factory, mapperMock.Object);

        recorder.Type.TryRecordArgument(typeParameter, typeSyntax);
        recorder.Constructor.Normal.TryRecordArgument(normalConstructorParameter, normalConstructorSyntax);
        recorder.Constructor.Params.TryRecordArgument(paramsConstructorParameter, paramsConstructorSyntax);
        recorder.Constructor.Default.TryRecordArgument(defaultConstructorParameter);
        recorder.Named.TryRecordArgument(namedParameterName, namedSyntax);

        mapperMock.Verify((mapper) => mapper.Type.MapParameter(typeParameter).TryRecordArgument(typeSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.Constructor.Normal.MapParameter(normalConstructorParameter).TryRecordArgument(normalConstructorSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.Constructor.Params.MapParameter(paramsConstructorParameter).TryRecordArgument(paramsConstructorSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.Constructor.Default.MapParameter(defaultConstructorParameter).TryRecordArgument(), Times.Once);
        mapperMock.Verify((mapper) => mapper.Named.MapParameter(namedParameterName).TryRecordArgument(namedSyntax), Times.Once);
    }
}
