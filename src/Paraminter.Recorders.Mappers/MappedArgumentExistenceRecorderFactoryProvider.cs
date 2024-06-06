namespace Paraminter.Mappers;

using System;

/// <inheritdoc cref="IMappedArgumentExistenceRecorderFactoryProvider"/>
public sealed class MappedArgumentExistenceRecorderFactoryProvider
    : IMappedArgumentExistenceRecorderFactoryProvider
{
    private readonly IBoolDelegateMappedArgumentExistenceRecorderFactory BoolDelegate;
    private readonly IVoidDelegateMappedArgumentExistenceRecorderFactory VoidDelegate;

    /// <summary>Instantiates a <see cref="MappedArgumentExistenceRecorderFactoryProvider"/>, providing factories of <see cref="IMappedArgumentExistenceRecorder{TRecord}"/>.</summary>
    /// <param name="boolDelegate">Handles creation of <see cref="IMappedArgumentExistenceRecorder{TRecord}"/> using <see cref="bool"/>-returning delegates.</param>
    /// <param name="voidDelegate">Handles creation of <see cref="IMappedArgumentExistenceRecorder{TRecord}"/> using <see langword="void"/>-returning delegates.</param>
    public MappedArgumentExistenceRecorderFactoryProvider(
        IBoolDelegateMappedArgumentExistenceRecorderFactory boolDelegate,
        IVoidDelegateMappedArgumentExistenceRecorderFactory voidDelegate)
    {
        BoolDelegate = boolDelegate ?? throw new ArgumentNullException(nameof(boolDelegate));
        VoidDelegate = voidDelegate ?? throw new ArgumentNullException(nameof(voidDelegate));
    }

    IBoolDelegateMappedArgumentExistenceRecorderFactory IMappedArgumentExistenceRecorderFactoryProvider.BoolDelegate => BoolDelegate;
    IVoidDelegateMappedArgumentExistenceRecorderFactory IMappedArgumentExistenceRecorderFactoryProvider.VoidDelegate => VoidDelegate;
}
