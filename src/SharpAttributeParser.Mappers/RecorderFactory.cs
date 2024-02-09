namespace SharpAttributeParser.Mappers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers.Logging;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="IRecorderFactory"/>
public sealed class RecorderFactory : IRecorderFactory
{
    private readonly IRecorderLoggerFactory LoggerFactory;

    /// <summary>Instantiates a <see cref="RecorderFactory"/>, handling creation of <see cref="IRecorder"/> using <see cref="IMapper{TRecord}"/>.</summary>
    /// <param name="loggerFactory">Handles creation of the loggers used by the created recorders.</param>
    public RecorderFactory(IRecorderLoggerFactory? loggerFactory = null)
    {
        LoggerFactory = loggerFactory ?? NullRecorderLoggerFactory.Instance;
    }

    IRecorder<TRecord> IRecorderFactory.Create<TRecord>(IMapper<TRecord> mapper, TRecord dataRecord)
    {
        if (mapper is null)
        {
            throw new ArgumentNullException(nameof(mapper));
        }

        if (dataRecord is null)
        {
            throw new ArgumentNullException(nameof(dataRecord));
        }

        var recorderLogger = LoggerFactory.Create<IRecorder<TRecord>>();

        return new Recorder<TRecord>(new Mapper<TRecord>(mapper), dataRecord, recorderLogger);
    }

    private sealed class Mapper<TRecord> : IMapper<TRecord>
    {
        private readonly IMapper<TRecord> WrappedMapper;

        public Mapper(IMapper<TRecord> wrappedMapper)
        {
            WrappedMapper = wrappedMapper;
        }

        IMappedTypeRecorder? IMapper<TRecord>.TryMapTypeParameter(ITypeParameterSymbol parameter, TRecord dataRecord) => WrappedMapper.TryMapTypeParameter(parameter, dataRecord);
        IMappedConstructorRecorder? IMapper<TRecord>.TryMapConstructorParameter(IParameterSymbol parameter, TRecord dataRecord) => WrappedMapper.TryMapConstructorParameter(parameter, dataRecord);
        IMappedNamedRecorder? IMapper<TRecord>.TryMapNamedParameter(string parameterName, TRecord dataRecord) => WrappedMapper.TryMapNamedParameter(parameterName, dataRecord);
    }

    private sealed class Recorder<TRecord> : IRecorder<TRecord>
    {
        private readonly TRecord Record;

        private readonly ITypeRecorder Type;
        private readonly IConstructorRecorder Constructor;
        private readonly INamedRecorder Named;

        public Recorder(IMapper<TRecord> argumentRecorderMapper, TRecord record, IRecorderLogger logger)
        {
            Record = record;

            Type = new TypeRecorder(argumentRecorderMapper, record, logger);
            Constructor = new ConstructorRecorder(argumentRecorderMapper, record, logger);
            Named = new NamedRecorder(argumentRecorderMapper, record, logger);
        }

        ITypeRecorder IRecorder.Type => Type;
        IConstructorRecorder IRecorder.Constructor => Constructor;
        INamedRecorder IRecorder.Named => Named;

        TRecord IRecorder<TRecord>.BuildRecord() => Record;

        private sealed class TypeRecorder : ITypeRecorder
        {
            private readonly IMapper<TRecord> RecorderMapper;
            private readonly TRecord RecordBuilder;

            private readonly IRecorderLogger Logger;

            public TypeRecorder(IMapper<TRecord> argumentRecorderMapper, TRecord recordBuilder, IRecorderLogger logger)
            {
                RecorderMapper = argumentRecorderMapper;
                RecordBuilder = recordBuilder;

                Logger = logger;
            }

            bool ITypeRecorder.TryRecordArgument(ITypeParameterSymbol parameter, ITypeSymbol argument, ExpressionSyntax syntax)
            {
                if (parameter is null)
                {
                    throw new ArgumentNullException(nameof(parameter));
                }

                if (argument is null)
                {
                    throw new ArgumentNullException(nameof(argument));
                }

                if (syntax is null)
                {
                    throw new ArgumentNullException(nameof(syntax));
                }

                using var _ = Logger.TypeArgument.BeginScopeRecordingTypeArgument(parameter, argument, syntax);

                if (RecorderMapper.TryMapTypeParameter(parameter, RecordBuilder) is not IMappedTypeRecorder argumentRecorder)
                {
                    Logger.TypeArgument.FailedToMapTypeParameterToRecorder();

                    return false;
                }

                return argumentRecorder.TryRecordArgument(argument, syntax);
            }
        }

