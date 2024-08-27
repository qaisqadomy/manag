using Application.DTOs;
using FluentValidation;

namespace LibraryManagment.Validators;

public class MemberDtoValidator : AbstractValidator<MemberDTO>
{
    public MemberDtoValidator()
    {
        RuleFor(x => x.Id)
        .GreaterThan(0).WithMessage("ID must be a positive number.");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");

    }

}
