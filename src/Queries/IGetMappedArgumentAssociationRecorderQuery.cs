namespace Paraminter.Recorders.Mappers.Queries;

using Paraminter.Cqs;

/// <summary>Represents a query for a recorder of associations between arguments and parameter.</summary>
/// <typeparam name="TParameter">The type representing the parameters.</typeparam>
public interface IGetMappedArgumentAssociationRecorderQuery<out TParameter>
    : IQuery
{
    /// <summary>The parameter.</summary>
    public abstract TParameter Parameter { get; }
}
