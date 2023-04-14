using FileEditor.Domain.Entities;
using FileEditor.Domain.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileEditor.Application.Documents;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;

    public DocumentService(IDocumentRepository documentRepository)
    {
        this._documentRepository = documentRepository;
    }

    public async Task<DocumentDTO> FindOrCreateDocumentAsync(string id)
    {
        var document = await _documentRepository.GetAsync(id);

        if (document is null)
        {
            document = new Document(id) { Data = string.Empty};
            await _documentRepository.CreateAsync(document);
        }

        return new DocumentDTO(document.Id) { Data = document.Data };
    }

    public async Task SaveDocumentAsync(DocumentDTO documentDTO)
    {
        try
        {
            var jsonString = JsonSerializer.Serialize(documentDTO.Data);

            var document = new Document(documentDTO.Id) { Data = jsonString };

            await _documentRepository.UpdateAsync(document);
        }
        catch (Exception ex)
        {
            var test = ex;
        }
    }
}