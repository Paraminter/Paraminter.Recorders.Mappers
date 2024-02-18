namespace SharpAttributeParser.Mappers.MappedRecorders.MappedConstructorRecorders;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Generic;

/// <summary>Records the <see langword="params"/>-arguments of some constructor parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which arguments are recorded.</typeparam>
public interface IMappedParamsConstructorRecorder<in TRecord>
{
    /// <summary>Attempts to record a <see langword="params"/>-argument of some constructor parameter.</summary>
    /// <param name="dataRecord">The data record to which arguments are recorded.</param>
    /// <param name="argument">The argument of the constructor parameter.</param>
    /// <param name="elementSyntax">The syntactic description of each element in the <see langword="params"/>-argument.</param>
    /// <returns>A <see cref="bool"/> indicating whether the argument was successfully recorded.</returns>
    public abstract bool TryRecordArgument(TRecord dataRecord, object? argument, IReadOnlyList<ExpressionSyntax> elementSyntax);
}
