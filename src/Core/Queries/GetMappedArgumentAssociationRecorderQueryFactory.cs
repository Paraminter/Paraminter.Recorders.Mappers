namespace Paraminter.Recorders.Mappers.Queries;

using Paraminter.Parameters.Models;

internal static class GetMappedArgumentAssociationRecorderQueryFactory
{
    public static IGetMappedArgumentAssociationRecorderQuery<TParameter> Create<TParameter>(
        TParameter parameter)
        where TParameter : IParameter
    {
        return new GetMappedArgumentAssociationRecorderQuery<TParameter>(parameter);
    }

    private sealed class GetMappedArgumentAssociationRecorderQuery<TParameter>
        : IGetMappedArgumentAssociationRecorderQuery<TParameter>
        where TParameter : IParameter
    {
        private readonly TParameter Parameter;

        public GetMappedArgumentAssociationRecorderQuery(
            TParameter parameter)
        {
            Parameter = parameter;
        }

        TParameter IGetMappedArgumentAssociationRecorderQuery<TParameter>.Parameter => Parameter;
    }
}
