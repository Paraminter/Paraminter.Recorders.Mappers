namespace Paraminter.Mappers;

using System;

/// <inheritdoc cref="IMappedArgumentDataRecorderFactory"/>
public sealed class MappedArgumentDataRecorderFactory : IMappedArgumentDataRecorderFactory
{
    private readonly IMappedArgumentDataRecorderFactoryProvider FactoryProvider;

    /// <summary>Instantiates a <see cref="MappedArgumentDataRecorderFactory"/>, handling creation of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/>.</summary>
    /// <param name="factoryProvider">Provides factories of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/>.</param>
    public MappedArgumentDataRecorderFactory(IMappedArgumentDataRecorderFactoryProvider factoryProvider)
    {
        FactoryProvider = factoryProvider ?? throw new ArgumentNullException(nameof(factoryProvider));
    }

    IMappedArgumentDataRecorder<TRecord, TArgumentData> IMappedArgumentDataRecorderFactory.Create<TRecord, TArgumentData>(DBoolArgumentDataRecorder<TRecord, TArgumentData> recorderDelegate)
    {
        if (recorderDelegate is null)
        {
            throw new ArgumentNullException(nameof(recorderDelegate));
        }

        return FactoryProvider.BoolDelegateFactory.Create(recorderDelegate);
    }

    IMappedArgumentDataRecorder<TRecord, TArgumentData> IMappedArgumentDataRecorderFactory.Create<TRecord, TArgumentData>(DVoidArgumentDataRecorder<TRecord, TArgumentData> recorderDelegate)
    {
        if (recorderDelegate is null)
        {
            throw new ArgumentNullException(nameof(recorderDelegate));
        }

        return FactoryProvider.VoidDelegateFactory.Create(recorderDelegate);
    }
}
