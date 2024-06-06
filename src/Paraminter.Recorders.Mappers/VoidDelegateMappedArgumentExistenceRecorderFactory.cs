namespace Paraminter.Mappers;

using System;

/// <inheritdoc cref="IVoidDelegateMappedArgumentExistenceRecorderFactory"/>
public sealed class VoidDelegateMappedArgumentExistenceRecorderFactory
    : IVoidDelegateMappedArgumentExistenceRecorderFactory
{
    /// <summary>Instantiates a <see cref="VoidDelegateMappedArgumentExistenceRecorderFactory"/>, handling creation of <see cref="IMappedArgumentExistenceRecorder{TRecord}"/> using <see langword="void"/>-returning delegates.</summary>
    public VoidDelegateMappedArgumentExistenceRecorderFactory() { }

    IMappedArgumentExistenceRecorder<TRecord> IVoidDelegateMappedArgumentExistenceRecorderFactory.Create<TRecord>(
        DVoidArgumentExistenceRecorder<TRecord> recorderDelegate)
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
        private readonly DVoidArgumentExistenceRecorder<TRecord> RecorderDelegate;

        public MappedArgumentExistenceRecorder(
            DVoidArgumentExistenceRecorder<TRecord> recorderDelegate)
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

            RecorderDelegate(dataRecord);

            return true;
        }
    }
}
