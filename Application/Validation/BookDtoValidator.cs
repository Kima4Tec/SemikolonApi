using Application.Dtos;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Application.Validation
{
    /// <summary>
    /// Imperativ validering/manuel validering for BookDto
    /// </summary>
    public class BookDtoValidator
    {
        public void ValidateBookDto(BookDto bookdto)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(bookdto.Title))
                throw new ValidationException("Title is required.");

            if (bookdto.PublishYear < 1500 || bookdto.PublishYear > DateTime.Now.Year)
                throw new ValidationException("PublishYear must be between 1500 and current year.");

            if (bookdto.BasePrice < 0)
                throw new ValidationException("BasePrice cannot be negative.");

            if (bookdto.AuthorId == 0)
            {
                if (string.IsNullOrWhiteSpace(bookdto.AuthorFirstName) || string.IsNullOrWhiteSpace(bookdto.AuthorLastName))
                    throw new ValidationException("Author name must be provided if AuthorId is missing.");
            }
        }
    }
}
