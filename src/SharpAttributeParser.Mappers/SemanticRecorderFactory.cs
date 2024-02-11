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

    ISemanticRecorder ISemanticRecorderFactory.Create<TRecord>(ISemanticMapper<TRecord> mapper, TRecord dataRecord)
    {
        if (mapper is null)
        {
            throw new ArgumentNullException(nameof(mapper));
        }

        if (dataRecord is null)
        {
            throw new ArgumentNullException(nameof(dataRecord));
        }

        var recorderLogger = LoggerFactory.Create<ISemanticRecorder>();

        return new Recorder<TRecord>(mapper, dataRecord, recorderLogger);
    }

    private sealed class Recorder<TRecord> : ISemanticRecorder
    {
        private readonly ISemanticTypeRecorder Type;
        private readonly ISemanticConstructorRecorder Constructor;
        private readonly ISemanticNamedRecorder Named;

        public Recorder(ISemanticMapper<TRecord> argumentRecorderMapper, TRecord record, ISemanticRecorderLogger logger)
        {
            Type = new TypeRecorder(argumentRecorderMapper, record, logger);
            Constructor = new ConstructorRecorder(argumentRecorderMapper, record, logger);
            Named = new NamedRecorder(argumentRecorderMapper, record, logger);
        }

        ISemanticTypeRecorder ISemanticRecorder.Type => Type;
        ISemanticConstructorRecorder ISemanticRecorder.Constructor => Constructor;
        ISemanticNamedRecorder ISemanticRecorder.Named => Named;

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

                if (RecorderMapper.Type.TryMapParameter(parameter, Record) is not ISemanticMappedTypeRecorder argumentRecorder)
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
            private readonly TRecord Record;

            private readonly ISemanticRecorderLogger Logger;

            public ConstructorRecorder(ISemanticMapper<TRecord> argumentRecorderMapper, TRecord record, ISemanticRecorderLogger logger)
            {
                RecorderMapper = argumentRecorderMapper;
                Record = record;

                Logger = logger;
            }

            bool ISemanticConstructorRecorder.TryRecordArgument(IParameterSymbol parameter, object? argument)
            {
                if (parameter is null)
                {
                    throw new ArgumentNullException(nameof(parameter));
                }

                using var _ = Logger.ConstructorArgument.BeginScopeRecordingConstructorArgument(parameter, argument);

                if (RecorderMapper.Constructor.TryMapParameter(parameter, Record) is not ISemanticMappedConstructorRecorder argumentRecorder)
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
            private readonly TRecord Record;

            private readonly ISemanticRecorderLogger Logger;

            public NamedRecorder(ISemanticMapper<TRecord> argumentRecorderMapper, TRecord record, ISemanticRecorderLogger logger)
            {
                RecorderMapper = argumentRecorderMapper;
                Record = record;

                Logger = logger;
            }

            bool ISemanticNamedRecorder.TryRecordArgument(string parameterName, object? argument)
            {
                if (parameterName is null)
                {
                    throw new ArgumentNullException(nameof(parameterName));
                }

                using var _ = Logger.NamedArgument.BeginScopeRecordingNamedArgument(parameterName, argument);

                if (RecorderMapper.Named.TryMapParameter(parameterName, Record) is not ISemanticMappedNamedRecorder argumentRecorder)
                {
                    Logger.NamedArgument.FailedToMapNamedParameterToRecorder();

                    return false;
                }

                return argumentRecorder.TryRecordArgument(argument);
            }
        }
    }
}
