namespace Attribinter.Mappers;

/// <summary>Handles creation of <see cref="IMappedArgumentRecorder{TRecord, TData}"/>.</summary>
public interface IMappedArgumentRecorderFactory
{
    /// <summary>Handles creation of <see cref="IMappedArgumentRecorder{TRecord, TData}"/> using <see cref="bool"/>-returning delegates.</summary>
    public abstract IBoolDelegateMappedArgumentRecorderFactory BoolDelegateFactory { get; }

    /// <summary>Handles creation of <see cref="IMappedArgumentRecorder{TRecord, TData}"/> using <see langword="void"/>-returning delegates.</summary>
    public abstract IVoidDelegateMappedArgumentRecorderFactory VoidDelegateFactory { get; }
}
