namespace RestSMTP.Measurement
{
    public interface IMeasurementService
    {
        public Task CountEmailResult(EmailResultType type);
    }
}
