using Application.Dtos;
using FluentValidation;

namespace Application.Validation
{
    /// <summary>
    /// Validator for AuthorDto using FluentValidation.
    /// </summary>
    public class AuthorDtoValidator : AbstractValidator<CreateAuthorDto>
    {
        public AuthorDtoValidator() 
        { 
        RuleFor(a => a.FirstName)
                .NotEmpty().WithMessage("Fra validator: Fornavn må ikke være tom. ")
                .MinimumLength(2).WithMessage("Fornavn på mindst to bogstaver er påkrævet");
            RuleFor(a => a.LastName)
                    .NotEmpty().WithMessage("Fra validator: Efternavn er påkrævet");


        }
    }
}
