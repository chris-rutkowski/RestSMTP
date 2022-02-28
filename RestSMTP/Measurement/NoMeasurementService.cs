namespace RestSMTP.Measurement
{
    public class NoMeasurementService : IMeasurementService
    {
        public Task CountEmailResult(EmailResultType type)
        {
            return Task.CompletedTask;
        }
    }
}
