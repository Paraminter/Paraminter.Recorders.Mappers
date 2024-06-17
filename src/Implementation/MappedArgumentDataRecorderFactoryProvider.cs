namespace Paraminter.Recorders.Mappers;

using System;

/// <inheritdoc cref="IMappedArgumentDataRecorderFactoryProvider"/>
public sealed class MappedArgumentDataRecorderFactoryProvider
    : IMappedArgumentDataRecorderFactoryProvider
{
    private readonly IBoolDelegateMappedArgumentDataRecorderFactory BoolDelegate;
    private readonly IVoidDelegateMappedArgumentDataRecorderFactory VoidDelegate;

    /// <summary>Instantiates a <see cref="MappedArgumentDataRecorderFactoryProvider"/>, providing factories of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/>.</summary>
    /// <param name="boolDelegate">Handles creation of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/> using <see cref="bool"/>-returning delegates.</param>
    /// <param name="voidDelegate">Handles creation of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/> using <see langword="void"/>-returning delegates.</param>
    public MappedArgumentDataRecorderFactoryProvider(
        IBoolDelegateMappedArgumentDataRecorderFactory boolDelegate,
        IVoidDelegateMappedArgumentDataRecorderFactory voidDelegate)
    {
        BoolDelegate = boolDelegate ?? throw new ArgumentNullException(nameof(boolDelegate));
        VoidDelegate = voidDelegate ?? throw new ArgumentNullException(nameof(voidDelegate));
    }

    IBoolDelegateMappedArgumentDataRecorderFactory IMappedArgumentDataRecorderFactoryProvider.BoolDelegate => BoolDelegate;
    IVoidDelegateMappedArgumentDataRecorderFactory IMappedArgumentDataRecorderFactoryProvider.VoidDelegate => VoidDelegate;
}
