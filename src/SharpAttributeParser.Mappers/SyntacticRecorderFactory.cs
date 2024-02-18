namespace SharpAttributeParser.Mappers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers.Logging;
using SharpAttributeParser.SyntacticRecorderComponents;
using SharpAttributeParser.SyntacticRecorderComponents.SyntacticConstructorRecorderComponents;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="ISyntacticRecorderFactory"/>
public sealed class SyntacticRecorderFactory : ISyntacticRecorderFactory
{
    private readonly ISyntacticRecorderLoggerFactory LoggerFactory;

    /// <summary>Instantiates a <see cref="SyntacticRecorderFactory"/>, handling creation of <see cref="ISyntacticRecorder"/> using <see cref="ISyntacticMapper{TRecord}"/>.</summary>
    /// <param name="loggerFactory">Handles creation of the loggers used by the created recorders.</param>
    public SyntacticRecorderFactory(ISyntacticRecorderLoggerFactory? loggerFactory = null)
    {
        LoggerFactory = loggerFactory ?? NullSyntacticRecorderLoggerFactory.Instance;
    }

    ISyntacticRecorder ISyntacticRecorderFactory.Create<TRecord>(ISyntacticMapper<TRecord> mapper, TRecord dataRecord)
    {
        if (mapper is null)
        {
            throw new ArgumentNullException(nameof(mapper));
        }

        if (dataRecord is null)
        {
            throw new ArgumentNullException(nameof(dataRecord));
        }

        var recorderLogger = LoggerFactory.Create<ISyntacticRecorder>();

        return new Recorder<TRecord>(mapper, dataRecord, recorderLogger);
    }

    private sealed class Recorder<TRecord> : ISyntacticRecorder
    {
        private readonly ISyntacticTypeRecorder Type;
        private readonly ISyntacticConstructorRecorder Constructor;
        private readonly ISyntacticNamedRecorder Named;

        public Recorder(ISyntacticMapper<TRecord> mapper, TRecord dataRecord, ISyntacticRecorderLogger logger)
        {
            Type = new TypeRecorder(mapper, dataRecord, logger);
            Constructor = new ConstructorRecorder(mapper, dataRecord, logger);
            Named = new NamedRecorder(mapper, dataRecord, logger);
        }

        ISyntacticTypeRecorder ISyntacticRecorder.Type => Type;
        ISyntacticConstructorRecorder ISyntacticRecorder.Constructor => Constructor;
        ISyntacticNamedRecorder ISyntacticRecorder.Named => Named;

        private sealed class TypeRecorder : ISyntacticTypeRecorder
        {
            private readonly ISyntacticMapper<TRecord> Mapper;
            private readonly TRecord DataRecord;

            private readonly ISyntacticRecorderLogger Logger;

            public TypeRecorder(ISyntacticMapper<TRecord> mapper, TRecord dataRecord, ISyntacticRecorderLogger logger)
            {
                Mapper = mapper;
                DataRecord = dataRecord;

                Logger = logger;
            }

            bool ISyntacticTypeRecorder.TryRecordArgument(ITypeParameterSymbol parameter, ExpressionSyntax syntax)
            {
                if (parameter is null)
                {
                    throw new ArgumentNullException(nameof(parameter));
                }

                if (syntax is null)
                {
                    throw new ArgumentNullException(nameof(syntax));
                }

                using var _ = Logger.TypeArgument.BeginScopeRecordingTypeArgument(parameter, syntax);

                var recorder = Mapper.Type.MapParameter(parameter);

                return recorder.TryRecordArgument(DataRecord, syntax);
            }
        }

        private sealed class ConstructorRecorder : ISyntacticConstructorRecorder
        {
            private readonly ISyntacticNormalConstructorRecorder Normal;
            private readonly ISyntacticParamsConstructorRecorder Params;
            private readonly ISyntacticDefaultConstructorRecorder Default;

            public ConstructorRecorder(ISyntacticMapper<TRecord> mapper, TRecord dataRecord, ISyntacticRecorderLogger logger)
            {
                Normal = new NormalConstructorRecorder(mapper, dataRecord, logger);
                Params = new ParamsConstructorRecorder(mapper, dataRecord, logger);
                Default = new DefaultConstructorRecorder(mapper, dataRecord, logger);
            }

            ISyntacticNormalConstructorRecorder ISyntacticConstructorRecorder.Normal => Normal;
            ISyntacticParamsConstructorRecorder ISyntacticConstructorRecorder.Params => Params;
            ISyntacticDefaultConstructorRecorder ISyntacticConstructorRecorder.Default => Default;

