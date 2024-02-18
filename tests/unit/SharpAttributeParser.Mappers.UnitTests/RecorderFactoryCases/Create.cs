namespace SharpAttributeParser.Mappers.RecorderFactoryCases;

using Microsoft.CodeAnalysis;

using Moq;

using System;

using Xunit;

public sealed class Create
{
    private static IRecorder Target<TRecord>(IRecorderFactory factory, IMapper<TRecord> mapper, TRecord dataRecord) => factory.Create(mapper, dataRecord);

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
        var exception = Record.Exception(() => Target(Context.Factory, Mock.Of<IMapper<object>>(), null!));

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

        var typeArgument = Mock.Of<ITypeSymbol>();
        var normalConstructorArgument = Mock.Of<object>();
        var paramsConstructorArgument = Mock.Of<object>();
        var defaultConstructorArgument = Mock.Of<object>();
        var namedArgument = Mock.Of<object>();

        var typeSyntax = ExpressionSyntaxFactory.Create();
        var normalConstructorSyntax = ExpressionSyntaxFactory.Create();
        var paramsConstructorSyntax = new[] { ExpressionSyntaxFactory.Create() };
        var namedSyntax = ExpressionSyntaxFactory.Create();

        Mock<IMapper<object>> mapperMock = new() { DefaultValue = DefaultValue.Mock };

        var recorder = Target(Context.Factory, mapperMock.Object, dataRecord);

        recorder.Type.TryRecordArgument(typeParameter, typeArgument, typeSyntax);
        recorder.Constructor.Normal.TryRecordArgument(normalConstructorParameter, normalConstructorArgument, normalConstructorSyntax);
        recorder.Constructor.Params.TryRecordArgument(paramsConstructorParameter, paramsConstructorArgument, paramsConstructorSyntax);
        recorder.Constructor.Default.TryRecordArgument(defaultConstructorParameter, defaultConstructorArgument);
        recorder.Named.TryRecordArgument(namedParameterName, namedArgument, namedSyntax);

        mapperMock.Verify((mapper) => mapper.Type.MapParameter(typeParameter).TryRecordArgument(dataRecord, typeArgument, typeSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.Constructor.Normal.MapParameter(normalConstructorParameter).TryRecordArgument(dataRecord, normalConstructorArgument, normalConstructorSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.Constructor.Params.MapParameter(paramsConstructorParameter).TryRecordArgument(dataRecord, paramsConstructorArgument, paramsConstructorSyntax), Times.Once);
        mapperMock.Verify((mapper) => mapper.Constructor.Default.MapParameter(defaultConstructorParameter).TryRecordArgument(dataRecord, defaultConstructorArgument), Times.Once);
        mapperMock.Verify((mapper) => mapper.Named.MapParameter(namedParameterName).TryRecordArgument(dataRecord, namedArgument, namedSyntax), Times.Once);
    }
}
