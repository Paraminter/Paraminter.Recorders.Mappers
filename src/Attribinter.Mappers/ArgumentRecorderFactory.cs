namespace Attribinter.Mappers;

using System;

/// <inheritdoc cref="IArgumentRecorderFactory{TParameter, TRecord, TData}"/>
public sealed class ArgumentRecorderFactory<TParameter, TRecord, TData> : IArgumentRecorderFactory<TParameter, TRecord, TData>
{
    private readonly IParameterMapper<TParameter, TRecord, TData> Mapper;

    /// <summary>Instantiates a <see cref="ArgumentRecorderFactory{TParameter, TRecord, TData}"/>, handling creation of <see cref="IArgumentRecorder{TParameter, TData}"/>.</summary>
    /// <param name="mapper">The <see cref="IParameterMapper{TParameter, TRecord, TData}"/> used to create <see cref="IArgumentRecorder{TParameter, TData}"/>.</param>
    public ArgumentRecorderFactory(IParameterMapper<TParameter, TRecord, TData> mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    IArgumentRecorder<TParameter, TData> IArgumentRecorderFactory<TParameter, TRecord, TData>.Create(TRecord dataRecord)
    {
        if (dataRecord is null)
        {
            throw new ArgumentNullException(nameof(dataRecord));
        }

        return new ArgumentRecorder(Mapper, dataRecord);
    }

    private sealed class ArgumentRecorder : IArgumentRecorder<TParameter, TData>
    {
        private readonly IParameterMapper<TParameter, TRecord, TData> Mapper;
        private readonly TRecord DataRecord;

        public ArgumentRecorder(IParameterMapper<TParameter, TRecord, TData> mapper, TRecord dataRecord)
        {
            Mapper = mapper;
            DataRecord = dataRecord;
        }

        bool IArgumentRecorder<TParameter, TData>.TryRecordData(TParameter parameter, TData data)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (Mapper.TryMapParameter(parameter) is not IMappedArgumentRecorder<TRecord, TData> recorder)
            {
                return false;
            }

            return recorder.TryRecordData(DataRecord, data);
        }
    }
}
