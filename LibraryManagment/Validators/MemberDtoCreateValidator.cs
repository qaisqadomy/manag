using Application.DTOs;
using FluentValidation;

namespace LibraryManagment.Validators;

public class MemberDtoCreateValidator : AbstractValidator<MemberDtoCreate>
{
    public MemberDtoCreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
    }
}
