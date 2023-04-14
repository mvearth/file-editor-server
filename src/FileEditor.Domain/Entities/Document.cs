namespace FileEditor.Domain.Entities;

public class Document
{
    public Document(string id)
    {
        Id = id;
    }

    public string Id { get; set; }

    public object? Data { get; set; }
}