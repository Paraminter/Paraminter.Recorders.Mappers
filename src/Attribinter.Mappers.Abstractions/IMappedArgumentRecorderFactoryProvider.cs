namespace Attribinter.Mappers;

/// <summary>Provides factories of <see cref="IMappedArgumentRecorder{TRecord, TData}"/>.</summary>
public interface IMappedArgumentRecorderFactoryProvider
{
    /// <summary>The factory handling creation of <see cref="IMappedArgumentRecorder{TRecord, TData}"/> using <see cref="bool"/>-returning delegates.</summary>
    public abstract IBoolDelegateMappedArgumentRecorderFactory BoolDelegateFactory { get; }

    /// <summary>The factory handling creation of <see cref="IMappedArgumentRecorder{TRecord, TData}"/> using <see langword="void"/>-returning delegates.</summary>
    public abstract IVoidDelegateMappedArgumentRecorderFactory VoidDelegateFactory { get; }
}
