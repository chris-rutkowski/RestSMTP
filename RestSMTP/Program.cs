using RestSMTP;

var builder = WebApplication.CreateBuilder(args);

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
app.Run();
