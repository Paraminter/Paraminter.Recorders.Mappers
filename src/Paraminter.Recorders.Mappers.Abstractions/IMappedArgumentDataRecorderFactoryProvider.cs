namespace Paraminter.Recorders.Mappers;

/// <summary>Provides factories of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/>.</summary>
public interface IMappedArgumentDataRecorderFactoryProvider
{
    /// <summary>Handles creation of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/> using <see cref="bool"/>-returning delegates.</summary>
    public abstract IBoolDelegateMappedArgumentDataRecorderFactory BoolDelegate { get; }

    /// <summary>Handles creation of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/> using <see langword="void"/>-returning delegates.</summary>
    public abstract IVoidDelegateMappedArgumentDataRecorderFactory VoidDelegate { get; }
}
