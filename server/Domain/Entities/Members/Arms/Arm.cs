using Robo.Core.Exceptions;
using Robo.Domain.Entities.Members.Elbows;
using Robo.Domain.Entities.Members.Enums;
using Robo.Domain.Entities.Members.Wrists;

namespace Robo.Domain.Entities.Members.Arms;

public class Arm
{
    #region Properties
    public Elbow Elbow { get; private set; } = new();
    public Wrist Wrist { get; private set; } = new();
    #endregion

    #region Methods
    public void ContractElbow()
    {
        if (Elbow.State == MemberState.StronglyContracted)
            throw new DomainException("O cotovelo já está fortemente contraido, não há como contrair ainda mais.");

        var newState = Elbow.State.GetHashCode() + 1;
        Elbow.SetState((MemberState)newState);
    }

    public void RelaxElbow()
    {
        if (Elbow.State == MemberState.AtRest)
            throw new DomainException("O cotovelo já está em repouso, não há como relaxar ainda mais.");

        var newState = Elbow.State.GetHashCode() - 1;
        Elbow.SetState((MemberState)newState);
    }

    public void RotateWristNext()
    {
        if (Wrist.Rotation == MemberRotation.Positive180)
            throw new DomainException("O pulso já está no maior ângulo possível, não há como rotacionar mais.");

        CheckRotateWrist();
        var newRotate = Wrist.Rotation.GetHashCode() + 1;
        Wrist.SetRotation((MemberRotation)newRotate);
    }

    public void RotateWristPrevious()
    {
        if (Wrist.Rotation == MemberRotation.Negative90)
            throw new DomainException("O pulso já está no menor ângulo possível, não há como rotacionar mais.");

        CheckRotateWrist();
        var newRotate = Wrist.Rotation.GetHashCode() - 1;
        Wrist.SetRotation((MemberRotation)newRotate);
    }

    private void CheckRotateWrist()
    {
        if (Elbow.State != MemberState.StronglyContracted)
            throw new DomainException("Só é possível movimentar o pulso se o cotovelo estiver fortemente contraído.");
    }
    #endregion
}
