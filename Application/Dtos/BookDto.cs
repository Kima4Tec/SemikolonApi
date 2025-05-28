using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
public class BookDto
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    
    // Her bruger vi PublishYear som int (fra DateOnly.Year)
    public int PublishDate { get; set; }

    public double Price { get; set; }

    // Forfatter
    public int? AuthorId { get; set; }
    public string AuthorFirstName { get; set; } = string.Empty;
    public string AuthorLastName { get; set; } = string.Empty;

        // Omslag
        public string DesignIdeas { get; set; } = string.Empty;
        public bool CoverIsDigital { get; set; }

    // Liste over omslagskunstnere
    public List<ArtistDto>? CoverArtists { get; set; } = new List<ArtistDto>();
}
}
