using TranslationApp.Infrastructure;
using TranslationApp.Repositorys;
using TranslationApp.Services;

var builder = WebApplication.CreateBuilder(args);

const string credentialsPath = @"c:\\google\\googletranslatorapi-key.json";

try
{
    var credentialsContent = File.ReadAllText(credentialsPath);
    if (string.IsNullOrWhiteSpace(credentialsContent))
    {
        Console.WriteLine("ERROR: Credentials file is empty");
        return;
    }
    Console.WriteLine($"Successfully loaded credentials file from {credentialsPath}");
    
    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR reading credentials file: {ex.Message}");
    return;
}

// Add services to the container.
builder.Services.AddControllers();

//register swagger service
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register translation services
builder.Services.AddSingleton<ITranslationService, GoogleTranslationService>();
builder.Services.AddSingleton<ITranslationRepository, InMemoryTranslationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();