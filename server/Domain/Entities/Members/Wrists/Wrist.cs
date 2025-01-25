using Robo.Core.Exceptions;
using Robo.Domain.Entities.Members.Enums;

namespace Robo.Domain.Entities.Members.Wrists;

public class Wrist
{
    #region Properties
    public MemberRotation Rotation { get; private set; }
    #endregion

    #region CTOR
    public Wrist()
    {
        Rotation = MemberRotation.AtRest;
        Validate();
    }
    #endregion

    #region Methods
    public void SetRotation(MemberRotation rotation)
    {
        Rotation = rotation;
        Validate();
    }
    #endregion

    #region Validations
    public void Validate()
    {
        var validator = new WristValidator();
        var validation = validator.Validate(this);

        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(x => x.ErrorMessage).ToList();

            throw new DomainException("Alguns campos estão inválidos, por favor corrija-os", errors);
        }
    }
    #endregion
}