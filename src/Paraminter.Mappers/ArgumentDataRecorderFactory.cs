namespace Paraminter.Mappers;

using System;

/// <inheritdoc cref="IArgumentDataRecorderFactory"/>
public sealed class ArgumentDataRecorderFactory
    : IArgumentDataRecorderFactory
{
    /// <summary>Instantiates a <see cref="ArgumentDataRecorderFactory"/>, handling creation of <see cref="IArgumentDataRecorder{TParameter, TArgumentData}"/>.</summary>
    public ArgumentDataRecorderFactory() { }

    IArgumentDataRecorder<TParameter, TArgumentData> IArgumentDataRecorderFactory.Create<TParameter, TRecord, TArgumentData>(
        IParameterMapper<TParameter, TRecord, TArgumentData> mapper,
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

        return new ArgumentDataRecorder<TParameter, TRecord, TArgumentData>(mapper, dataRecord);
    }

    private sealed class ArgumentDataRecorder<TParameter, TRecord, TArgumentData>
        : IArgumentDataRecorder<TParameter, TArgumentData>
    {
        private readonly IParameterMapper<TParameter, TRecord, TArgumentData> Mapper;
        private readonly TRecord DataRecord;

        public ArgumentDataRecorder(
            IParameterMapper<TParameter, TRecord, TArgumentData> mapper,
            TRecord dataRecord)
        {
            Mapper = mapper;
            DataRecord = dataRecord;
        }

        bool IArgumentDataRecorder<TParameter, TArgumentData>.TryRecordData(
            TParameter parameter,
            TArgumentData argumentData)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (argumentData is null)
            {
                throw new ArgumentNullException(nameof(argumentData));
            }

            if (Mapper.TryMapParameter(parameter) is not IMappedArgumentDataRecorder<TRecord, TArgumentData> recorder)
            {
                return false;
            }

            return recorder.TryRecordData(DataRecord, argumentData);
        }
    }
}
