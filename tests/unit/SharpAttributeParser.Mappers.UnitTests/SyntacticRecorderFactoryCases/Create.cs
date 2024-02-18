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
        var constructorParameter = Mock.Of<IParameterSymbol>();
        var namedParameterName = string.Empty;

        var typeSyntax = ExpressionSyntaxFactory.Create();
        var constructorSyntax = ExpressionSyntaxFactory.Create();
        var namedSyntax = ExpressionSyntaxFactory.Create();

        Mock<ISyntacticMapper> mapperMock = new() { DefaultValue = DefaultValue.Mock };

        var recorder = Target(Context.Factory, mapperMock.Object);

        recorder.Type.TryRecordArgument(typeParameter, typeSyntax);
        recorder.Constructor.Normal.TryRecordArgument(constructorParameter, constructorSyntax);
        recorder.Named.TryRecordArgument(namedParameterName, namedSyntax);

        mapperMock.Verify((mapper) => mapper.Type.MapParameter(typeParameter)!.TryRecordArgument(typeSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.Constructor.MapParameter(constructorParameter)!.Normal.TryRecordArgument(constructorSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.Named.MapParameter(namedParameterName)!.TryRecordArgument(namedSyntax), Times.Once);
    }
}
