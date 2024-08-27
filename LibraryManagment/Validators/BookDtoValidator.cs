using System;
using Application.DTOs;
using FluentValidation;

namespace LibraryManagment.Validators;

public class BookDtoValidator : AbstractValidator<BookDTO>
{
    public BookDtoValidator()
    {

        RuleFor(x => x.Id)
       .GreaterThan(0).WithMessage("ID must be a positive number.");
       
        RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");

        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("Author is required.");

        RuleFor(x => x.BorrowedDate)
            .Must((dto, borrowedDate) => !dto.IsBorrowed || borrowedDate.HasValue)
            .WithMessage("BorrowedDate is required when the book is borrowed.");

        RuleFor(x => x.BorrowedBy)
            .Must((dto, borrowedBy) => !dto.IsBorrowed || borrowedBy.HasValue)
            .WithMessage("BorrowedBy is required when the book is borrowed.");

    }


}
