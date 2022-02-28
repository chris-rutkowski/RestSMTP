using InfluxDB.Client;
using InfluxDB.Client.Writes;
using Microsoft.Extensions.Options;

namespace RestSMTP.Measurement.Influx
{
    public class Influx2MeasurementService : IMeasurementService
    {
        private readonly ILogger<Influx2MeasurementService> _logger;
        private readonly Influx2Settings _settings;
        private readonly WriteApiAsync _writeApi;

        public Influx2MeasurementService(
            ILogger<Influx2MeasurementService> logger,
            IOptions<Influx2Settings> settings)
        {
            _settings = settings.Value;
            _writeApi = InfluxDBClientFactory.Create(_settings.Url, _settings.Token).GetWriteApiAsync();
            _logger = logger;

            logger.LogInformation("Using Influx2MeasurementService");
        }

        public async Task CountEmailResult(EmailResultType type)
        {
            var point = PointData
                .Measurement("EmailResult")
                .Tag("type", type.ToString())
                .Field("count", 1);

            try
            {
                await _writeApi.WritePointAsync(_settings.Bucket, _settings.Organization, point);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Count");
            }
        }
    }
}
