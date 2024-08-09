namespace Paraminter.Recorders.Mappers.Commands;

using Paraminter.Arguments.Models;

internal static class RecordMappedArgumentAssociationCommandFactory
{
    public static IRecordMappedArgumentAssociationCommand<TArgumentData> Create<TArgumentData>(
        TArgumentData argumentData)
        where TArgumentData : IArgumentData
    {
        return new RecordMappedArgumentAssociationCommand<TArgumentData>(argumentData);
    }

    private sealed class RecordMappedArgumentAssociationCommand<TArgumentData>
        : IRecordMappedArgumentAssociationCommand<TArgumentData>
        where TArgumentData : IArgumentData
    {
        private readonly TArgumentData ArgumentData;

        public RecordMappedArgumentAssociationCommand(
            TArgumentData argumentData)
        {
            ArgumentData = argumentData;
        }

        TArgumentData IRecordMappedArgumentAssociationCommand<TArgumentData>.ArgumentData => ArgumentData;
    }
}
