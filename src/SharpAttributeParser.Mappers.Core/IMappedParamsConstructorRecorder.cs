namespace SharpAttributeParser.Mappers;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;

/// <summary>Records the <see langword="params"/>-arguments of some constructor parameter.</summary>
public interface IMappedParamsConstructorRecorder
{
    /// <summary>Attempts to record a <see langword="params"/>-argument of some constructor parameter.</summary>
    /// <param name="argument">The argument of the constructor parameter.</param>
    /// <param name="elementSyntax">The syntactic description of each element in the <see langword="params"/>-argument.</param>
    /// <returns>A <see cref="bool"/> indicating whether the argument was successfully recorded.</returns>
    public abstract bool TryRecordArgument(object? argument, IReadOnlyList<ExpressionSyntax> elementSyntax);
}
