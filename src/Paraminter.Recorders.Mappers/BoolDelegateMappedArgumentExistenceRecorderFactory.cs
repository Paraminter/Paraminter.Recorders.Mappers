namespace Paraminter.Mappers;

using System;

/// <inheritdoc cref="IBoolDelegateMappedArgumentExistenceRecorderFactory"/>
public sealed class BoolDelegateMappedArgumentExistenceRecorderFactory
    : IBoolDelegateMappedArgumentExistenceRecorderFactory
{
    /// <summary>Instantiates a <see cref="BoolDelegateMappedArgumentExistenceRecorderFactory"/>, handling creation of <see cref="IMappedArgumentExistenceRecorder{TRecord}"/> using <see cref="bool"/>-returning delegates.</summary>
    public BoolDelegateMappedArgumentExistenceRecorderFactory() { }

    IMappedArgumentExistenceRecorder<TRecord> IBoolDelegateMappedArgumentExistenceRecorderFactory.Create<TRecord>(
        DBoolArgumentExistenceRecorder<TRecord> recorderDelegate)
    {
        if (recorderDelegate is null)
        {
            throw new ArgumentNullException(nameof(recorderDelegate));
        }

        return new MappedArgumentExistenceRecorder<TRecord>(recorderDelegate);
    }

    private sealed class MappedArgumentExistenceRecorder<TRecord>
        : IMappedArgumentExistenceRecorder<TRecord>
    {
        private readonly DBoolArgumentExistenceRecorder<TRecord> RecorderDelegate;

        public MappedArgumentExistenceRecorder(
            DBoolArgumentExistenceRecorder<TRecord> recorderDelegate)
        {
            RecorderDelegate = recorderDelegate;
        }

        bool IMappedArgumentExistenceRecorder<TRecord>.TryRecordExistence(
            TRecord dataRecord)
        {
            if (dataRecord is null)
            {
                throw new ArgumentNullException(nameof(dataRecord));
            }

            return RecorderDelegate(dataRecord);
        }
    }
}
