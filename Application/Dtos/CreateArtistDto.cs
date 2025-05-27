using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class CreateArtistDto
    {

        [Required(ErrorMessage = "Fra Dataannotation: Fornavn skal udfyldes.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Fra Dataannotation: Efternavn skal udfyldes.")]
        public string LastName { get; set; } = string.Empty;
    }
}
