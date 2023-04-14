using FileEditor.Domain.Entities;

namespace FileEditor.Application.Documents;

public interface IDocumentService
{
    Task<DocumentDTO> FindOrCreateDocumentAsync(string id);
    Task SaveDocumentAsync(DocumentDTO document);
}