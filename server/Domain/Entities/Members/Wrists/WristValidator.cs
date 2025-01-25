using FluentValidation;

namespace Robo.Domain.Entities.Members.Wrists;

public class WristValidator : AbstractValidator<Wrist>
{
    public WristValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("A entidade não pode ser nula")

            .NotEmpty()
            .WithMessage("A entidade não pode ser vazia");

        RuleFor(x => x.Rotation)
            .NotNull()
            .WithMessage("A rotação do pulso não pode ser nula")

            .IsInEnum()
            .WithMessage("Rotação inválida! Sistema do R.O.B.O está corrompido!");
    }
}