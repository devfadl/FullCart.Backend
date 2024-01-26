using FluentValidation;

using FullCart.Application.User.Commands.UpdateUser;
using FullCart.Domain.ValueObjects;

namespace FullCart.Application.User.Commands.CreateUser;
public class UpdateUserCommandValidation : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidation()
    {
        RuleFor(v => v).NotNull().WithMessage(Localization.BAD_REQUEST);
        RuleFor(v => v.FirstName).Must(a => !string.IsNullOrWhiteSpace(a) && a.Length <= 30).WithMessage("أدخل اسم الموظف بما لا يتجاوز 30 حرف");
        RuleFor(v => v.SecondName).Must(a => !string.IsNullOrWhiteSpace(a) && a.Length <= 30).WithMessage("أدخل اسم الأب بما لا يتجاوز 30 حرف");
        RuleFor(v => v.ThirdName).Must(a => !string.IsNullOrWhiteSpace(a) && a.Length <= 30).WithMessage("أدخل اسم الجد بما لا يتجاوز 30 حرف");
        RuleFor(v => v.LastName).Must(a => !string.IsNullOrWhiteSpace(a) && a.Length <= 30).WithMessage("أدخل اسم العائلة بما لا يتجاوز 30 حرف");
        RuleFor(v => v.PhoneNumber).Must(a => !string.IsNullOrWhiteSpace(a) && a.Length <= 10).WithMessage("أدخل رقم الموظف المكون من 10 خانات");
        RuleFor(v => v.Email).EmailAddress().NotEmpty().WithMessage("ادخل ايميل الموظف بالصيغة الصحيحة");
    }
}

