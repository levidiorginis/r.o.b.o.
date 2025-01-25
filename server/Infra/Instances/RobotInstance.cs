using Robo.Domain.Entities.Robots;

namespace Robo.Infra.Instances;

public static class RobotInstance
{
    private static Robot _robot { get; set; } = new();

    public static Robot Instance => _robot;
}