            private sealed class NormalConstructorRecorder : ISyntacticNormalConstructorRecorder
            {
                private readonly ISyntacticMapper<TRecord> Mapper;
                private readonly TRecord DataRecord;

                private readonly ISyntacticRecorderLogger Logger;

                public NormalConstructorRecorder(ISyntacticMapper<TRecord> mapper, TRecord dataRecord, ISyntacticRecorderLogger logger)
                {
                    Mapper = mapper;
                    DataRecord = dataRecord;

                    Logger = logger;
                }

                bool ISyntacticNormalConstructorRecorder.TryRecordArgument(IParameterSymbol parameter, ExpressionSyntax syntax)
                {
                    if (parameter is null)
                    {
                        throw new ArgumentNullException(nameof(parameter));
                    }

                    if (syntax is null)
                    {
                        throw new ArgumentNullException(nameof(syntax));
                    }

                    using var _ = Logger.ConstructorArgument.BeginScopeRecordingNormalConstructorArgument(parameter, syntax);

                    var recorder = Mapper.Constructor.Normal.MapParameter(parameter);

                    return recorder.TryRecordArgument(DataRecord, syntax);
                }
            }

            private sealed class ParamsConstructorRecorder : ISyntacticParamsConstructorRecorder
            {
                private readonly ISyntacticMapper<TRecord> Mapper;
                private readonly TRecord DataRecord;

                private readonly ISyntacticRecorderLogger Logger;

                public ParamsConstructorRecorder(ISyntacticMapper<TRecord> mapper, TRecord dataRecord, ISyntacticRecorderLogger logger)
                {
                    Mapper = mapper;
                    DataRecord = dataRecord;

                    Logger = logger;
                }

                bool ISyntacticParamsConstructorRecorder.TryRecordArgument(IParameterSymbol parameter, IReadOnlyList<ExpressionSyntax> elementSyntax)
                {
                    if (parameter is null)
                    {
                        throw new ArgumentNullException(nameof(parameter));
                    }

                    if (elementSyntax is null)
                    {
                        throw new ArgumentNullException(nameof(elementSyntax));
                    }

                    using var _ = Logger.ConstructorArgument.BeginScopeRecordingParamsConstructorArgument(parameter, elementSyntax);

                    var recorder = Mapper.Constructor.Params.MapParameter(parameter);

                    return recorder.TryRecordArgument(DataRecord, elementSyntax);
                }
            }

            private sealed class DefaultConstructorRecorder : ISyntacticDefaultConstructorRecorder
            {
                private readonly ISyntacticMapper<TRecord> Mapper;
                private readonly TRecord DataRecord;

                private readonly ISyntacticRecorderLogger Logger;

                public DefaultConstructorRecorder(ISyntacticMapper<TRecord> mapper, TRecord dataRecord, ISyntacticRecorderLogger logger)
                {
                    Mapper = mapper;
                    DataRecord = dataRecord;

                    Logger = logger;
                }

                bool ISyntacticDefaultConstructorRecorder.TryRecordArgument(IParameterSymbol parameter)
                {
                    if (parameter is null)
                    {
                        throw new ArgumentNullException(nameof(parameter));
                    }

                    using var _ = Logger.ConstructorArgument.BeginScopeRecordingDefaultConstructorArgument(parameter);

                    var recorder = Mapper.Constructor.Default.MapParameter(parameter);

                    return recorder.TryRecordArgument(DataRecord);
                }
            }
        }

        private sealed class NamedRecorder : ISyntacticNamedRecorder
        {
            private readonly ISyntacticMapper<TRecord> Mapper;
            private readonly TRecord DataRecord;

            private readonly ISyntacticRecorderLogger Logger;

            public NamedRecorder(ISyntacticMapper<TRecord> mapper, TRecord dataRecord, ISyntacticRecorderLogger logger)
            {
                Mapper = mapper;
                DataRecord = dataRecord;

                Logger = logger;
            }

            bool ISyntacticNamedRecorder.TryRecordArgument(string parameterName, ExpressionSyntax syntax)
            {
                if (parameterName is null)
                {
                    throw new ArgumentNullException(nameof(parameterName));
                }

                if (syntax is null)
                {
                    throw new ArgumentNullException(nameof(syntax));
                }

                using var _ = Logger.NamedArgument.BeginScopeRecordingNamedArgument(parameterName, syntax);

                var recorder = Mapper.Named.MapParameter(parameterName);

                return recorder.TryRecordArgument(DataRecord, syntax);
            }
        }
    }
}
