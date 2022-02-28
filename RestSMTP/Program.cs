using RestSMTP;
using RestSMTP.Measurement;
using RestSMTP.Measurement.Influx;

var builder = WebApplication.CreateBuilder(args);

var influx2Settings = builder.Configuration.GetSection("Influx2").Get<Influx2Settings>();
if (!string.IsNullOrEmpty(influx2Settings?.Url))
{
    builder.Services.AddOptions<Influx2Settings>()
        .Bind(builder.Configuration.GetSection("Influx2"))
        .Validate(x => !string.IsNullOrWhiteSpace(x.Token), "Missing Influx2:Token")
        .Validate(x => !string.IsNullOrWhiteSpace(x.Bucket), "Missing Influx2:Bucket")
        .Validate(x => !string.IsNullOrWhiteSpace(x.Organization), "Missing Influx2:Organization")
        .ValidateOnStart();

    builder.Services.AddSingleton<IMeasurementService, Influx2MeasurementService>();
}
else
{
    builder.Services.AddSingleton<IMeasurementService, NoMeasurementService>();
}

builder.Services.AddOptions<SMTPSettings>()
    .Bind(builder.Configuration.GetSection("SMTP"))
    .Validate(x => !string.IsNullOrWhiteSpace(x.Host), "Missing host")
    .Validate(x => x.Port > 0, "Missing port")
    .Validate(x => !string.IsNullOrWhiteSpace(x.Username), "Missing username")
    .Validate(x => !string.IsNullOrWhiteSpace(x.Password), "Missing password")
    .ValidateOnStart();

builder.Services.AddSingleton<Service>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseAuthorization();
app.MapControllers();

// warm up
app.Services.GetRequiredService<IMeasurementService>(); 

app.Run();
