using Robo.Domain.Entities.Robots;

namespace Robo.Services.Robots;

public interface IRobotService
{
    void ChangeStateElbow(bool contract, bool right);
    void RotateWrist(bool next, bool right);
    void InclinateHead(bool next);
    void RotateHead(bool next);
    Robot GetRobot();
}