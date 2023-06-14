using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileMinder.Models;
using FileMinder.Services;

namespace FileMinder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetAllDocuments()
        {
            var documents = await _documentService.GetDocumentsAsync();
            return Ok(documents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocumentById(Guid id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);
        }

        [HttpPost]
        public async Task<ActionResult<Document>> CreateDocument(Document document)
        {
            var createdDocument = await _documentService.CreateDocumentAsync(document);
            return CreatedAtAction(nameof(GetDocumentById), new { id = createdDocument.Id }, createdDocument);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(Guid id, Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }
            var updatedDocument = await _documentService.UpdateDocumentAsync(document);
            if (updatedDocument == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteDocument(Guid id)
        {
            var deletedDocument = await _documentService.DeleteDocumentAsync(id);
            if (deletedDocument == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}