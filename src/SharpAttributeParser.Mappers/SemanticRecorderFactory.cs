namespace SharpAttributeParser.Mappers;

using Microsoft.CodeAnalysis;

using SharpAttributeParser.Mappers.Logging;
using SharpAttributeParser.SemanticRecorderComponents;

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

        public Recorder(ISemanticMapper<TRecord> mapper, TRecord dataRecord, ISemanticRecorderLogger logger)
        {
            Type = new TypeRecorder(mapper, dataRecord, logger);
            Constructor = new ConstructorRecorder(mapper, dataRecord, logger);
            Named = new NamedRecorder(mapper, dataRecord, logger);
        }

        ISemanticTypeRecorder ISemanticRecorder.Type => Type;
        ISemanticConstructorRecorder ISemanticRecorder.Constructor => Constructor;
        ISemanticNamedRecorder ISemanticRecorder.Named => Named;

        private sealed class TypeRecorder : ISemanticTypeRecorder
        {
            private readonly ISemanticMapper<TRecord> Mapper;
            private readonly TRecord DataRecord;

            private readonly ISemanticRecorderLogger Logger;

            public TypeRecorder(ISemanticMapper<TRecord> mapper, TRecord dataRecord, ISemanticRecorderLogger logger)
            {
                Mapper = mapper;
                DataRecord = dataRecord;

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

                var recorder = Mapper.Type.MapParameter(parameter);

                return recorder.TryRecordArgument(DataRecord, argument);
            }
        }

        private sealed class ConstructorRecorder : ISemanticConstructorRecorder
        {
            private readonly ISemanticMapper<TRecord> Mapper;
            private readonly TRecord DataRecord;

            private readonly ISemanticRecorderLogger Logger;

            public ConstructorRecorder(ISemanticMapper<TRecord> mapper, TRecord dataRecord, ISemanticRecorderLogger logger)
            {
                Mapper = mapper;
                DataRecord = dataRecord;

                Logger = logger;
            }

            bool ISemanticConstructorRecorder.TryRecordArgument(IParameterSymbol parameter, object? argument)
            {
                if (parameter is null)
                {
                    throw new ArgumentNullException(nameof(parameter));
                }

                using var _ = Logger.ConstructorArgument.BeginScopeRecordingConstructorArgument(parameter, argument);

                var recorder = Mapper.Constructor.MapParameter(parameter);

                return recorder.TryRecordArgument(DataRecord, argument);
            }
        }

        private sealed class NamedRecorder : ISemanticNamedRecorder
        {
            private readonly ISemanticMapper<TRecord> Mapper;
            private readonly TRecord DataRecord;

            private readonly ISemanticRecorderLogger Logger;

            public NamedRecorder(ISemanticMapper<TRecord> mapper, TRecord dataRecord, ISemanticRecorderLogger logger)
            {
                Mapper = mapper;
                DataRecord = dataRecord;

                Logger = logger;
            }

            bool ISemanticNamedRecorder.TryRecordArgument(string parameterName, object? argument)
            {
                if (parameterName is null)
                {
                    throw new ArgumentNullException(nameof(parameterName));
                }

                using var _ = Logger.NamedArgument.BeginScopeRecordingNamedArgument(parameterName, argument);

                var recorder = Mapper.Named.MapParameter(parameterName);

                return recorder.TryRecordArgument(DataRecord, argument);
            }
        }
    }
}
