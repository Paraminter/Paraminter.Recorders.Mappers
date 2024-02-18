namespace SharpAttributeParser.Mappers.SyntacticMappedRecorders;

using Microsoft.CodeAnalysis.CSharp.Syntax;

/// <summary>Records syntactic information about the arguments of some named parameter.</summary>
/// <typeparam name="TRecord">The type of the data record to which syntactic information is recorded are recorded.</typeparam>
public interface ISyntacticMappedNamedRecorder<in TRecord>
{
    /// <summary>Attempts to record syntactic information about an argument of some named parameter.</summary>
    /// <param name="dataRecord">The data record to which syntactic information is recorded.</param>
    /// <param name="syntax">The syntactic description of the argument.</param>
    /// <returns>A <see cref="bool"/> indicating whether syntactic information was successfully recorded.</returns>
    public abstract bool TryRecordArgument(TRecord dataRecord, ExpressionSyntax syntax);
}
