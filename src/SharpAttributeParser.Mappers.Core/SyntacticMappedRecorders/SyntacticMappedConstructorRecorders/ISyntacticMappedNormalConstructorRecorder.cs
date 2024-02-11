namespace SharpAttributeParser.Mappers.SyntacticMappedRecorders.SyntacticMappedConstructorRecorders;

using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Records syntactic information about the normal arguments of some constructor parameter.</summary>
public interface ISyntacticMappedNormalConstructorRecorder
{
    /// <summary>Attempts to record syntactic information about an argument of some constructor parameter.</summary>
    /// <param name="syntax">The syntactic description of the argument.</param>
    /// <returns>A <see cref="bool"/> indicating whether syntactic information was successfully recorded.</returns>
    public abstract bool TryRecordArgument(ExpressionSyntax syntax);
}
