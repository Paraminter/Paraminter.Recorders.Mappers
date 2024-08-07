namespace Paraminter.Recorders.Mappers;

using Moq;

using Paraminter.Arguments.Models;
using Paraminter.Commands.Handlers;
using Paraminter.Parameters.Models;
using Paraminter.Queries.Handlers;
using Paraminter.Recorders.Commands;
using Paraminter.Recorders.Mappers.Commands;
using Paraminter.Recorders.Mappers.Queries;

internal interface IFixture<TParameter, TArgumentData>
    where TParameter : IParameter
    where TArgumentData : IArgumentData
{
    public abstract ICommandHandler<IRecordArgumentAssociationCommand<TParameter, TArgumentData>> Sut { get; }

    public abstract Mock<IQueryHandler<IGetMappedArgumentAssociationRecorderQuery<TParameter>, ICommandHandler<IRecordMappedArgumentAssociationCommand<TArgumentData>>>> MapperMock { get; }
}
