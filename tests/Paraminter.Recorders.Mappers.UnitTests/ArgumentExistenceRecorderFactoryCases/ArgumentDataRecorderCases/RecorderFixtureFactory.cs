﻿namespace Paraminter.Recorders.Mappers.ArgumentExistenceRecorderFactoryCases.ArgumentExistenceRecorderCases;

using Moq;

internal static class RecorderFixtureFactory
{
    public static IRecorderFixture<TParameter, TRecord> Create<TParameter, TRecord>()
        where TRecord : class
    {
        IArgumentExistenceRecorderFactory factory = new ArgumentExistenceRecorderFactory();

        Mock<IArgumentExistenceRecorderMapper<TParameter, TRecord>> mapperMock = new() { DefaultValue = DefaultValue.Mock };
        Mock<TRecord> dataRecordMock = new() { DefaultValue = DefaultValue.Mock };

        var sut = factory.Create(mapperMock.Object, dataRecordMock.Object);

        return new RecorderFixture<TParameter, TRecord>(sut, mapperMock, dataRecordMock);
    }

    private sealed class RecorderFixture<TParameter, TRecord>
        : IRecorderFixture<TParameter, TRecord>
        where TRecord : class
    {
        private readonly IArgumentExistenceRecorder<TParameter> Sut;

        private readonly Mock<IArgumentExistenceRecorderMapper<TParameter, TRecord>> MapperMock;
        private readonly Mock<TRecord> DataRecordMock;

        public RecorderFixture(
            IArgumentExistenceRecorder<TParameter> sut,
            Mock<IArgumentExistenceRecorderMapper<TParameter, TRecord>> mapperMock,
            Mock<TRecord> dataRecordMock)
        {
            Sut = sut;

            MapperMock = mapperMock;
            DataRecordMock = dataRecordMock;
        }

        IArgumentExistenceRecorder<TParameter> IRecorderFixture<TParameter, TRecord>.Sut => Sut;

        Mock<IArgumentExistenceRecorderMapper<TParameter, TRecord>> IRecorderFixture<TParameter, TRecord>.MapperMock => MapperMock;
        Mock<TRecord> IRecorderFixture<TParameter, TRecord>.DataRecordMock => DataRecordMock;
    }
}
