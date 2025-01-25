using FluentValidation;
using Robo.Domain.Entities.Members.Enums;

namespace Robo.Domain.Entities.Members.Heads;

public class HeadValidator : AbstractValidator<Head>
{
    public HeadValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("A entidade não pode ser nula")

            .NotEmpty()
            .WithMessage("A entidade não pode ser vazia");

        RuleFor(x => x.Rotation)
            .NotNull()
            .WithMessage("A rotação da cabeça não pode ser nula")

            .IsInEnum()
            .WithMessage("Rotação inválida! Sistema do R.O.B.O está corrompido!")

            .Must(rotation => rotation != MemberRotation.Positive135 && rotation != MemberRotation.Positive180)
            .WithMessage("A rotação da cabeça não pode ser maior que 90º.");

        RuleFor(x => x.Inclination)
            .NotNull()
            .WithMessage("A inclinação da cabeça não pode ser nula")

            .IsInEnum()
            .WithMessage("Inclinação inválida! Sistema do R.O.B.O está corrompido!");
    }
}