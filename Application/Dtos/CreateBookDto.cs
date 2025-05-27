using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class CreateBookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        [Required]
        [Range(1000, 9999, ErrorMessage = "Årstallet skal indeholde 4 cifre.")]
        public int PublishYear { get; set; }
        public double BasePrice { get; set; }
        public int AuthorId { get; set; }

    }
}
