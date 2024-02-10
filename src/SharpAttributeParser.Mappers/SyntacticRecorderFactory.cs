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

        public Recorder(ISyntacticMapper<TRecord> argumentRecorderMapper, TRecord record, ISyntacticRecorderLogger logger)
        {
            Type = new TypeRecorder(argumentRecorderMapper, record, logger);
            Constructor = new ConstructorRecorder(argumentRecorderMapper, record, logger);
            Named = new NamedRecorder(argumentRecorderMapper, record, logger);
        }

        ISyntacticTypeRecorder ISyntacticRecorder.Type => Type;
        ISyntacticConstructorRecorder ISyntacticRecorder.Constructor => Constructor;
        ISyntacticNamedRecorder ISyntacticRecorder.Named => Named;

        private sealed class TypeRecorder : ISyntacticTypeRecorder
        {
            private readonly ISyntacticMapper<TRecord> ArgumentMapper;
            private readonly TRecord Record;

            private readonly ISyntacticRecorderLogger Logger;

            public TypeRecorder(ISyntacticMapper<TRecord> argumentMapper, TRecord record, ISyntacticRecorderLogger logger)
            {
                ArgumentMapper = argumentMapper;
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

                if (ArgumentMapper.TryMapTypeParameter(parameter, Record) is not IMappedSyntacticTypeRecorder argumentRecorder)
                {
                    Logger.TypeArgument.FailedToMapTypeParameterToRecorder();

                    return false;
                }

                return argumentRecorder.TryRecordArgument(syntax);
            }
        }

        private sealed class ConstructorRecorder : ISyntacticConstructorRecorder
        {
            private readonly ISyntacticMapper<TRecord> ArgumentMapper;
            private readonly TRecord Record;

            private readonly ISyntacticRecorderLogger Logger;

            public ConstructorRecorder(ISyntacticMapper<TRecord> argumentMapper, TRecord record, ISyntacticRecorderLogger logger)
            {
                ArgumentMapper = argumentMapper;
                Record = record;

                Logger = logger;
            }

            bool ISyntacticConstructorRecorder.TryRecordArgument(IParameterSymbol parameter, ExpressionSyntax syntax)
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

                if (TryMapParameter(parameter) is not IMappedSyntacticConstructorRecorder argumentRecorder)
                {
                    return false;
                }

                return argumentRecorder.TryRecordArgument(syntax);
            }

            bool ISyntacticConstructorRecorder.TryRecordParamsArgument(IParameterSymbol parameter, IReadOnlyList<ExpressionSyntax> elementSyntax)
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

                if (TryMapParameter(parameter) is not IMappedSyntacticConstructorRecorder argumentRecorder)
                {
                    return false;
                }

                return argumentRecorder.TryRecordParamsArgument(elementSyntax);
            }

            bool ISyntacticConstructorRecorder.TryRecordDefaultArgument(IParameterSymbol parameter)
            {
                if (parameter is null)
                {
                    throw new ArgumentNullException(nameof(parameter));
                }

                using var _ = Logger.ConstructorArgument.BeginScopeRecordingDefaultConstructorArgument(parameter);

                if (TryMapParameter(parameter) is not IMappedSyntacticConstructorRecorder argumentRecorder)
                {
                    return false;
                }

                return argumentRecorder.TryRecordDefaultArgument();
            }

            private IMappedSyntacticConstructorRecorder? TryMapParameter(IParameterSymbol parameter)
            {
                if (ArgumentMapper.TryMapConstructorParameter(parameter, Record) is not IMappedSyntacticConstructorRecorder argumentRecorder)
                {
                    Logger.ConstructorArgument.FailedToMapConstructorParameterToRecorder();

                    return null;
                }

                return argumentRecorder;
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

                if (ArgumentMapper.TryMapNamedParameter(parameterName, Record) is not IMappedSyntacticNamedRecorder argumentRecorder)
                {
                    Logger.NamedArgument.FailedToMapNamedParameterToRecorder();

                    return false;
                }

                return argumentRecorder.TryRecordArgument(syntax);
            }
        }
    }
}
