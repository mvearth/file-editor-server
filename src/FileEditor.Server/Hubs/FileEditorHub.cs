using FileEditor.Application.Documents;
using Microsoft.AspNetCore.SignalR;

namespace FileEditor.Server.Hubs;

public class FileEditorHub : Hub
{
    private readonly IDocumentService _documentService;

    public FileEditorHub(IDocumentService documentService)
    {
        this._documentService = documentService;
    }

    public async Task StartEditingFile(string documentId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, documentId);

        var documentDTO = await _documentService.FindOrCreateDocumentAsync(documentId);

        await Clients.Caller.SendAsync("LoadFile", documentDTO);
    }

    public async Task SendChanges(DocumentDTO documentDTO)
    {
        await Clients.GroupExcept(documentDTO.Id, Context.ConnectionId).SendAsync("ReceiveChanges", documentDTO.Data);
    }

    public async Task SaveFile(DocumentDTO documentDTO)
    {
        await _documentService.SaveDocumentAsync(documentDTO);
    }
}