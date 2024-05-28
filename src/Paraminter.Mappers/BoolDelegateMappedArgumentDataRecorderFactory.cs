namespace Paraminter.Mappers;

using System;

/// <inheritdoc cref="IBoolDelegateMappedArgumentDataRecorderFactory"/>
public sealed class BoolDelegateMappedArgumentDataRecorderFactory
    : IBoolDelegateMappedArgumentDataRecorderFactory
{
    /// <summary>Instantiates a <see cref="BoolDelegateMappedArgumentDataRecorderFactory"/>, handling creation of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/> using <see cref="bool"/>-returning delegates.</summary>
    public BoolDelegateMappedArgumentDataRecorderFactory() { }

    IMappedArgumentDataRecorder<TRecord, TArgumentData> IBoolDelegateMappedArgumentDataRecorderFactory.Create<TRecord, TArgumentData>(
        DBoolArgumentDataRecorder<TRecord, TArgumentData> recorderDelegate)
    {
        if (recorderDelegate is null)
        {
            throw new ArgumentNullException(nameof(recorderDelegate));
        }

        return new MappedArgumentRecorder<TRecord, TArgumentData>(recorderDelegate);
    }

    private sealed class MappedArgumentRecorder<TRecord, TArgumentData>
        : IMappedArgumentDataRecorder<TRecord, TArgumentData>
    {
        private readonly DBoolArgumentDataRecorder<TRecord, TArgumentData> RecorderDelegate;

        public MappedArgumentRecorder(
            DBoolArgumentDataRecorder<TRecord, TArgumentData> recorderDelegate)
        {
            RecorderDelegate = recorderDelegate;
        }

        bool IMappedArgumentDataRecorder<TRecord, TArgumentData>.TryRecordData(
            TRecord dataRecord,
            TArgumentData argumentData)
        {
            if (dataRecord is null)
            {
                throw new ArgumentNullException(nameof(dataRecord));
            }

            return RecorderDelegate(dataRecord, argumentData);
        }
    }
}
