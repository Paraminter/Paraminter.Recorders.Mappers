﻿namespace Paraminter.Mappers.ArgumentDataRecorderFactoryCases.ArgumentDataRecorderCases;

using Moq;

internal interface IRecorderFixture<TParameter, TRecord, TArgumentData>
    where TRecord : class
{
    public abstract IArgumentDataRecorder<TParameter, TArgumentData> Sut { get; }

    public abstract Mock<IParameterMapper<TParameter, TRecord, TArgumentData>> MapperMock { get; }
    public abstract Mock<TRecord> DataRecordMock { get; }
}
