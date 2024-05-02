namespace Paraminter.Mappers;

/// <summary>Provides factories of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/>.</summary>
public interface IMappedArgumentDataRecorderFactoryProvider
{
    /// <summary>The factory handling creation of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/> using <see cref="bool"/>-returning delegates.</summary>
    public abstract IBoolDelegateMappedArgumentDataRecorderFactory BoolDelegateFactory { get; }

    /// <summary>The factory handling creation of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/> using <see langword="void"/>-returning delegates.</summary>
    public abstract IVoidDelegateMappedArgumentDataRecorderFactory VoidDelegateFactory { get; }
}
