using FluentValidation;

using FullCart.Domain.ValueObjects;

namespace FullCart.Application.Group.Commands.CreateGroup;
public class CreateGroupCommandValidation : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidation()
    {
        RuleFor(v => v).NotNull().WithMessage(Localization.BAD_REQUEST);
        RuleFor(v => v.Name).Must(a => !string.IsNullOrWhiteSpace(a) && a.Length <= 100).WithMessage("أدخل اسم المجموعة بما لا يتجاوز 100 حرف");
        RuleFor(v => v.Description).Must(a => !string.IsNullOrWhiteSpace(a) && a.Length <= 200).WithMessage("أدخل وصف المجموعة بما لا يتجاوز 200 حرف");
    }
}

