namespace SharpAttributeParser.Mappers.MappedRecorders;

using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Records the arguments of some named parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which arguments are recorded.</typeparam>
public interface IMappedNamedRecorder<in TRecord>
{
    /// <summary>Attempts to record an argument of some named parameter.</summary>
    /// <param name="dataRecord">The data record to which arguments are recorded.</param>
    /// <param name="argument">The argument of the named parameter.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    /// <returns>A <see cref="bool"/> indicating whether the argument was successfully recorded.</returns>
    public abstract bool TryRecordArgument(TRecord dataRecord, object? argument, ExpressionSyntax syntax);
}
