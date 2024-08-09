namespace Paraminter.Recorders.Mappers;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Parameters.Models;
using Paraminter.Recorders.Commands;
using Paraminter.Recorders.Mappers.Commands;
using Paraminter.Recorders.Mappers.Queries;

using System;

/// <summary>Records associations between arguments and parameters through mapped recorders.</summary>
/// <typeparam name="TParameter">The type representing the parameters.</typeparam>
/// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
public sealed class MappingRecorder<TParameter, TArgumentData>
    : ICommandHandler<IRecordArgumentAssociationCommand<TParameter, TArgumentData>>
    where TParameter : IParameter
    where TArgumentData : IArgumentData
{
    private readonly IQueryHandler<IGetMappedArgumentAssociationRecorderQuery<TParameter>, ICommandHandler<IRecordMappedArgumentAssociationCommand<TArgumentData>>> Mapper;

    /// <summary>Instantiates a <see cref="MappingRecorder{TParameter, TArgumentData}"/>, recording associations between arguments and parameters through mapped recorders.</summary>
    /// <param name="mapper">Maps parameters to recorders.</param>
    public MappingRecorder(
        IQueryHandler<IGetMappedArgumentAssociationRecorderQuery<TParameter>, ICommandHandler<IRecordMappedArgumentAssociationCommand<TArgumentData>>> mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    void ICommandHandler<IRecordArgumentAssociationCommand<TParameter, TArgumentData>>.Handle(
        IRecordArgumentAssociationCommand<TParameter, TArgumentData> command)
    {
        if (command is null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        var mappingCommand = GetMappedArgumentAssociationRecorderQueryFactory.Create(command.Parameter);
        var recordCommand = RecordMappedArgumentAssociationCommandFactory.Create(command.ArgumentData);

        var recorder = Mapper.Handle(mappingCommand);

        recorder.Handle(recordCommand);
    }
}
