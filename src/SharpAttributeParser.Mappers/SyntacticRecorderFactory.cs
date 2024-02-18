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

    /// <summary>Instantiates a <see cref="SyntacticRecorderFactory"/>, handling creation of <see cref="ISyntacticRecorder"/> using <see cref="ISyntacticMapper"/>.</summary>
    /// <param name="loggerFactory">Handles creation of the loggers used by the created recorders.</param>
    public SyntacticRecorderFactory(ISyntacticRecorderLoggerFactory? loggerFactory = null)
    {
        LoggerFactory = loggerFactory ?? NullSyntacticRecorderLoggerFactory.Instance;
    }

    ISyntacticRecorder ISyntacticRecorderFactory.Create(ISyntacticMapper mapper)
    {
        if (mapper is null)
        {
            throw new ArgumentNullException(nameof(mapper));
        }

        var recorderLogger = LoggerFactory.Create<ISyntacticRecorder>();

        return new Recorder(mapper, recorderLogger);
    }

    private sealed class Recorder : ISyntacticRecorder
    {
        private readonly ISyntacticTypeRecorder Type;
        private readonly ISyntacticConstructorRecorder Constructor;
        private readonly ISyntacticNamedRecorder Named;

        public Recorder(ISyntacticMapper mapper, ISyntacticRecorderLogger logger)
        {
            Type = new TypeRecorder(mapper, logger);
            Constructor = new ConstructorRecorder(mapper, logger);
            Named = new NamedRecorder(mapper, logger);
        }

        ISyntacticTypeRecorder ISyntacticRecorder.Type => Type;
        ISyntacticConstructorRecorder ISyntacticRecorder.Constructor => Constructor;
        ISyntacticNamedRecorder ISyntacticRecorder.Named => Named;

        private sealed class TypeRecorder : ISyntacticTypeRecorder
        {
            private readonly ISyntacticMapper Mapper;

            private readonly ISyntacticRecorderLogger Logger;

            public TypeRecorder(ISyntacticMapper mapper, ISyntacticRecorderLogger logger)
            {
                Mapper = mapper;

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

                return recorder.TryRecordArgument(syntax);
            }
        }

        private sealed class ConstructorRecorder : ISyntacticConstructorRecorder
        {
            private readonly ISyntacticNormalConstructorRecorder Normal;
            private readonly ISyntacticParamsConstructorRecorder Params;
            private readonly ISyntacticDefaultConstructorRecorder Default;

            public ConstructorRecorder(ISyntacticMapper mapper, ISyntacticRecorderLogger logger)
            {
                Normal = new NormalConstructorRecorder(mapper, logger);
                Params = new ParamsConstructorRecorder(mapper, logger);
                Default = new DefaultConstructorRecorder(mapper, logger);
            }

            ISyntacticNormalConstructorRecorder ISyntacticConstructorRecorder.Normal => Normal;
            ISyntacticParamsConstructorRecorder ISyntacticConstructorRecorder.Params => Params;
            ISyntacticDefaultConstructorRecorder ISyntacticConstructorRecorder.Default => Default;

            private sealed class NormalConstructorRecorder : ISyntacticNormalConstructorRecorder
            {
                private readonly ISyntacticMapper Mapper;

                private readonly ISyntacticRecorderLogger Logger;

                public NormalConstructorRecorder(ISyntacticMapper mapper, ISyntacticRecorderLogger logger)
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

                    var recorder = Mapper.Constructor.Normal.MapParameter(parameter);

                    return recorder.TryRecordArgument(syntax);
                }
            }

            private sealed class ParamsConstructorRecorder : ISyntacticParamsConstructorRecorder
            {
                private readonly ISyntacticMapper Mapper;

                private readonly ISyntacticRecorderLogger Logger;

                public ParamsConstructorRecorder(ISyntacticMapper mapper, ISyntacticRecorderLogger logger)
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

                    var recorder = Mapper.Constructor.Params.MapParameter(parameter);

                    return recorder.TryRecordArgument(elementSyntax);
                }
            }

            private sealed class DefaultConstructorRecorder : ISyntacticDefaultConstructorRecorder
            {
                private readonly ISyntacticMapper Mapper;

                private readonly ISyntacticRecorderLogger Logger;

                public DefaultConstructorRecorder(ISyntacticMapper mapper, ISyntacticRecorderLogger logger)
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

                    var recorder = Mapper.Constructor.Default.MapParameter(parameter);

                    return recorder.TryRecordArgument();
                }
            }
        }

        private sealed class NamedRecorder : ISyntacticNamedRecorder
        {
            private readonly ISyntacticMapper Mapper;

            private readonly ISyntacticRecorderLogger Logger;

            public NamedRecorder(ISyntacticMapper mapper, ISyntacticRecorderLogger logger)
            {
                Mapper = mapper;

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

                return recorder.TryRecordArgument(syntax);
            }
        }
    }
}
