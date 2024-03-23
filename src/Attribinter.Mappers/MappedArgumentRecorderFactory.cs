namespace Attribinter.Mappers;

using System;

/// <inheritdoc cref="IMappedArgumentRecorderFactory"/>
public sealed class MappedArgumentRecorderFactory : IMappedArgumentRecorderFactory
{
    private readonly IBoolDelegateMappedArgumentRecorderFactory BoolDelegateFactory;
    private readonly IVoidDelegateMappedArgumentRecorderFactory VoidDelegateFactory;

    /// <summary>Instantiates a <see cref="MappedArgumentRecorderFactory"/>, handling creation of <see cref="IMappedArgumentRecorder{TRecord, TData}"/>.</summary>
    /// <param name="boolDelegateFactory">Handles creation of <see cref="IMappedArgumentRecorder{TRecord, TData}"/> using <see cref="bool"/>-returning delegates.</param>
    /// <param name="voidDelegateFactory">Handles creation of <see cref="IMappedArgumentRecorder{TRecord, TData}"/> using <see langword="void"/>-returning delegates.</param>
    public MappedArgumentRecorderFactory(IBoolDelegateMappedArgumentRecorderFactory boolDelegateFactory, IVoidDelegateMappedArgumentRecorderFactory voidDelegateFactory)
    {
        BoolDelegateFactory = boolDelegateFactory ?? throw new ArgumentNullException(nameof(boolDelegateFactory));
        VoidDelegateFactory = voidDelegateFactory ?? throw new ArgumentNullException(nameof(voidDelegateFactory));
    }

    IBoolDelegateMappedArgumentRecorderFactory IMappedArgumentRecorderFactory.BoolDelegateFactory => BoolDelegateFactory;
    IVoidDelegateMappedArgumentRecorderFactory IMappedArgumentRecorderFactory.VoidDelegateFactory => VoidDelegateFactory;
}
