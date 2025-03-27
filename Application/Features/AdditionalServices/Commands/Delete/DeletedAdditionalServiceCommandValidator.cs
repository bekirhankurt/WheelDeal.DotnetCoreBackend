using FluentValidation;

namespace Application.Features.AdditionalServices.Commands.Delete;

public class DeletedAdditionalServiceCommandValidator : AbstractValidator<DeleteAdditionalServiceCommand>
{
    public DeletedAdditionalServiceCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}