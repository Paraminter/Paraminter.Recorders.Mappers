namespace SharpAttributeParser.Mappers.MappedRecorders.MappedConstructorRecorders;

using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Records the normal arguments of some constructor parameter.</summary>
public interface IMappedNormalConstructorRecorder
{
    /// <summary>Attempts to record an argument of some constructor parameter.</summary>
    /// <param name="argument">The argument of the constructor parameter.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    /// <returns>A <see cref="bool"/> indicating whether the argument was successfully recorded.</returns>
    public abstract bool TryRecordArgument(object? argument, ExpressionSyntax syntax);
}
