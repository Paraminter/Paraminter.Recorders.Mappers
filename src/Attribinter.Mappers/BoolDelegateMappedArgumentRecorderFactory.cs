namespace Attribinter.Mappers;

using System;

/// <inheritdoc cref="IBoolDelegateMappedArgumentRecorderFactory"/>
public sealed class BoolDelegateMappedArgumentRecorderFactory : IBoolDelegateMappedArgumentRecorderFactory
{
    /// <summary>Instantiates a <see cref="BoolDelegateMappedArgumentRecorderFactory"/>, handling creation of <see cref="IMappedArgumentRecorder{TRecord, TData}"/> using <see cref="bool"/>-returning delegates.</summary>
    public BoolDelegateMappedArgumentRecorderFactory() { }

    IMappedArgumentRecorder<TRecord, TData> IBoolDelegateMappedArgumentRecorderFactory.Create<TRecord, TData>(DAttemptingArgumentRecorder<TRecord, TData> recorderDelegate)
    {
        if (recorderDelegate is null)
        {
            throw new ArgumentNullException(nameof(recorderDelegate));
        }

        return new MappedArgumentRecorder<TRecord, TData>(recorderDelegate);
    }

    private sealed class MappedArgumentRecorder<TRecord, TData> : IMappedArgumentRecorder<TRecord, TData>
    {
        private readonly DAttemptingArgumentRecorder<TRecord, TData> RecorderDelegate;

        public MappedArgumentRecorder(DAttemptingArgumentRecorder<TRecord, TData> recorderDelegate)
        {
            RecorderDelegate = recorderDelegate;
        }

        bool IMappedArgumentRecorder<TRecord, TData>.TryRecordData(TRecord dataRecord, TData data)
        {
            if (dataRecord is null)
            {
                throw new ArgumentNullException(nameof(dataRecord));
            }

            return RecorderDelegate(dataRecord, data);
        }
    }
}
