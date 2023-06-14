using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using FileMinder.Models;
using FileMinder.Services;
using FileMinder.Repositories;

namespace FileMinderTests
{
    public class Tests
    {
        private Mock<IDocumentRepository> mockRepository;
        private DocumentService documentService;
        private List<Document> originalDocuments;

        [SetUp]
        public void Setup()
        {
            //Arrange
            mockRepository = new Mock<IDocumentRepository>();
            documentService = new DocumentService(mockRepository.Object);

            originalDocuments = new List<Document>
                {
                    new Document { Id = new Guid(), Title = "Document 1" },
                    new Document { Id = new Guid(), Title = "Document 2" }
                };

            mockRepository.Setup(repo => repo.GetDocumentsAsync()).ReturnsAsync(originalDocuments);
        }

        [Test]
        public async Task ReturnsListOfDocuments()
        {
            //Act
            var result = await documentService.GetDocumentsAsync();

            //Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(originalDocuments.Count, result.Count());
        }
        [Test]
        public async Task CreateNewDoc()
        {
            var documentToCreate = new Document { Id = new Guid(), DocId = "TEST1", Title = "New Document" };
            var createdDocument = new Document { Id = Guid.NewGuid(), DocId = "TEST1", Title = "New Document" };

            mockRepository.Setup(repo => repo.CreateDocumentAsync(documentToCreate))
                .ReturnsAsync(createdDocument);

            var documentService = new DocumentService(mockRepository.Object);

            // Act
            var result = await documentService.CreateDocumentAsync(documentToCreate);

            // Assert
            Assert.AreEqual(createdDocument.Id, result.Id);
            Assert.AreEqual(createdDocument.Title, result.Title);
            Assert.IsNotNull(result.CreatedAt);
            Assert.IsNotNull(result.UpdatedAt);
        }
        [Test]
        public async Task UpdateExistingDoc()
        {
            var existingDocumentId = Guid.NewGuid();
            var updatedDocument = new Document { Id = existingDocumentId, DocId = "TEST1", Title = "Updated Document" };

            mockRepository.Setup(repo => repo.GetDocumentByIdAsync(existingDocumentId))
                .ReturnsAsync(new Document { Id = existingDocumentId, Title = "Old Document" });
            mockRepository.Setup(repo => repo.UpdateDocumentAsync(updatedDocument))
                .ReturnsAsync(updatedDocument);

            var documentService = new DocumentService(mockRepository.Object);

            // Act
            var result = await documentService.UpdateDocumentAsync(updatedDocument);

            // Assert
            Assert.AreEqual(updatedDocument.Id, result.Id);
            Assert.AreEqual(updatedDocument.Title, result.Title);
            Assert.IsNotNull(result.UpdatedAt);
        }
        [Test]
        public async Task DeletesExistingDocument()
        {
            // Arrange
            var existingDocumentId = Guid.NewGuid();
            var deletedDocument = new Document { Id = existingDocumentId, Title = "Deleted Document" };

            mockRepository.Setup(repo => repo.GetDocumentByIdAsync(existingDocumentId))
                .ReturnsAsync(deletedDocument);
            mockRepository.Setup(repo => repo.DeleteDocumentAsync(existingDocumentId))
                .ReturnsAsync(deletedDocument);

            var documentService = new DocumentService(mockRepository.Object);

            // Act
            var result = await documentService.DeleteDocumentAsync(existingDocumentId);

            // Assert
            Assert.AreEqual(deletedDocument.Id, result.Id);
            Assert.AreEqual(deletedDocument.Title, result.Title);
        }
    }
}