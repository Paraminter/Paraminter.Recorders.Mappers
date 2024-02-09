namespace SharpAttributeParser.Mappers.RecorderFactoryCases.SyntacticRecorderCases;

using Xunit;

public sealed class BuildRecord
{
    private static TRecord Target<TRecord>(ISyntacticRecorder<TRecord> recorder) => recorder.BuildRecord();

    private readonly RecorderContext<object> Context = RecorderContext<object>.Create();

    [Fact]
    public void ReturnsRecord()
    {
        var actual = Target(Context.Recorder);

        Assert.Same(Context.DataRecordMock.Object, actual);
    }
}
