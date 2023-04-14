using Microsoft.Extensions.DependencyInjection;

namespace FileEditor.Application.Documents;

public static class DocumentServiceModule
{
    public static IServiceCollection AddDocumentService(this IServiceCollection services)
    {
        services.AddScoped<IDocumentService, DocumentService>();

        return services;
    }
}