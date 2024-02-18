namespace SharpAttributeParser.Mappers.RecorderFactoryCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private static IRecorder Target(IRecorderFactory factory, IMapper mapper) => factory.Create(mapper);

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

        var typeSyntax = ExpressionSyntaxFactory.Create();
        var constructorSyntax = ExpressionSyntaxFactory.Create();
        var namedSyntax = ExpressionSyntaxFactory.Create();

        Mock<IMapper> mapperMock = new() { DefaultValue = DefaultValue.Mock };

        var recorder = Target(Context.Factory, mapperMock.Object);

        recorder.Type.TryRecordArgument(typeParameter, typeArgument, typeSyntax);
        recorder.Constructor.Normal.TryRecordArgument(constructorParameter, constructorArgument, constructorSyntax);
        recorder.Named.TryRecordArgument(namedParameterName, namedArgument, namedSyntax);

        mapperMock.Verify((mapper) => mapper.Type.MapParameter(typeParameter)!.TryRecordArgument(typeArgument, typeSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.Constructor.MapParameter(constructorParameter)!.Normal.TryRecordArgument(constructorArgument, constructorSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.Named.MapParameter(namedParameterName)!.TryRecordArgument(namedArgument, namedSyntax), Times.Once);
    }
}
