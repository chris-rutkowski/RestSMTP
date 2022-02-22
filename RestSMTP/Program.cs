using RestSMTP;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<Settings>()
    .Bind(builder.Configuration.GetSection("Settings"))
    .Validate(x => !string.IsNullOrWhiteSpace(x.Username), "Missing username")
    .Validate(x => !string.IsNullOrWhiteSpace(x.Password), "Missing password")
    .Validate(x => !string.IsNullOrWhiteSpace(x.Host), "Missing host")
    .Validate(x => x.Port != -1, "Missing port")
    .ValidateOnStart();

builder.Services.AddSingleton<Service>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
