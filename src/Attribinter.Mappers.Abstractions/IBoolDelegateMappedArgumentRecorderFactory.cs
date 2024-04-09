namespace Attribinter.Mappers;

/// <summary>Handles creation of <see cref="IMappedArgumentRecorder{TRecord, TData}"/> using <see cref="bool"/>-returning delegates.</summary>
public interface IBoolDelegateMappedArgumentRecorderFactory
{
    /// <summary>Creates a <see cref="IMappedArgumentRecorder{TRecord, TData}"/> using the provided delegate.</summary>
    /// <typeparam name="TRecord">The type of the data record to which data is recorded.</typeparam>
    /// <typeparam name="TData">The type of the recorded data.</typeparam>
    /// <param name="recorderDelegate">The delegate reponsible for recording data.</param>
    /// <returns>The created <see cref="IMappedArgumentRecorder{TRecord, TData}"/>.</returns>
    public abstract IMappedArgumentRecorder<TRecord, TData> Create<TRecord, TData>(DAttemptingArgumentRecorder<TRecord, TData> recorderDelegate);
}
