namespace SharpAttributeParser.Mappers.Logging;

using SharpAttributeParser.Mappers.Logging.RecorderLoggerComponents;

using System.Diagnostics.CodeAnalysis;

/// <summary>Handles logging for <see cref="IRecorder"/>.</summary>
public interface IRecorderLogger
{
    /// <summary>Handles logging related to type arguments.</summary>
    public abstract ITypeArgumentLogger TypeArgument { get; }

    /// <summary>Handles logging related to constructor arguments.</summary>
    public abstract IConstructorArgumentLogger ConstructorArgument { get; }

    /// <summary>Handles logging related to named arguments.</summary>
    public abstract INamedArgumentLogger NamedArgument { get; }
}

/// <summary>Handles logging for <see cref="IRecorder"/>.</summary>
/// <typeparam name="TCategoryName">The name of the logging category.</typeparam>
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed", Justification = "Follows the pattern of ILogger<CategoryName>")]
public interface IRecorderLogger<out TCategoryName> : IRecorderLogger { }
