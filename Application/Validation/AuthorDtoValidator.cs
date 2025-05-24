using Application.Dtos;
using FluentValidation;

namespace Application.Validation
{
    /// <summary>
    /// Validator for AuthorDto using FluentValidation.
    /// </summary>
    public class AuthorDtoValidator : AbstractValidator<AuthorDto>
    {
        public AuthorDtoValidator() 
        { 
        RuleFor(a => a.FirstName).NotEmpty().WithMessage("Fra validator: Fornavn er påkrævet");
        RuleFor(a => a.LastName).NotEmpty().WithMessage("Fra validator: Efternavn er påkrævet");

        }
    }
}
