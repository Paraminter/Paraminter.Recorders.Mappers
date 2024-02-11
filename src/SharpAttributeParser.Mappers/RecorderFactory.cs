namespace SharpAttributeParser.Mappers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser.Mappers.Logging;
using SharpAttributeParser.Mappers.MappedRecorders;
using SharpAttributeParser.RecorderComponents;
using SharpAttributeParser.RecorderComponents.ConstructorRecorderComponents;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="IRecorderFactory"/>
public sealed class RecorderFactory : IRecorderFactory
{
    private readonly IRecorderLoggerFactory LoggerFactory;

    /// <summary>Instantiates a <see cref="RecorderFactory"/>, handling creation of <see cref="IRecorder"/> using <see cref="IMapper"/>.</summary>
    /// <param name="loggerFactory">Handles creation of the loggers used by the created recorders.</param>
    public RecorderFactory(IRecorderLoggerFactory? loggerFactory = null)
    {
        LoggerFactory = loggerFactory ?? NullRecorderLoggerFactory.Instance;
    }

    IRecorder IRecorderFactory.Create(IMapper mapper)
    {
        if (mapper is null)
        {
            throw new ArgumentNullException(nameof(mapper));
        }

        var recorderLogger = LoggerFactory.Create<IRecorder>();

        return new Recorder(mapper, recorderLogger);
    }

    private sealed class Recorder : IRecorder
    {
        private readonly ITypeRecorder Type;
        private readonly IConstructorRecorder Constructor;
        private readonly INamedRecorder Named;

        public Recorder(IMapper mapper, IRecorderLogger logger)
        {
            Type = new TypeRecorder(mapper, logger);
            Constructor = new ConstructorRecorder(mapper, logger);
            Named = new NamedRecorder(mapper, logger);
        }

        ITypeRecorder IRecorder.Type => Type;
        IConstructorRecorder IRecorder.Constructor => Constructor;
        INamedRecorder IRecorder.Named => Named;

        private sealed class TypeRecorder : ITypeRecorder
        {
            private readonly IMapper Mapper;

            private readonly IRecorderLogger Logger;

            public TypeRecorder(IMapper mapper, IRecorderLogger logger)
            {
                Mapper = mapper;

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

                if (Mapper.Type.TryMapParameter(parameter) is not IMappedTypeRecorder argumentRecorder)
                {
                    Logger.TypeArgument.FailedToMapTypeParameterToRecorder();

                    return false;
                }

                return argumentRecorder.TryRecordArgument(argument, syntax);
            }
        }

        private sealed class ConstructorRecorder : IConstructorRecorder
        {
            private readonly INormalConstructorRecorder Normal;
            private readonly IParamsConstructorRecorder Params;
            private readonly IDefaultConstructorRecorder Default;

            public ConstructorRecorder(IMapper mapper, IRecorderLogger logger)
            {
                ConstructorMapper constructorMapper = new(mapper, logger);

                Normal = new NormalConstructorRecorder(constructorMapper, logger);
                Params = new ParamsConstructorRecorder(constructorMapper, logger);
                Default = new DefaultConstructorRecorder(constructorMapper, logger);
            }

            INormalConstructorRecorder IConstructorRecorder.Normal => Normal;
            IParamsConstructorRecorder IConstructorRecorder.Params => Params;
            IDefaultConstructorRecorder IConstructorRecorder.Default => Default;

            private sealed class ConstructorMapper
            {
                private readonly IMapper Mapper;

                private readonly IRecorderLogger Logger;

                public ConstructorMapper(IMapper mapper, IRecorderLogger logger)
                {
                    Mapper = mapper;

                    Logger = logger;
                }

                public IMappedConstructorRecorder? TryMapParameter(IParameterSymbol parameter)
                {
                    if (Mapper.Constructor.TryMapParameter(parameter) is not IMappedConstructorRecorder argumentRecorder)
                    {
                        Logger.ConstructorArgument.FailedToMapConstructorParameterToRecorder();

                        return null;
                    }

                    return argumentRecorder;
                }
            }

            private sealed class NormalConstructorRecorder : INormalConstructorRecorder
            {
                private readonly ConstructorMapper Mapper;

                private readonly IRecorderLogger Logger;

                public NormalConstructorRecorder(ConstructorMapper mapper, IRecorderLogger logger)
                {
                    Mapper = mapper;

                    Logger = logger;
                }

                bool INormalConstructorRecorder.TryRecordArgument(IParameterSymbol parameter, object? argument, ExpressionSyntax syntax)
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

                    if (Mapper.TryMapParameter(parameter) is not IMappedConstructorRecorder argumentRecorder)
                    {
                        return false;
                    }

                    return argumentRecorder.Normal.TryRecordArgument(argument, syntax);
                }

            }

            private sealed class ParamsConstructorRecorder : IParamsConstructorRecorder
            {
                private readonly ConstructorMapper Mapper;

                private readonly IRecorderLogger Logger;

                public ParamsConstructorRecorder(ConstructorMapper mapper, IRecorderLogger logger)
                {
                    Mapper = mapper;

                    Logger = logger;
                }

                bool IParamsConstructorRecorder.TryRecordArgument(IParameterSymbol parameter, object? argument, IReadOnlyList<ExpressionSyntax> elementSyntax)
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

                    if (Mapper.TryMapParameter(parameter) is not IMappedConstructorRecorder argumentRecorder)
                    {
                        return false;
                    }

                    return argumentRecorder.Params.TryRecordArgument(argument, elementSyntax);
                }
            }

            private sealed class DefaultConstructorRecorder : IDefaultConstructorRecorder
            {
                private readonly ConstructorMapper Mapper;

                private readonly IRecorderLogger Logger;

                public DefaultConstructorRecorder(ConstructorMapper mapper, IRecorderLogger logger)
                {
                    Mapper = mapper;

                    Logger = logger;
                }

                bool IDefaultConstructorRecorder.TryRecordArgument(IParameterSymbol parameter, object? argument)
                {
                    if (parameter is null)
                    {
                        throw new ArgumentNullException(nameof(parameter));
                    }

                    using var _ = Logger.ConstructorArgument.BeginScopeRecordingDefaultConstructorArgument(parameter, argument);

                    if (Mapper.TryMapParameter(parameter) is not IMappedConstructorRecorder argumentRecorder)
                    {
                        return false;
                    }

                    return argumentRecorder.Default.TryRecordArgument(argument);
                }
            }
        }

        private sealed class NamedRecorder : INamedRecorder
        {
            private readonly IMapper RecorderMapper;

            private readonly IRecorderLogger Logger;

            public NamedRecorder(IMapper argumentRecorderMapper, IRecorderLogger logger)
            {
                RecorderMapper = argumentRecorderMapper;

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

                if (RecorderMapper.Named.TryMapParameter(parameterName) is not IMappedNamedRecorder argumentRecorder)
                {
                    Logger.NamedArgument.FailedToMapNamedParameterToRecorder();

                    return false;
                }

                return argumentRecorder.TryRecordArgument(argument, syntax);
            }
        }
    }
}
