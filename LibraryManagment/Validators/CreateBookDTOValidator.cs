using Application.DTOs;
using FluentValidation;

public class CreateBookDTOValidator : AbstractValidator<BookDtoUpdate>
{
    public CreateBookDTOValidator()
    {
        RuleFor(book => book.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(1, 100).WithMessage("Title must be between 1 and 100 characters.");

        RuleFor(book => book.Author)
            .NotEmpty().WithMessage("Author is required.")
            .Length(1, 100).WithMessage("Author must be between 1 and 100 characters.");

        RuleFor(book => book.BorrowedDate)
            .Must(BeAValidDate).WithMessage("BorrowedDate must be a valid date.");

        RuleFor(book => book.BorrowedBy)
            .GreaterThan(0).When(book => book.BorrowedBy.HasValue).WithMessage("BorrowedBy must be greater than 0 if provided.");
    }

    private bool BeAValidDate(DateTime? date)
    {
        return !date.HasValue || date.Value <= DateTime.UtcNow;
    }
}