namespace Attribinter.Mappers;

using System;

/// <inheritdoc cref="IVoidDelegateMappedArgumentRecorderFactory"/>
public sealed class VoidDelegateMappedArgumentRecorderFactory : IVoidDelegateMappedArgumentRecorderFactory
{
    /// <summary>Instantiates a <see cref="VoidDelegateMappedArgumentRecorderFactory"/>, handling creation of <see cref="IMappedArgumentRecorder{TRecord, TData}"/> using <see langword="void"/>-returning delegates.</summary>
    public VoidDelegateMappedArgumentRecorderFactory() { }

    IMappedArgumentRecorder<TRecord, TData> IVoidDelegateMappedArgumentRecorderFactory.Create<TRecord, TData>(Action<TRecord, TData> recorderDelegate)
    {
        if (recorderDelegate is null)
        {
            throw new ArgumentNullException(nameof(recorderDelegate));
        }

        return new MappedArgumentRecorder<TRecord, TData>(recorderDelegate);
    }

    private sealed class MappedArgumentRecorder<TRecord, TData> : IMappedArgumentRecorder<TRecord, TData>
    {
        private readonly Action<TRecord, TData> RecorderDelegate;

        public MappedArgumentRecorder(Action<TRecord, TData> recorderDelegate)
        {
            RecorderDelegate = recorderDelegate;
        }

        bool IMappedArgumentRecorder<TRecord, TData>.TryRecordData(TRecord dataRecord, TData data)
        {
            if (dataRecord is null)
            {
                throw new ArgumentNullException(nameof(dataRecord));
            }

            RecorderDelegate(dataRecord, data);

            return true;
        }
    }
}
