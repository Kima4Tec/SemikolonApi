using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    /// <summary>
    /// Data Transfer Object for Author to/from client
    /// </summary>
    public class AuthorDto
    {
        /// <summary>
        /// Gets or sets the ID for the author.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the author.
        /// </summary>
        //[Required(ErrorMessage = "Firstname is required.")]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the last name of the author.
        /// </summary>
        //[Required]
        public string LastName { get; set; } = null!;
    }
}
