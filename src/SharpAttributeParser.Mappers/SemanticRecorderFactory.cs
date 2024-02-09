namespace SharpAttributeParser.Mappers;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.Logging;

using System;

/// <inheritdoc cref="ISemanticRecorderFactory"/>
public sealed class SemanticRecorderFactory : ISemanticRecorderFactory
{
    private readonly ISemanticRecorderLoggerFactory LoggerFactory;

    /// <summary>Instantiates a <see cref="SemanticRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder"/> using <see cref="ISemanticMapper{TRecord}"/>.</summary>
    /// <param name="loggerFactory">Handles creation of the loggers used by the created recorders.</param>
    public SemanticRecorderFactory(ISemanticRecorderLoggerFactory? loggerFactory = null)
    {
        LoggerFactory = loggerFactory ?? NullSemanticRecorderLoggerFactory.Instance;
    }

    ISemanticRecorder<TRecord> ISemanticRecorderFactory.Create<TRecord>(ISemanticMapper<TRecord> mapper, TRecord dataRecord)
    {
        if (mapper is null)
        {
            throw new ArgumentNullException(nameof(mapper));
        }

        if (dataRecord is null)
        {
            throw new ArgumentNullException(nameof(dataRecord));
        }

        var recorderLogger = LoggerFactory.Create<ISemanticRecorder<TRecord>>();

        return new Recorder<TRecord>(new Mapper<TRecord>(mapper), dataRecord, recorderLogger);
    }

    private sealed class Mapper<TRecord> : ISemanticMapper<TRecord>
    {
        private readonly ISemanticMapper<TRecord> WrappedMapper;

        public Mapper(ISemanticMapper<TRecord> wrappedMapper)
        {
            WrappedMapper = wrappedMapper;
        }

        IMappedSemanticTypeRecorder? ISemanticMapper<TRecord>.TryMapTypeParameter(ITypeParameterSymbol parameter, TRecord dataRecord) => WrappedMapper.TryMapTypeParameter(parameter, dataRecord);
        IMappedSemanticConstructorRecorder? ISemanticMapper<TRecord>.TryMapConstructorParameter(IParameterSymbol parameter, TRecord dataRecord) => WrappedMapper.TryMapConstructorParameter(parameter, dataRecord);
        IMappedSemanticNamedRecorder? ISemanticMapper<TRecord>.TryMapNamedParameter(string parameterName, TRecord dataRecord) => WrappedMapper.TryMapNamedParameter(parameterName, dataRecord);
    }

    private sealed class Recorder<TRecord> : ISemanticRecorder<TRecord>
    {
        private readonly TRecord Record;

        private readonly ISemanticTypeRecorder Type;
        private readonly ISemanticConstructorRecorder Constructor;
        private readonly ISemanticNamedRecorder Named;

        public Recorder(ISemanticMapper<TRecord> argumentRecorderMapper, TRecord record, ISemanticRecorderLogger logger)
        {
            Record = record;

            Type = new TypeRecorder(argumentRecorderMapper, record, logger);
            Constructor = new ConstructorRecorder(argumentRecorderMapper, record, logger);
            Named = new NamedRecorder(argumentRecorderMapper, record, logger);
        }

        ISemanticTypeRecorder ISemanticRecorder.Type => Type;
        ISemanticConstructorRecorder ISemanticRecorder.Constructor => Constructor;
        ISemanticNamedRecorder ISemanticRecorder.Named => Named;

        TRecord ISemanticRecorder<TRecord>.BuildRecord() => Record;

        private sealed class TypeRecorder : ISemanticTypeRecorder
        {
            private readonly ISemanticMapper<TRecord> RecorderMapper;
            private readonly TRecord Record;

            private readonly ISemanticRecorderLogger Logger;

            public TypeRecorder(ISemanticMapper<TRecord> argumentRecorderMapper, TRecord record, ISemanticRecorderLogger logger)
            {
                RecorderMapper = argumentRecorderMapper;
                Record = record;

                Logger = logger;
            }

            bool ISemanticTypeRecorder.TryRecordArgument(ITypeParameterSymbol parameter, ITypeSymbol argument)
            {
                if (parameter is null)
                {
                    throw new ArgumentNullException(nameof(parameter));
                }

                if (argument is null)
                {
                    throw new ArgumentNullException(nameof(argument));
                }

                using var _ = Logger.TypeArgument.BeginScopeRecordingTypeArgument(parameter, argument);

                if (RecorderMapper.TryMapTypeParameter(parameter, Record) is not IMappedSemanticTypeRecorder argumentRecorder)
                {
                    Logger.TypeArgument.FailedToMapTypeParameterToRecorder();

                    return false;
                }

                return argumentRecorder.TryRecordArgument(argument);
            }
        }

        private sealed class ConstructorRecorder : ISemanticConstructorRecorder
        {
            private readonly ISemanticMapper<TRecord> RecorderMapper;
            private readonly TRecord RecordBuilder;

            private readonly ISemanticRecorderLogger Logger;

            public ConstructorRecorder(ISemanticMapper<TRecord> argumentRecorderMapper, TRecord recordBuilder, ISemanticRecorderLogger logger)
            {
                RecorderMapper = argumentRecorderMapper;
                RecordBuilder = recordBuilder;

                Logger = logger;
            }

            bool ISemanticConstructorRecorder.TryRecordArgument(IParameterSymbol parameter, object? argument)
            {
                if (parameter is null)
                {
                    throw new ArgumentNullException(nameof(parameter));
                }

                using var _ = Logger.ConstructorArgument.BeginScopeRecordingConstructorArgument(parameter, argument);

                if (RecorderMapper.TryMapConstructorParameter(parameter, RecordBuilder) is not IMappedSemanticConstructorRecorder argumentRecorder)
                {
                    Logger.ConstructorArgument.FailedToMapConstructorParameterToRecorder();

                    return false;
                }

                return argumentRecorder.TryRecordArgument(argument);
            }
        }

        private sealed class NamedRecorder : ISemanticNamedRecorder
        {
            private readonly ISemanticMapper<TRecord> RecorderMapper;
            private readonly TRecord RecordBuilder;

            private readonly ISemanticRecorderLogger Logger;

            public NamedRecorder(ISemanticMapper<TRecord> argumentRecorderMapper, TRecord recordBuilder, ISemanticRecorderLogger logger)
            {
                RecorderMapper = argumentRecorderMapper;
                RecordBuilder = recordBuilder;

                Logger = logger;
            }

            bool ISemanticNamedRecorder.TryRecordArgument(string parameterName, object? argument)
            {
                if (parameterName is null)
                {
                    throw new ArgumentNullException(nameof(parameterName));
                }

                using var _ = Logger.NamedArgument.BeginScopeRecordingNamedArgument(parameterName, argument);

                if (RecorderMapper.TryMapNamedParameter(parameterName, RecordBuilder) is not IMappedSemanticNamedRecorder argumentRecorder)
                {
                    Logger.NamedArgument.FailedToMapNamedParameterToRecorder();

                    return false;
                }

                return argumentRecorder.TryRecordArgument(argument);
            }
        }
    }
}
