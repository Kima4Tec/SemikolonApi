using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Validation for PublishYear using dataannotation
        /// </summary>
        [Required]
        [Range(1000, 9999, ErrorMessage = "Year must be a 4-digit number.")]
        public int PublishYear { get; set; }
        public double BasePrice { get; set; }
        public int? AuthorId { get; set; }
        public string AuthorFirstName { get; set; } = string.Empty;
        public string AuthorLastName { get; set; } = string.Empty;
    }
}
