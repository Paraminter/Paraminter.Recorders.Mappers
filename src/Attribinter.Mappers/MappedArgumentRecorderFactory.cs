namespace Attribinter.Mappers;

using System;

/// <inheritdoc cref="IMappedArgumentRecorderFactory"/>
public sealed class MappedArgumentRecorderFactory : IMappedArgumentRecorderFactory
{
    /// <summary>Instantiates a <see cref="MappedArgumentRecorderFactory"/>, handling creation of <see cref="IMappedArgumentRecorder{TRecord, TData}"/> using delegates.</summary>
    public MappedArgumentRecorderFactory() { }

    IMappedArgumentRecorder<TRecord, TData> IMappedArgumentRecorderFactory.Create<TRecord, TData>(Func<TRecord, TData, bool> recorderDelegate)
    {
        if (recorderDelegate is null)
        {
            throw new ArgumentNullException(nameof(recorderDelegate));
        }

        return new MappedArgumentRecorder<TRecord, TData>(recorderDelegate);
    }

    private sealed class MappedArgumentRecorder<TRecord, TData> : IMappedArgumentRecorder<TRecord, TData>
    {
        private readonly Func<TRecord, TData, bool> RecorderDelegate;

        public MappedArgumentRecorder(Func<TRecord, TData, bool> recorderDelegate)
        {
            RecorderDelegate = recorderDelegate;
        }

        bool IMappedArgumentRecorder<TRecord, TData>.TryRecordData(TRecord dataRecord, TData data)
        {
            if (dataRecord is null)
            {
                throw new ArgumentNullException(nameof(dataRecord));
            }

            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            return RecorderDelegate(dataRecord, data);
        }
    }
}
