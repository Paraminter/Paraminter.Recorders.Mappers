namespace Paraminter.Mappers;

using System;

/// <inheritdoc cref="IVoidDelegateMappedArgumentDataRecorderFactory"/>
public sealed class VoidDelegateMappedArgumentDataRecorderFactory : IVoidDelegateMappedArgumentDataRecorderFactory
{
    /// <summary>Instantiates a <see cref="VoidDelegateMappedArgumentDataRecorderFactory"/>, handling creation of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/> using <see langword="void"/>-returning delegates.</summary>
    public VoidDelegateMappedArgumentDataRecorderFactory() { }

    IMappedArgumentDataRecorder<TRecord, TArgumentData> IVoidDelegateMappedArgumentDataRecorderFactory.Create<TRecord, TArgumentData>(DVoidArgumentDataRecorder<TRecord, TArgumentData> recorderDelegate)
    {
        if (recorderDelegate is null)
        {
            throw new ArgumentNullException(nameof(recorderDelegate));
        }

        return new MappedArgumentRecorder<TRecord, TArgumentData>(recorderDelegate);
    }

    private sealed class MappedArgumentRecorder<TRecord, TArgumentData> : IMappedArgumentDataRecorder<TRecord, TArgumentData>
    {
        private readonly DVoidArgumentDataRecorder<TRecord, TArgumentData> RecorderDelegate;

        public MappedArgumentRecorder(DVoidArgumentDataRecorder<TRecord, TArgumentData> recorderDelegate)
        {
            RecorderDelegate = recorderDelegate;
        }

        bool IMappedArgumentDataRecorder<TRecord, TArgumentData>.TryRecordData(TRecord dataRecord, TArgumentData argumentData)
        {
            if (dataRecord is null)
            {
                throw new ArgumentNullException(nameof(dataRecord));
            }

            RecorderDelegate(dataRecord, argumentData);

            return true;
        }
    }
}
