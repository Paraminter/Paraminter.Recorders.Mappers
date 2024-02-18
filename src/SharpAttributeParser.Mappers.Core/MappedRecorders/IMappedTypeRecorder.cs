namespace SharpAttributeParser.Mappers.MappedRecorders;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Records the arguments of some type parameter, together with syntactic information about the argument.</summary>
/// <typeparam name="TRecord">The type of the data record to which arguments are recorded.</typeparam>
public interface IMappedTypeRecorder<in TRecord>
{
    /// <summary>Attempts to record an argument of some type parameter.</summary>
    /// <param name="dataRecord">The data record to which arguments are recorded.</param>
    /// <param name="argument">The argument of the type parameter.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    /// <returns>A <see cref="bool"/> indicating whether the argument was successfully recorded.</returns>
    public abstract bool TryRecordArgument(TRecord dataRecord, ITypeSymbol argument, ExpressionSyntax syntax);
}
