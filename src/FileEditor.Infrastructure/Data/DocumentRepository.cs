using FileEditor.Domain.Entities;
using FileEditor.Domain.Interfaces;
using MongoDB.Driver;

namespace FileEditor.Infrastructure.Data;

public class DocumentRepository : IDocumentRepository
{
    public readonly IMongoCollection<Document> _documents;

    public DocumentRepository(IMongoClient mongoClient, string databaseName)
    {
        var database = mongoClient.GetDatabase(databaseName);
        _documents = database.GetCollection<Document>("Documents");
    }

    public async Task CreateAsync(Document document)
    {
        await _documents.InsertOneAsync(document);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<Document>.Filter.Eq(u => u.Id, id);
        await _documents.DeleteOneAsync(filter);
    }

    public async Task<Document> GetAsync(string id)
    {
        var filter = Builders<Document>.Filter.Eq(u => u.Id, id);
        return await _documents.Find(filter).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Document document)
    {
        var filter = Builders<Document>.Filter.Eq(u => u.Id, document.Id);
        await _documents.ReplaceOneAsync(filter, document);
    }
}