namespace Attribinter.Mappers;

/// <summary>Handles creation of <see cref="IMappedArgumentRecorder{TRecord, TData}"/>.</summary>
public interface IMappedArgumentRecorderFactory
{
    /// <summary>Creates a <see cref="IMappedArgumentRecorder{TRecord, TData}"/> using the provided <see cref="bool"/>-returning delegate.</summary>
    /// <typeparam name="TRecord">The type of the data record to which data is recorded.</typeparam>
    /// <typeparam name="TData">The type of the recorded data.</typeparam>
    /// <param name="recorderDelegate">The delegate reponsible for recording data.</param>
    /// <returns>The created <see cref="IMappedArgumentRecorder{TRecord, TData}"/>.</returns>
    public abstract IMappedArgumentRecorder<TRecord, TData> Create<TRecord, TData>(DAttemptingArgumentRecorder<TRecord, TData> recorderDelegate);

    /// <summary>Creates a <see cref="IMappedArgumentRecorder{TRecord, TData}"/> using the provided <see langword="void"/>-returning delegate.</summary>
    /// <typeparam name="TRecord">The type of the data record to which data is recorded.</typeparam>
    /// <typeparam name="TData">The type of the recorded data.</typeparam>
    /// <param name="recorderDelegate">The delegate reponsible for recording data.</param>
    /// <returns>The created <see cref="IMappedArgumentRecorder{TRecord, TData}"/>.</returns>
    public abstract IMappedArgumentRecorder<TRecord, TData> Create<TRecord, TData>(DCertainArgumentRecorder<TRecord, TData> recorderDelegate);
}
