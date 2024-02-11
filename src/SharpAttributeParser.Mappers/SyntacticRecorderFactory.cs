namespace SharpAttributeParser.Mappers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers.Logging;

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

        public Recorder(ISyntacticMapper<TRecord> mapper, TRecord record, ISyntacticRecorderLogger logger)
        {
            Type = new TypeRecorder(mapper, record, logger);
            Constructor = new ConstructorRecorder(mapper, record, logger);
            Named = new NamedRecorder(mapper, record, logger);
        }

        ISyntacticTypeRecorder ISyntacticRecorder.Type => Type;
        ISyntacticConstructorRecorder ISyntacticRecorder.Constructor => Constructor;
        ISyntacticNamedRecorder ISyntacticRecorder.Named => Named;

        private sealed class TypeRecorder : ISyntacticTypeRecorder
        {
            private readonly ISyntacticMapper<TRecord> Mapper;
            private readonly TRecord Record;

            private readonly ISyntacticRecorderLogger Logger;

            public TypeRecorder(ISyntacticMapper<TRecord> mapper, TRecord record, ISyntacticRecorderLogger logger)
            {
                Mapper = mapper;
                Record = record;

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

                if (Mapper.Type.TryMapParameter(parameter, Record) is not ISyntacticMappedTypeRecorder argumentRecorder)
                {
                    Logger.TypeArgument.FailedToMapTypeParameterToRecorder();

                    return false;
                }

                return argumentRecorder.TryRecordArgument(syntax);
            }
        }

        private sealed class ConstructorRecorder : ISyntacticConstructorRecorder
        {
            private readonly ISyntacticNormalConstructorRecorder Normal;
            private readonly ISyntacticParamsConstructorRecorder Params;
            private readonly ISyntacticDefaultConstructorRecorder Default;

            public ConstructorRecorder(ISyntacticMapper<TRecord> mapper, TRecord record, ISyntacticRecorderLogger logger)
            {
                ConstructorMapper constructorMapper = new(mapper, record, logger);

                Normal = new NormalConstructorRecorder(constructorMapper, logger);
                Params = new ParamsConstructorRecorder(constructorMapper, logger);
                Default = new DefaultConstructorRecorder(constructorMapper, logger);
            }

            ISyntacticNormalConstructorRecorder ISyntacticConstructorRecorder.Normal => Normal;
            ISyntacticParamsConstructorRecorder ISyntacticConstructorRecorder.Params => Params;
            ISyntacticDefaultConstructorRecorder ISyntacticConstructorRecorder.Default => Default;

            private sealed class ConstructorMapper
            {
                private readonly ISyntacticMapper<TRecord> Mapper;
                private readonly TRecord Record;

                private readonly ISyntacticRecorderLogger Logger;

                public ConstructorMapper(ISyntacticMapper<TRecord> mapper, TRecord record, ISyntacticRecorderLogger logger)
                {
                    Mapper = mapper;
                    Record = record;

                    Logger = logger;
                }

                public ISyntacticMappedConstructorRecorder? TryMapParameter(IParameterSymbol parameter)
                {
                    if (Mapper.Constructor.TryMapParameter(parameter, Record) is not ISyntacticMappedConstructorRecorder argumentRecorder)
                    {
                        Logger.ConstructorArgument.FailedToMapConstructorParameterToRecorder();

                        return null;
                    }

                    return argumentRecorder;
                }
            }

            private sealed class NormalConstructorRecorder : ISyntacticNormalConstructorRecorder
            {
                private readonly ConstructorMapper Mapper;

                private readonly ISyntacticRecorderLogger Logger;

                public NormalConstructorRecorder(ConstructorMapper mapper, ISyntacticRecorderLogger logger)
                {
                    Mapper = mapper;

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

                    if (Mapper.TryMapParameter(parameter) is not ISyntacticMappedConstructorRecorder argumentRecorder)
                    {
                        return false;
                    }

                    return argumentRecorder.Normal.TryRecordArgument(syntax);
                }
            }

            private sealed class ParamsConstructorRecorder : ISyntacticParamsConstructorRecorder
            {
                private readonly ConstructorMapper Mapper;

                private readonly ISyntacticRecorderLogger Logger;

                public ParamsConstructorRecorder(ConstructorMapper mapper, ISyntacticRecorderLogger logger)
                {
                    Mapper = mapper;

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

                    if (Mapper.TryMapParameter(parameter) is not ISyntacticMappedConstructorRecorder argumentRecorder)
                    {
                        return false;
                    }

                    return argumentRecorder.Params.TryRecordArgument(elementSyntax);
                }
            }

            private sealed class DefaultConstructorRecorder : ISyntacticDefaultConstructorRecorder
            {
                private readonly ConstructorMapper Mapper;

                private readonly ISyntacticRecorderLogger Logger;

                public DefaultConstructorRecorder(ConstructorMapper mapper, ISyntacticRecorderLogger logger)
                {
                    Mapper = mapper;

                    Logger = logger;
                }

                bool ISyntacticDefaultConstructorRecorder.TryRecordArgument(IParameterSymbol parameter)
                {
                    if (parameter is null)
                    {
                        throw new ArgumentNullException(nameof(parameter));
                    }

                    using var _ = Logger.ConstructorArgument.BeginScopeRecordingDefaultConstructorArgument(parameter);

                    if (Mapper.TryMapParameter(parameter) is not ISyntacticMappedConstructorRecorder argumentRecorder)
                    {
                        return false;
                    }

                    return argumentRecorder.Default.TryRecordArgument();
                }
            }
        }

        private sealed class NamedRecorder : ISyntacticNamedRecorder
        {
            private readonly ISyntacticMapper<TRecord> ArgumentMapper;
            private readonly TRecord Record;

            private readonly ISyntacticRecorderLogger Logger;

            public NamedRecorder(ISyntacticMapper<TRecord> argumentMapper, TRecord record, ISyntacticRecorderLogger logger)
            {
                ArgumentMapper = argumentMapper;
                Record = record;

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

                if (ArgumentMapper.Named.TryMapParameter(parameterName, Record) is not ISyntacticMappedNamedRecorder argumentRecorder)
                {
                    Logger.NamedArgument.FailedToMapNamedParameterToRecorder();

                    return false;
                }

                return argumentRecorder.TryRecordArgument(syntax);
            }
        }
    }
}
