using FileEditor.Domain.Interfaces;
using FileEditor.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace FileEditor.Infrastructure.Modules;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoConnectionString = configuration.GetValue<string>("MongoDB:ConnectionString") ?? throw new ArgumentNullException("MongoDB:ConnectionString");

        var mongoUrl = new MongoUrl(mongoConnectionString);
        var mongoClientSettings = MongoClientSettings.FromUrl(mongoUrl);

        services.AddSingleton(new MongoClient(mongoClientSettings));
        services.AddScoped<IDocumentRepository, DocumentRepository>(provider =>
        {
            var mongoClient = provider.GetRequiredService<MongoClient>();
            return new DocumentRepository(mongoClient, mongoUrl.DatabaseName);
        });

        return services;
    }
}