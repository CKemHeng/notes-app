using System;
using System.ComponentModel.DataAnnotations;

namespace Note.Models
{
    public class NoteModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime? updatedAt { get; set; }
    }
}
