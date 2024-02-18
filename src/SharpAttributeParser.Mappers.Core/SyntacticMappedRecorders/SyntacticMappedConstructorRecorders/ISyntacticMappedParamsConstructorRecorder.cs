namespace SharpAttributeParser.Mappers.SyntacticMappedRecorders.SyntacticMappedConstructorRecorders;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;

/// <summary>Records syntactic information about the <see langword="params"/>-arguments of some constructor parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which syntactic information is recorded are recorded.</typeparam>
public interface ISyntacticMappedParamsConstructorRecorder<in TRecord>
{
    /// <summary>Attempts to record syntactic information about a <see langword="params"/>-argument of a constructor parameter.</summary>
    /// <param name="dataRecord">The data record to which syntactic information is recorded.</param>
    /// <param name="elementSyntax">The syntactic description of each element in the <see langword="params"/>-argument.</param>
    /// <returns>A <see cref="bool"/> indicating whether syntactic information was successfully recorded.</returns>
    public abstract bool TryRecordArgument(TRecord dataRecord, IReadOnlyList<ExpressionSyntax> elementSyntax);
}
