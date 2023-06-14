using System.Collections.Generic;
using System.Threading.Tasks;
using FileMinder.Models;

namespace FileMinder.Repositories
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<Document>> GetDocumentsAsync();
        Task<Document> GetDocumentByIdAsync(Guid id);
        Task<Document> CreateDocumentAsync(Document document);
        Task<Document> UpdateDocumentAsync(Document document);
        Task<Document> DeleteDocumentAsync(Guid id);
    }
}
