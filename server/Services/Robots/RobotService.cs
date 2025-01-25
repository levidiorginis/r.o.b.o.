using Robo.Domain.Entities.Robots;
using Robo.Infra.Instances;

namespace Robo.Services.Robots;

public class RobotService : IRobotService
{
    public RobotService() { }

    public void ChangeStateElbow(bool contract, bool right)
    {
        if (right)
        {
            if (contract)
                RobotInstance.Instance.RightArm.ContractElbow();
            else
                RobotInstance.Instance.RightArm.RelaxElbow();
        }
        else
        {
            if (contract)
                RobotInstance.Instance.LeftArm.ContractElbow();
            else
                RobotInstance.Instance.LeftArm.RelaxElbow();
        }
    }

    public Robot GetRobot()
    {
        return RobotInstance.Instance;
    }

    public void InclinateHead(bool next)
    {
        if (next)
            RobotInstance.Instance.Head.InclinateNext();
        else
            RobotInstance.Instance.Head.InclinatePrevious();
    }

    public void RotateHead(bool next)
    {
        if (next)
            RobotInstance.Instance.Head.RotateNext();
        else
            RobotInstance.Instance.Head.RotatePrevious();
    }

    public void RotateWrist(bool next, bool right)
    {
        if (right)
        {
            if (next)
                RobotInstance.Instance.RightArm.RotateWristNext();
            else
                RobotInstance.Instance.RightArm.RotateWristPrevious();
        }
        else
        {
            if (next)
                RobotInstance.Instance.LeftArm.RotateWristNext();
            else
                RobotInstance.Instance.LeftArm.RotateWristPrevious();
        }
    }
}