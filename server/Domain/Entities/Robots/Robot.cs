using Robo.Domain.Entities.Members.Arms;
using Robo.Domain.Entities.Members.Heads;

namespace Robo.Domain.Entities.Robots;

public class Robot
{
    #region Properties
    public Arm LeftArm { get; private set; } = new();
    public Arm RightArm { get; private set; } = new();
    public Head Head { get; private set; } = new();
    #endregion
}