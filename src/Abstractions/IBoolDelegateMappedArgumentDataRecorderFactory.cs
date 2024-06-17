namespace Paraminter.Recorders.Mappers;

/// <summary>Handles creation of <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/> using <see cref="bool"/>-returning delegates.</summary>
public interface IBoolDelegateMappedArgumentDataRecorderFactory
{
    /// <summary>Creates a <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/> using the provided delegate.</summary>
    /// <typeparam name="TRecord">The type of the record to which data is recorded.</typeparam>
    /// <typeparam name="TArgumentData">The type representing data about the arguments.</typeparam>
    /// <param name="recorderDelegate">The delegate reponsible for recording data.</param>
    /// <returns>The created <see cref="IMappedArgumentDataRecorder{TRecord, TArgumentData}"/>.</returns>
    public abstract IMappedArgumentDataRecorder<TRecord, TArgumentData> Create<TRecord, TArgumentData>(
        DBoolArgumentDataRecorder<TRecord, TArgumentData> recorderDelegate);
}
