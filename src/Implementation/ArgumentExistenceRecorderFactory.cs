namespace Paraminter.Recorders.Mappers;

using System;

/// <inheritdoc cref="IArgumentExistenceRecorderFactory"/>
public sealed class ArgumentExistenceRecorderFactory
    : IArgumentExistenceRecorderFactory
{
    /// <summary>Instantiates a <see cref="ArgumentExistenceRecorderFactory"/>, handling creation of <see cref="IArgumentExistenceRecorder{TParameter}"/>.</summary>
    public ArgumentExistenceRecorderFactory() { }

    IArgumentExistenceRecorder<TParameter> IArgumentExistenceRecorderFactory.Create<TParameter, TRecord>(
        IArgumentExistenceRecorderMapper<TParameter, TRecord> mapper,
        TRecord dataRecord)
    {
        if (mapper is null)
        {
            throw new ArgumentNullException(nameof(mapper));
        }

        if (dataRecord is null)
        {
            throw new ArgumentNullException(nameof(dataRecord));
        }

        return new ArgumentExistenceRecorder<TParameter, TRecord>(mapper, dataRecord);
    }

    private sealed class ArgumentExistenceRecorder<TParameter, TRecord>
        : IArgumentExistenceRecorder<TParameter>
    {
        private readonly IArgumentExistenceRecorderMapper<TParameter, TRecord> Mapper;
        private readonly TRecord ExistenceRecord;

        public ArgumentExistenceRecorder(
            IArgumentExistenceRecorderMapper<TParameter, TRecord> mapper,
            TRecord dataRecord)
        {
            Mapper = mapper;
            ExistenceRecord = dataRecord;
        }

        bool IArgumentExistenceRecorder<TParameter>.TryRecordExistence(
            TParameter parameter)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (Mapper.TryMapParameter(parameter) is not IMappedArgumentExistenceRecorder<TRecord> recorder)
            {
                return false;
            }

            return recorder.TryRecordExistence(ExistenceRecord);
        }
    }
}
