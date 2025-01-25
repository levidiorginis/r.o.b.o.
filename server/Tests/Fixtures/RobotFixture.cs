using Robo.Domain.Entities.Members.Enums;
using Robo.Domain.Entities.Robots;

namespace Robo.Tests.Fixtures;

public static class RobotFixture
{
    public static Robot SetupRobotWithContractedElbow(this Robot robot, MemberState state)
    {
        if (state == robot.RightArm.Elbow.State && state == robot.LeftArm.Elbow.State) return robot;

        if (state < robot.RightArm.Elbow.State) robot.RightArm.RelaxElbow();
        if (state > robot.RightArm.Elbow.State) robot.RightArm.ContractElbow();
        if (state < robot.LeftArm.Elbow.State) robot.LeftArm.RelaxElbow();
        if (state > robot.LeftArm.Elbow.State) robot.LeftArm.ContractElbow();

        return robot.SetupRobotWithContractedElbow(state);
    }

    public static Robot SetupRobotWithRotateWrist(this Robot robot, MemberRotation rotation)
    {
        if (rotation <= MemberRotation.Negative45) RobotRotateWristPrevious(robot);
        if (rotation <= MemberRotation.Negative90) RobotRotateWristPrevious(robot);

        if (rotation >= MemberRotation.Positive45) RobotRotateWristNext(robot);
        if (rotation >= MemberRotation.Positive90) RobotRotateWristNext(robot);
        if (rotation >= MemberRotation.Positive135) RobotRotateWristNext(robot);
        if (rotation >= MemberRotation.Positive180) RobotRotateWristNext(robot);

        return robot;
    }

    public static Robot SetupRobotWithInclinateHead(this Robot robot, MemberInclination inclination)
    {
        if (inclination <= MemberInclination.Upward) robot.Head.InclinatePrevious();

        if (inclination >= MemberInclination.Downward) robot.Head.InclinateNext();

        return robot;
    }

    public static Robot SetupRobotWithRotateHead(this Robot robot, MemberRotation rotation)
    {
        if (rotation <= MemberRotation.Negative45) robot.Head.RotatePrevious();
        if (rotation <= MemberRotation.Negative90) robot.Head.RotatePrevious();

        if (rotation >= MemberRotation.Positive45) robot.Head.RotateNext();
        if (rotation >= MemberRotation.Positive90) robot.Head.RotateNext();
        if (rotation >= MemberRotation.Positive135) robot.Head.RotateNext();
        if (rotation >= MemberRotation.Positive180) robot.Head.RotateNext();

        return robot;
    }

    private static void RobotRotateWristNext(Robot robot)
    {
        robot.RightArm.RotateWristNext();
        robot.LeftArm.RotateWristNext();
    }

    private static void RobotRotateWristPrevious(Robot robot)
    {
        robot.RightArm.RotateWristPrevious();
        robot.LeftArm.RotateWristPrevious();
    }
}