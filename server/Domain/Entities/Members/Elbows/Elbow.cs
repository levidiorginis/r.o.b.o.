using Robo.Core.Exceptions;
using Robo.Domain.Entities.Members.Enums;

namespace Robo.Domain.Entities.Members.Elbows;

public class Elbow
{
    #region Properties
    public MemberState State { get; private set; }
    #endregion

    #region CTOR
    public Elbow()
    {
        State = MemberState.AtRest;
        Validate();
    }
    #endregion

    #region Methods
    public void SetState(MemberState state)
    {
        State = state;
        Validate();
    }
    #endregion

    #region Validations
    public void Validate()
    {
        var validator = new ElbowValidator();
        var validation = validator.Validate(this);

        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(x => x.ErrorMessage).ToList();

            throw new DomainException("Alguns campos estão inválidos, por favor corrija-os", errors);
        }
    }
    #endregion
}