using FluentValidation;

using FullCart.Domain.ValueObjects;

namespace FullCart.Application.Group.Commands.UpdateGroup;
public class UpdateGroupCommandValidation : AbstractValidator<UpdateGroupCommand>
{
    public UpdateGroupCommandValidation()
    {
        RuleFor(v => v).NotNull().WithMessage(Localization.BAD_REQUEST);
        RuleFor(v => v.Name).Must(a => !string.IsNullOrWhiteSpace(a) && a.Length <= 100).WithMessage("أدخل اسم المجموعة بما لا يتجاوز 100 حرف");
        RuleFor(v => v.Description).Must(a => !string.IsNullOrWhiteSpace(a) && a.Length <= 200).WithMessage("أدخل وصف المجموعة بما لا يتجاوز 200 حرف");
    }
}

