using Application.Dtos;
using FluentValidation;

namespace Application.Validation
{
    /// <summary>
    /// Validator not used since I am using dataannotations. Look at Dto.
    /// </summary>
    public class ArtistDtoValidator : AbstractValidator<CreateAuthorDto>
    {
        public ArtistDtoValidator() 
        { 

        }
    }
}
