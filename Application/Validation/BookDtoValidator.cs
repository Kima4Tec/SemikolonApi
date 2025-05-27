using Application.Dtos;

public class BookDtoValidator
{
    public bool ValidateBookDto(CreateBookDto bookDto, out List<string> errors)
    {
        errors = new List<string>();

        if (string.IsNullOrWhiteSpace(bookDto.Title))
            errors.Add("Title is required.");

        if (bookDto.PublishYear < 1500 || bookDto.PublishYear > DateTime.Now.Year)
            errors.Add("PublishYear must be between 1500 and current year.");

        if (bookDto.BasePrice < 0)
            errors.Add("BasePrice cannot be negative.");

        return errors.Count == 0;
    }
}
