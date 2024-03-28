namespace Attribinter.Mappers;

using System;

/// <inheritdoc cref="IArgumentRecorderFactory"/>
public sealed class ArgumentRecorderFactory : IArgumentRecorderFactory
{
    /// <summary>Instantiates a <see cref="ArgumentRecorderFactory"/>, handling creation of <see cref="IArgumentRecorder{TParameter, TData}"/>.</summary>
    public ArgumentRecorderFactory() { }

    IArgumentRecorderFactory<TParameter, TRecord, TData> IArgumentRecorderFactory.WithMapper<TParameter, TRecord, TData>(IParameterMapper<TParameter, TRecord, TData> mapper)
    {
        if (mapper is null)
        {
            throw new ArgumentNullException(nameof(mapper));
        }

        return new ArgumentRecorderFactory<TParameter, TRecord, TData>(mapper);
    }
}
