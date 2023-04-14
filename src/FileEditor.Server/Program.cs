using FileEditor.Server.Hubs;
using FileEditor.Infrastructure.Modules;
using FileEditor.Application.Documents;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowCredentials();
        });
});

builder.Services.AddSignalR();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDocumentService();

var app = builder.Build();

app.UseCors();

app.MapHub<FileEditorHub>("/hub");

app.Run();