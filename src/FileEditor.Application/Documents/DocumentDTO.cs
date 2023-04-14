namespace FileEditor.Application.Documents;

public class DocumentDTO
{
    public DocumentDTO(string id)
    {
        Id = id;
    }

    public string Id { get; set; }

    public object? Data { get; set; }
}