namespace Paraminter.Mappers;

using System;

/// <inheritdoc cref="IMappedArgumentDataRecorderFactoryProvider"/>
public sealed class MappedArgumentDataRecorderFactoryProvider : IMappedArgumentDataRecorderFactoryProvider
{
    private readonly IBoolDelegateMappedArgumentDataRecorderFactory BoolDelegateFactory;
    private readonly IVoidDelegateMappedArgumentDataRecorderFactory VoidDelegateFactory;

    /// <summary>Instantiates a <see cref="MappedArgumentDataRecorderFactoryProvider"/>, providing factories of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/>.</summary>
    /// <param name="boolDelegateFactory">The factory handling creation of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/> using <see cref="bool"/>-returning delegates.</param>
    /// <param name="voidDelegateFactory">The factory handling creation of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/> using <see langword="void"/>-returning delegates.</param>
    public MappedArgumentDataRecorderFactoryProvider(IBoolDelegateMappedArgumentDataRecorderFactory boolDelegateFactory, IVoidDelegateMappedArgumentDataRecorderFactory voidDelegateFactory)
    {
        BoolDelegateFactory = boolDelegateFactory ?? throw new ArgumentNullException(nameof(boolDelegateFactory));
        VoidDelegateFactory = voidDelegateFactory ?? throw new ArgumentNullException(nameof(voidDelegateFactory));
    }

    IBoolDelegateMappedArgumentDataRecorderFactory IMappedArgumentDataRecorderFactoryProvider.BoolDelegateFactory => BoolDelegateFactory;
    IVoidDelegateMappedArgumentDataRecorderFactory IMappedArgumentDataRecorderFactoryProvider.VoidDelegateFactory => VoidDelegateFactory;
}
