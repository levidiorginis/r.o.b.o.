using FluentValidation;

namespace Robo.Domain.Entities.Members.Elbows;

public class ElbowValidator : AbstractValidator<Elbow>
{
    public ElbowValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("A entidade não pode ser nula")

            .NotEmpty()
            .WithMessage("A entidade não pode ser vazia");

        RuleFor(x => x.State)
            .NotNull()
            .WithMessage("O estado do cotovelo não pode ser nulo")

            .IsInEnum()
            .WithMessage("Estado inválido! Sistema do R.O.B.O está corrompido!");
    }
}