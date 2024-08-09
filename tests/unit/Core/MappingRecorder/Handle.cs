namespace Paraminter.Recorders.Mappers;

using Moq;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Parameters.Models;
using Paraminter.Recorders.Commands;
using Paraminter.Recorders.Mappers.Commands;
using Paraminter.Recorders.Mappers.Queries;

using System;
using System.Linq.Expressions;

using Xunit;

public sealed class Handle
{
    [Fact]
    public void NullCommand_ThrowsArgumentNullException()
    {
        var fixture = FixtureFactory.Create<IParameter, IArgumentData>();

        var result = Record.Exception(() => Target(fixture, null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidCommand_MapsAndRecords()
    {
        var parameter = Mock.Of<IParameter>();
        var argumentData = Mock.Of<IArgumentData>();

        Mock<IRecordArgumentAssociationCommand<IParameter, IArgumentData>> commandMock = new();

        commandMock.Setup(static (command) => command.Parameter).Returns(parameter);
        commandMock.Setup(static (command) => command.ArgumentData).Returns(argumentData);

        Mock<ICommandHandler<IRecordMappedArgumentAssociationCommand<IArgumentData>>> mappedRecorderMock = new();

        var fixture = FixtureFactory.Create<IParameter, IArgumentData>();

        fixture.MapperMock.Setup(static (mapper) => mapper.Handle(It.IsAny<IGetMappedArgumentAssociationRecorderQuery<IParameter>>())).Returns(mappedRecorderMock.Object);

        Target(fixture, commandMock.Object);

        fixture.MapperMock.Verify(GetMappedRecorderExpression<IParameter, IArgumentData>(parameter), Times.Once());

        mappedRecorderMock.Verify(RecordMappedExpression(argumentData), Times.Once());
    }

    private static Expression<Action<IQueryHandler<IGetMappedArgumentAssociationRecorderQuery<TParameter>, ICommandHandler<IRecordMappedArgumentAssociationCommand<TArgumentData>>>>> GetMappedRecorderExpression<TParameter, TArgumentData>(
        TParameter parameter)
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        return (recorder) => recorder.Handle(It.Is(MatchGetMappedRecorderCommand(parameter)));
    }

    private static Expression<Action<ICommandHandler<IRecordMappedArgumentAssociationCommand<TArgumentData>>>> RecordMappedExpression<TArgumentData>(
        TArgumentData argumentData)
        where TArgumentData : IArgumentData
    {
        return (recorder) => recorder.Handle(It.Is(MatchRecordMappedCommand(argumentData)));
    }

    private static Expression<Func<IGetMappedArgumentAssociationRecorderQuery<TParameter>, bool>> MatchGetMappedRecorderCommand<TParameter>(
        TParameter parameter)
        where TParameter : IParameter
    {
        return (command) => ReferenceEquals(parameter, command.Parameter);
    }

    private static Expression<Func<IRecordMappedArgumentAssociationCommand<TArgumentData>, bool>> MatchRecordMappedCommand<TArgumentData>(
        TArgumentData argumentData)
        where TArgumentData : IArgumentData
    {
        return (command) => ReferenceEquals(argumentData, command.ArgumentData);
    }

    private static void Target<TParameter, TArgumentData>(
        IFixture<TParameter, TArgumentData> fixture,
        IRecordArgumentAssociationCommand<TParameter, TArgumentData> command)
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        fixture.Sut.Handle(command);
    }
}
