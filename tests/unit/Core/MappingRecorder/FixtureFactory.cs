namespace Paraminter.Recorders.Mappers;

using Moq;

using Paraminter.Arguments.Models;
using Paraminter.Cqs.Handlers;
using Paraminter.Parameters.Models;
using Paraminter.Recorders.Commands;
using Paraminter.Recorders.Mappers.Commands;
using Paraminter.Recorders.Mappers.Queries;

internal static class FixtureFactory
{
    public static IFixture<TParameter, TArgumentData> Create<TParameter, TArgumentData>()
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        Mock<IQueryHandler<IGetMappedArgumentAssociationRecorderQuery<TParameter>, ICommandHandler<IRecordMappedArgumentAssociationCommand<TArgumentData>>>> mapperMock = new();

        MappingRecorder<TParameter, TArgumentData> sut = new(mapperMock.Object);

        return new Fixture<TParameter, TArgumentData>(sut, mapperMock);
    }

    private sealed class Fixture<TParameter, TArgumentData>
        : IFixture<TParameter, TArgumentData>
        where TParameter : IParameter
        where TArgumentData : IArgumentData
    {
        private readonly ICommandHandler<IRecordArgumentAssociationCommand<TParameter, TArgumentData>> Sut;

        private readonly Mock<IQueryHandler<IGetMappedArgumentAssociationRecorderQuery<TParameter>, ICommandHandler<IRecordMappedArgumentAssociationCommand<TArgumentData>>>> MapperMock;

        public Fixture(
            ICommandHandler<IRecordArgumentAssociationCommand<TParameter, TArgumentData>> sut,
            Mock<IQueryHandler<IGetMappedArgumentAssociationRecorderQuery<TParameter>, ICommandHandler<IRecordMappedArgumentAssociationCommand<TArgumentData>>>> mapperMock)
        {
            Sut = sut;

            MapperMock = mapperMock;
        }

        ICommandHandler<IRecordArgumentAssociationCommand<TParameter, TArgumentData>> IFixture<TParameter, TArgumentData>.Sut => Sut;

        Mock<IQueryHandler<IGetMappedArgumentAssociationRecorderQuery<TParameter>, ICommandHandler<IRecordMappedArgumentAssociationCommand<TArgumentData>>>> IFixture<TParameter, TArgumentData>.MapperMock => MapperMock;
    }
}