        private sealed class ConstructorRecorder : IConstructorRecorder
        {
            private readonly IMapper<TRecord> RecorderMapper;
            private readonly TRecord RecordBuilder;

            private readonly IRecorderLogger Logger;

            public ConstructorRecorder(IMapper<TRecord> argumentRecorderMapper, TRecord recordBuilder, IRecorderLogger logger)
            {
                RecorderMapper = argumentRecorderMapper;
                RecordBuilder = recordBuilder;

                Logger = logger;
            }

            bool IConstructorRecorder.TryRecordArgument(IParameterSymbol parameter, object? argument, ExpressionSyntax syntax)
            {
                if (parameter is null)
                {
                    throw new ArgumentNullException(nameof(parameter));
                }

                if (syntax is null)
                {
                    throw new ArgumentNullException(nameof(syntax));
                }

                using var _ = Logger.ConstructorArgument.BeginScopeRecordingNormalConstructorlArgument(parameter, argument, syntax);

                if (TryMapParameter(parameter) is not IMappedConstructorRecorder argumentRecorder)
                {
                    return false;
                }

                return argumentRecorder.TryRecordArgument(argument, syntax);
            }

            bool IConstructorRecorder.TryRecordParamsArgument(IParameterSymbol parameter, object? argument, IReadOnlyList<ExpressionSyntax> elementSyntax)
            {
                if (parameter is null)
                {
                    throw new ArgumentNullException(nameof(parameter));
                }

                if (elementSyntax is null)
                {
                    throw new ArgumentNullException(nameof(elementSyntax));
                }

                using var _ = Logger.ConstructorArgument.BeginScopeRecordingParamsConstructorArgument(parameter, argument, elementSyntax);

                if (TryMapParameter(parameter) is not IMappedConstructorRecorder argumentRecorder)
                {
                    return false;
                }

                return argumentRecorder.TryRecordParamsArgument(argument, elementSyntax);
            }

            bool IConstructorRecorder.TryRecordDefaultArgument(IParameterSymbol parameter, object? argument)
            {
                if (parameter is null)
                {
                    throw new ArgumentNullException(nameof(parameter));
                }

                using var _ = Logger.ConstructorArgument.BeginScopeRecordingDefaultConstructorArgument(parameter, argument);

                if (TryMapParameter(parameter) is not IMappedConstructorRecorder argumentRecorder)
                {
                    return false;
                }

                return argumentRecorder.TryRecordDefaultArgument(argument);
            }

            private IMappedConstructorRecorder? TryMapParameter(IParameterSymbol parameter)
            {
                if (RecorderMapper.TryMapConstructorParameter(parameter, RecordBuilder) is not IMappedConstructorRecorder argumentRecorder)
                {
                    Logger.ConstructorArgument.FailedToMapConstructorParameterToRecorder();

                    return null;
                }

                return argumentRecorder;
            }
        }

        private sealed class NamedRecorder : INamedRecorder
        {
            private readonly IMapper<TRecord> RecorderMapper;
            private readonly TRecord RecordBuilder;

            private readonly IRecorderLogger Logger;

            public NamedRecorder(IMapper<TRecord> argumentRecorderMapper, TRecord recordBuilder, IRecorderLogger logger)
            {
                RecorderMapper = argumentRecorderMapper;
                RecordBuilder = recordBuilder;

                Logger = logger;
            }

            bool INamedRecorder.TryRecordArgument(string parameterName, object? argument, ExpressionSyntax syntax)
            {
                if (parameterName is null)
                {
                    throw new ArgumentNullException(nameof(parameterName));
                }

                if (syntax is null)
                {
                    throw new ArgumentNullException(nameof(syntax));
                }

                using var _ = Logger.NamedArgument.BeginScopeRecordingNamedArgument(parameterName, argument, syntax);

                if (RecorderMapper.TryMapNamedParameter(parameterName, RecordBuilder) is not IMappedNamedRecorder argumentRecorder)
                {
                    Logger.NamedArgument.FailedToMapNamedParameterToRecorder();

                    return false;
                }

                return argumentRecorder.TryRecordArgument(argument, syntax);
            }
        }
    }
}
