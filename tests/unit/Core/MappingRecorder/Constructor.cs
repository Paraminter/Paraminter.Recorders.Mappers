namespace Paraminter.Recorders.Mappers;

using Moq;

using Paraminter.Arguments.Models;
using Paraminter.Commands.Handlers;
using Paraminter.Parameters.Models;
using Paraminter.Queries.Handlers;
using Paraminter.Recorders.Mappers.Commands;
using Paraminter.Recorders.Mappers.Queries;

using System;

using Xunit;

public sealed class Constructor
{
    [Fact]
    public void NullMapper_ThrowsArgumentNullException()
    {
        var result = Record.Exception(() => Target<IParameter, IArgumentData>(null!));

        Assert.IsType<ArgumentNullException>(result);
    }

    [Fact]
    public void ValidArguments_ReturnsRecorder()
    {
        var result = Target(Mock.Of<IQueryHandler<IGetMappedArgumentAssociationRecorderQuery<IParameter>, ICommandHandler<IRecordMappedArgumentAssociationCommand<IArgumentData>>>>());

        Assert.NotNull(result);
    }

    private static MappingRecorder<TParameter, TArgumentData> Target<TParameter, TArgumentData>(
        IQueryHandler<IGetMappedArgumentAssociationRecorderQuery<TParameter>, ICommandHandler<IRecordMappedArgumentAssociationCommand<TArgumentData>>> mapper)
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        return new MappingRecorder<TParameter, TArgumentData>(mapper);
    }
}
