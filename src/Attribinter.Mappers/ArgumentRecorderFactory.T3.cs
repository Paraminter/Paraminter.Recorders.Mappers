namespace Attribinter.Mappers;

using System;

/// <inheritdoc cref="IArgumentRecorderFactory{TParameter, TRecord, TData}"/>
public sealed class ArgumentRecorderFactory<TParameter, TRecord, TData> : IArgumentRecorderFactory<TParameter, TRecord, TData>
{
    private readonly IArgumentRecorderFactory InnerFactory;
    private readonly IParameterMapper<TParameter, TRecord, TData> Mapper;

    /// <summary>Instantiates a <see cref="ArgumentRecorderFactory{TParameter, TRecord, TData}"/>, handling creation of <see cref="IArgumentRecorder{TParameter, TData}"/>.</summary>
    /// <param name="innerFactory">Handles creation of <see cref="IArgumentRecorder{TParameter, TData}"/> using <see cref="IParameterMapper{TParameter, TRecord, TData}"/>.</param>
    /// <param name="mapper">The <see cref="IParameterMapper{TParameter, TRecord, TData}"/> used to create <see cref="IArgumentRecorder{TParameter, TData}"/>.</param>
    public ArgumentRecorderFactory(IArgumentRecorderFactory innerFactory, IParameterMapper<TParameter, TRecord, TData> mapper)
    {
        InnerFactory = innerFactory ?? throw new ArgumentNullException(nameof(innerFactory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    IArgumentRecorder<TParameter, TData> IArgumentRecorderFactory<TParameter, TRecord, TData>.Create(TRecord dataRecord)
    {
        if (dataRecord is null)
        {
            throw new ArgumentNullException(nameof(dataRecord));
        }

        return InnerFactory.Create(Mapper, dataRecord);
    }
}
