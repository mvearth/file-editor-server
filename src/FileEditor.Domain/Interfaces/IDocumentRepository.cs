using FileEditor.Domain.Entities;

namespace FileEditor.Domain.Interfaces;

public interface IDocumentRepository
{
    Task<Document> GetAsync(string id);
    Task CreateAsync(Document document);
    Task UpdateAsync(Document document);
    Task DeleteAsync(string id);
}