namespace Paraminter.Mappers;

/// <summary>Handles creation of <see cref="IMappedArgumentExistenceRecorder{TRecord}"/> using <see langword="void"/>-returning delegates.</summary>
public interface IVoidDelegateMappedArgumentExistenceRecorderFactory
{
    /// <summary>Creates a <see cref="IMappedArgumentExistenceRecorder{TRecord}"/> using the provided delegate.</summary>
    /// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
    /// <param name="recorderDelegate">The delegate reponsible for recording data.</param>
    /// <returns>The created <see cref="IMappedArgumentExistenceRecorder{TRecord}"/>.</returns>
    public abstract IMappedArgumentExistenceRecorder<TRecord> Create<TRecord>(
        DVoidArgumentExistenceRecorder<TRecord> recorderDelegate);
}
