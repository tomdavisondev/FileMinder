using Castle.Components.DictionaryAdapter;
using System;
using System.ComponentModel.DataAnnotations;

namespace FileMinder.Models
{
    public class Document
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid Id { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string DocId { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        [MaxLength(255)]
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [MaxLength(255)]
        public string FileName { get; set; }

        public byte[] FileData { get; set; }
    }
}
