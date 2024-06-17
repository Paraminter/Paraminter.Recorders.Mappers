namespace Paraminter.Recorders.Mappers;

/// <summary>Provides factories of <see cref="IMappedArgumentExistenceRecorder{TRecord}"/>.</summary>
public interface IMappedArgumentExistenceRecorderFactoryProvider
{
    /// <summary>Handles creation of <see cref="IMappedArgumentExistenceRecorder{TRecord}"/> using <see cref="bool"/>-returning delegates.</summary>
    public abstract IBoolDelegateMappedArgumentExistenceRecorderFactory BoolDelegate { get; }

    /// <summary>Handles creation of <see cref="IMappedArgumentExistenceRecorder{TRecord}"/> using <see langword="void"/>-returning delegates.</summary>
    public abstract IVoidDelegateMappedArgumentExistenceRecorderFactory VoidDelegate { get; }
}
