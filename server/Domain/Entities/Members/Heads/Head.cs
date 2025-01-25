using Robo.Core.Exceptions;
using Robo.Domain.Entities.Members.Enums;

namespace Robo.Domain.Entities.Members.Heads;

public class Head
{
    #region Properties
    public MemberRotation Rotation { get; private set; }
    public MemberInclination Inclination { get; private set; }
    #endregion

    #region CTOR
    public Head()
    {
        Rotation = MemberRotation.AtRest;
        Inclination = MemberInclination.AtRest;
        Validate();
    }
    #endregion

    #region Methods
    private void SetRotation(MemberRotation rotation)
    {
        Rotation = rotation;
        Validate();
    }

    private void SetInclination(MemberInclination inclination)
    {
        Inclination = inclination;
        Validate();
    }

    public void RotateNext()
    {
        if (Rotation == MemberRotation.Positive90)
            throw new DomainException("A cabeça já está no maior ângulo possível, não há como rotacionar mais.");

        CheckRotate();
        var newRotate = Rotation.GetHashCode() + 1;
        SetRotation((MemberRotation)newRotate);
    }

    public void RotatePrevious()
    {
        if (Rotation == MemberRotation.Negative90)
            throw new DomainException("A cabeça já está no menor ângulo possível, não há como rotacionar mais.");

        CheckRotate();
        var newRotate = Rotation.GetHashCode() - 1;
        SetRotation((MemberRotation)newRotate);
    }

    public void InclinateNext()
    {
        if (Inclination == MemberInclination.Downward)
            throw new DomainException("A cabeça já está o mais inclinada possível, não há como inclinar mais.");

        var newInclination = Inclination.GetHashCode() + 1;
        SetInclination((MemberInclination)newInclination);
    }

    public void InclinatePrevious()
    {
        if (Inclination == MemberInclination.Upward)
            throw new DomainException("A cabeça já está o menos inclinada possível, não há como inclinar menos.");

        var newInclination = Inclination.GetHashCode() - 1;
        SetInclination((MemberInclination)newInclination);
    }

    private void CheckRotate()
    {
        if (Inclination == MemberInclination.Downward)
            throw new DomainException("Só é possível movimentar a cabeça se ela não estiver inclinada para baixo.");
    }
    #endregion

    #region Validations
    public void Validate()
    {
        var validator = new HeadValidator();
        var validation = validator.Validate(this);

        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(x => x.ErrorMessage).ToList();

            throw new DomainException("Alguns campos estão inválidos, por favor corrija-os", errors);
        }
    }
    #endregion
}
