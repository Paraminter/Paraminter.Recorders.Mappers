namespace Attribinter.Mappers;

using System;

/// <inheritdoc cref="IMappedArgumentRecorderFactoryProvider"/>
public sealed class MappedArgumentRecorderFactoryProvider : IMappedArgumentRecorderFactoryProvider
{
    private readonly IBoolDelegateMappedArgumentRecorderFactory BoolDelegateFactory;
    private readonly IVoidDelegateMappedArgumentRecorderFactory VoidDelegateFactory;

    /// <summary>Instantiates a <see cref="MappedArgumentRecorderFactoryProvider"/>, providing factories of <see cref="IMappedArgumentRecorder{TRecord, TData}"/>.</summary>
    /// <param name="boolDelegateFactory">The factory handling creation of <see cref="IMappedArgumentRecorder{TRecord, TData}"/> using <see cref="bool"/>-returning delegates.</param>
    /// <param name="voidDelegateFactory">The factory handling creation of <see cref="IMappedArgumentRecorder{TRecord, TData}"/> using <see langword="void"/>-returning delegates.</param>
    public MappedArgumentRecorderFactoryProvider(IBoolDelegateMappedArgumentRecorderFactory boolDelegateFactory, IVoidDelegateMappedArgumentRecorderFactory voidDelegateFactory)
    {
        BoolDelegateFactory = boolDelegateFactory ?? throw new ArgumentNullException(nameof(boolDelegateFactory));
        VoidDelegateFactory = voidDelegateFactory ?? throw new ArgumentNullException(nameof(voidDelegateFactory));
    }

    IBoolDelegateMappedArgumentRecorderFactory IMappedArgumentRecorderFactoryProvider.BoolDelegateFactory => BoolDelegateFactory;
    IVoidDelegateMappedArgumentRecorderFactory IMappedArgumentRecorderFactoryProvider.VoidDelegateFactory => VoidDelegateFactory;
}
