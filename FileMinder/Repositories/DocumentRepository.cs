using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileMinder.Models;
using FileMinder.Data;
using Microsoft.EntityFrameworkCore;

namespace FileMinder.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DocumentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Document>> GetDocumentsAsync()
        {
            return await _dbContext.Documents.ToListAsync();
        }
        public async Task<Document> GetDocumentByIdAsync(Guid id)
        {
            return await _dbContext.Documents.FindAsync(id);
        }
        public async Task<Document> CreateDocumentAsync(Document document)
        {
            _dbContext.Documents.Add(document);
            await _dbContext.SaveChangesAsync();
            return document;
        }
        public async Task<Document> UpdateDocumentAsync(Document document)
        {
            _dbContext.Entry(document).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return document;
        }
        public async Task<Document> DeleteDocumentAsync(Guid id)
        {
            var document = await _dbContext.Documents.FindAsync(id);
            if(document != null) 
            {
                _dbContext.Documents.Remove(document);
                await _dbContext.SaveChangesAsync();
            }
            return document;
        }
    }
}
