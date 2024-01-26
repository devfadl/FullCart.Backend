using FluentValidation;

using FullCart.Application.Group.Commands.CreateGroup;
using FullCart.Domain.ValueObjects;

namespace FullCart.Application.Group.Commands.DeleteGroup;
public class DeleteGroupCommandValidation : AbstractValidator<DeleteGroupCommand>
{
    public DeleteGroupCommandValidation()
    {
        RuleFor(v => v).NotNull().WithMessage(Localization.BAD_REQUEST);
    }
}

