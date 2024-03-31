namespace Attribinter.Mappers;

using System;

/// <inheritdoc cref="IMappedArgumentRecorderFactory"/>
public sealed class MappedArgumentRecorderFactory : IMappedArgumentRecorderFactory
{
    private readonly IMappedArgumentRecorderFactoryProvider FactoryProvider;

    /// <summary>Instantiates a <see cref="MappedArgumentRecorderFactory"/>, handling creation of <see cref="IMappedArgumentRecorder{TRecord, TData}"/>.</summary>
    /// <param name="factoryProvider">Provides factories of <see cref="IMappedArgumentRecorder{TRecord, TData}"/>.</param>
    public MappedArgumentRecorderFactory(IMappedArgumentRecorderFactoryProvider factoryProvider)
    {
        FactoryProvider = factoryProvider ?? throw new ArgumentNullException(nameof(factoryProvider));
    }

    IMappedArgumentRecorder<TRecord, TData> IMappedArgumentRecorderFactory.Create<TRecord, TData>(Func<TRecord, TData, bool> recorderDelegate)
    {
        if (recorderDelegate is null)
        {
            throw new ArgumentNullException(nameof(recorderDelegate));
        }

        return FactoryProvider.BoolDelegateFactory.Create(recorderDelegate);
    }

    IMappedArgumentRecorder<TRecord, TData> IMappedArgumentRecorderFactory.Create<TRecord, TData>(Action<TRecord, TData> recorderDelegate)
    {
        if (recorderDelegate is null)
        {
            throw new ArgumentNullException(nameof(recorderDelegate));
        }

        return FactoryProvider.VoidDelegateFactory.Create(recorderDelegate);
    }
}
