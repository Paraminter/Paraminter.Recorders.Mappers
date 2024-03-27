namespace Attribinter.Mappers;

using System;

/// <inheritdoc cref="IArgumentRecorderFactory"/>
public sealed class ArgumentRecorderFactory : IArgumentRecorderFactory
{
    /// <summary>Instantiates a <see cref="ArgumentRecorderFactory"/>, handling creation of <see cref="IArgumentRecorder{TParameter, TData}"/>.</summary>
    public ArgumentRecorderFactory() { }

    IArgumentRecorderFactory<TParameter, TRecord, TData> IArgumentRecorderFactory.WithMapper<TParameter, TRecord, TData>(IParameterMapper<TParameter, TRecord, TData> mapper)
    {
        if (mapper is null)
        {
            throw new ArgumentNullException(nameof(mapper));
        }

        return new SpecificArgumentRecorderFactory<TParameter, TRecord, TData>(mapper);
    }

    private sealed class SpecificArgumentRecorderFactory<TParameter, TRecord, TData> : IArgumentRecorderFactory<TParameter, TRecord, TData>
    {
        private readonly IParameterMapper<TParameter, TRecord, TData> Mapper;

        public SpecificArgumentRecorderFactory(IParameterMapper<TParameter, TRecord, TData> mapper)
        {
            Mapper = mapper;
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
}
