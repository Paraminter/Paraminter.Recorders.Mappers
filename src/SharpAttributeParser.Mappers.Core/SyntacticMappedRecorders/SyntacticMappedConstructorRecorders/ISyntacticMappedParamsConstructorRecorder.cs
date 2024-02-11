namespace SharpAttributeParser.Mappers.SyntacticMappedRecorders.SyntacticMappedConstructorRecorders;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;

/// <summary>Records syntactic information about the <see langword="params"/>-arguments of some constructor parameter.</summary>
public interface ISyntacticMappedParamsConstructorRecorder
{
    /// <summary>Attempts to record syntactic information about a <see langword="params"/>-argument of a constructor parameter.</summary>
    /// <param name="elementSyntax">The syntactic description of each element in the <see langword="params"/>-argument.</param>
    /// <returns>A <see cref="bool"/> indicating whether syntactic information was successfully recorded.</returns>
    public abstract bool TryRecordArgument(IReadOnlyList<ExpressionSyntax> elementSyntax);
}
