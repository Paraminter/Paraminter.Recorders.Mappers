namespace Attribinter.Mappers;

using System;

/// <inheritdoc cref="IArgumentRecorderFactory"/>
public sealed class ArgumentRecorderFactory : IArgumentRecorderFactory
{
    /// <summary>Instantiates a <see cref="ArgumentRecorderFactory"/>, handling creation of <see cref="IArgumentRecorder{TParameter, TData}"/> using <see cref="IParameterMapper{TParameter, TRecord, TData}"/>.</summary>
    public ArgumentRecorderFactory() { }

    IArgumentRecorder<TParameter, TData> IArgumentRecorderFactory.Create<TParameter, TRecord, TData>(IParameterMapper<TParameter, TRecord, TData> mapper, TRecord dataRecord)
    {
        if (mapper is null)
        {
            throw new ArgumentNullException(nameof(mapper));
        }

        if (dataRecord is null)
        {
            throw new ArgumentNullException(nameof(dataRecord));
        }

        return new ArgumentRecorder<TParameter, TRecord, TData>(mapper, dataRecord);
    }

    private sealed class ArgumentRecorder<TParameter, TRecord, TData> : IArgumentRecorder<TParameter, TData>
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
