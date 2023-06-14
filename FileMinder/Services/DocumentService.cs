using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileMinder.Models;
using FileMinder.Repositories;

namespace FileMinder.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<IEnumerable<Document>> GetDocumentsAsync()
        {
            return await _documentRepository.GetDocumentsAsync();
        }

        public async Task<Document> GetDocumentByIdAsync(Guid id)
        {
            return await _documentRepository.GetDocumentByIdAsync(id);
        }

        public async Task<Document> CreateDocumentAsync(Document document)
        {
            document.Id = Guid.NewGuid();
            document.CreatedAt = DateTime.UtcNow;
            document.UpdatedAt = DateTime.UtcNow;
            return await _documentRepository.CreateDocumentAsync(document);
        }
        
        public async Task<Document> UpdateDocumentAsync(Document document)
        {
            var existingDocument = await _documentRepository.GetDocumentByIdAsync(document.Id);
            if(existingDocument == null)
            {
                return null;
            }
            document.UpdatedAt = DateTime.UtcNow;
            return await _documentRepository.UpdateDocumentAsync(document);
        }

        public async Task<Document> DeleteDocumentAsync(Guid id)
        {
            var document = await _documentRepository.GetDocumentByIdAsync(id);
            if(document == null)
            {
                return null;
            }
            return await _documentRepository.DeleteDocumentAsync(id);
        }
    }
}
