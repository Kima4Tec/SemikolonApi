using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;

        public int PublishDate { get; set; }
        public double Price { get; set; }
        public Cover? Cover { get; set; }
        public int? AuthorId { get; set; }
    }
}
