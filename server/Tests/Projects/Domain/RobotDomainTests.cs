using FluentAssertions;
using Robo.Core.Exceptions;
using Robo.Domain.Entities.Members.Enums;
using Robo.Domain.Entities.Robots;
using Robo.Tests.Fixtures;
using Xunit;

namespace Robo.Tests.Projects.Domain;

public class RobotDomainTests
{
    #region DomainExceptionsMessages
    public const string ElbowMustBeStronglyContractedForWristMovement = "Só é possível movimentar o pulso se o cotovelo estiver fortemente contraído.";
    public const string ElbowAlreadyStronglyContracted = "O cotovelo já está fortemente contraido, não há como contrair ainda mais.";
    public const string ElbowAlreadyAtRest = "O cotovelo já está em repouso, não há como relaxar ainda mais.";
    public const string WristAtMaxRotation = "O pulso já está no maior ângulo possível, não há como rotacionar mais.";
    public const string WristAtMinRotation = "O pulso já está no menor ângulo possível, não há como rotacionar mais.";
    public const string HeadAtMaxRotation = "A cabeça já está no maior ângulo possível, não há como rotacionar mais.";
    public const string HeadAtMinRotation = "A cabeça já está no menor ângulo possível, não há como rotacionar mais.";
    public const string HeadAtMaxInclination = "A cabeça já está o mais inclinada possível, não há como inclinar mais.";
    public const string HeadAtMinInclination = "A cabeça já está o menos inclinada possível, não há como inclinar menos.";
    public const string OnlyMoveHeadIfNotInclinedDownward = "Só é possível movimentar a cabeça se ela não estiver inclinada para baixo.";

    #endregion

    #region O estado inicial dos movimentos é Em Repouso.
    [Fact(DisplayName = "Create Robot When Initial State Is At Rest")]
    public void CreateRobot_WhenInitialStateIsAtRest_ShouldSetAllPartsToRest()
    {
        //Arrange
        var robot = new Robot();

        //Assert
        robot.RightArm.Elbow.State.Should().Be(MemberState.AtRest, "right elbow should be at rest");
        robot.LeftArm.Elbow.State.Should().Be(MemberState.AtRest, "left elbow should be at rest");

        robot.RightArm.Wrist.Rotation.Should().Be(MemberRotation.AtRest, "right wrist should be at rest");
        robot.LeftArm.Wrist.Rotation.Should().Be(MemberRotation.AtRest, "left wrist should be at rest");

        robot.Head.Rotation.Should().Be(MemberRotation.AtRest, "head rotation should be at rest");
        robot.Head.Inclination.Should().Be(MemberInclination.AtRest, "head inclination should be at rest");
    }
    #endregion

    #region Só poderá movimentar o Pulso caso o Cotovelo esteja Fortemente Contraído.
    [Theory(DisplayName = "Move Wrist When Elbow Is Not Strongly Contracted")]
    [InlineData(MemberState.AtRest, true, MemberRotation.Negative90)]
    [InlineData(MemberState.AtRest, true, MemberRotation.Negative45)]
    [InlineData(MemberState.AtRest, true, MemberRotation.AtRest)]
    [InlineData(MemberState.AtRest, true, MemberRotation.Positive45)]
    [InlineData(MemberState.AtRest, true, MemberRotation.Positive90)]
    [InlineData(MemberState.AtRest, true, MemberRotation.Positive135)]
    [InlineData(MemberState.SlightlyContracted, true, MemberRotation.Negative90)]
    [InlineData(MemberState.SlightlyContracted, true, MemberRotation.Negative45)]
    [InlineData(MemberState.SlightlyContracted, true, MemberRotation.AtRest)]
    [InlineData(MemberState.SlightlyContracted, true, MemberRotation.Positive45)]
    [InlineData(MemberState.SlightlyContracted, true, MemberRotation.Positive90)]
    [InlineData(MemberState.SlightlyContracted, true, MemberRotation.Positive135)]
    [InlineData(MemberState.Contracted, true, MemberRotation.Negative90)]
    [InlineData(MemberState.Contracted, true, MemberRotation.Negative45)]
    [InlineData(MemberState.Contracted, true, MemberRotation.AtRest)]
    [InlineData(MemberState.Contracted, true, MemberRotation.Positive45)]
    [InlineData(MemberState.Contracted, true, MemberRotation.Positive90)]
    [InlineData(MemberState.Contracted, true, MemberRotation.Positive135)]
    [InlineData(MemberState.AtRest, false, MemberRotation.Positive180)]
    [InlineData(MemberState.AtRest, false, MemberRotation.Positive135)]
    [InlineData(MemberState.AtRest, false, MemberRotation.Positive90)]
    [InlineData(MemberState.AtRest, false, MemberRotation.Positive45)]
    [InlineData(MemberState.AtRest, false, MemberRotation.AtRest)]
    [InlineData(MemberState.AtRest, false, MemberRotation.Negative45)]
    [InlineData(MemberState.SlightlyContracted, false, MemberRotation.Positive180)]
    [InlineData(MemberState.SlightlyContracted, false, MemberRotation.Positive135)]
    [InlineData(MemberState.SlightlyContracted, false, MemberRotation.Positive90)]
    [InlineData(MemberState.SlightlyContracted, false, MemberRotation.Positive45)]
    [InlineData(MemberState.SlightlyContracted, false, MemberRotation.AtRest)]
    [InlineData(MemberState.SlightlyContracted, false, MemberRotation.Negative45)]
    [InlineData(MemberState.Contracted, false, MemberRotation.Positive180)]
    [InlineData(MemberState.Contracted, false, MemberRotation.Positive135)]
    [InlineData(MemberState.Contracted, false, MemberRotation.Positive90)]
    [InlineData(MemberState.Contracted, false, MemberRotation.Positive45)]
    [InlineData(MemberState.Contracted, false, MemberRotation.AtRest)]
    [InlineData(MemberState.Contracted, false, MemberRotation.Negative45)]
    public void MoveWrist_WhenElbowIsNotStronglyContracted_ShouldThrowException(MemberState elbowState, bool next, MemberRotation initialRotation)
    {
        //Arrange
        var robot = new Robot().SetupRobotWithContractedElbow(MemberState.StronglyContracted);
        robot.SetupRobotWithRotateWrist(initialRotation);
        robot.SetupRobotWithContractedElbow(elbowState);

        //Act & Assert
        DomainException exceptionRight;
        DomainException exceptionLeft;

        if (next)
        {
            exceptionRight = Assert.Throws<DomainException>(() => robot.RightArm.RotateWristNext()); //Error 
            exceptionLeft = Assert.Throws<DomainException>(() => robot.LeftArm.RotateWristNext()); //Error 
        }
        else
        {
            exceptionRight = Assert.Throws<DomainException>(() => robot.RightArm.RotateWristPrevious()); //Error 
            exceptionLeft = Assert.Throws<DomainException>(() => robot.LeftArm.RotateWristPrevious()); //Error 
        }

        //Assert
        Assert.Equal(ElbowMustBeStronglyContractedForWristMovement, exceptionRight.Message);
        Assert.Equal(ElbowMustBeStronglyContractedForWristMovement, exceptionLeft.Message);
    }
    #endregion

    #region Só poderá Rotacionar a Cabeça caso sua Inclinação da Cabeça não esteja em estado Para Baixo.
    [Theory(DisplayName = "Rotate Head When Inclination Head Is Downward")]
    [InlineData(true, MemberRotation.Negative90)]
    [InlineData(true, MemberRotation.Negative45)]
    [InlineData(true, MemberRotation.AtRest)]
    [InlineData(true, MemberRotation.Positive45)]
    [InlineData(false, MemberRotation.Positive90)]
    [InlineData(false, MemberRotation.Positive45)]
    [InlineData(false, MemberRotation.AtRest)]
    [InlineData(false, MemberRotation.Negative45)]
    public void RotateHead_WhenInclinationHeadIsDonward_ShouldThrowException(bool next, MemberRotation initialRotation)
    {
        //Arrange
        var robot = new Robot().SetupRobotWithRotateHead(initialRotation);
        robot.SetupRobotWithInclinateHead(MemberInclination.Downward);

        //Act & Assert
        DomainException exception;

        if (next)
        {
            exception = Assert.Throws<DomainException>(() => robot.Head.RotateNext()); //Error
        }
        else
        {
            exception = Assert.Throws<DomainException>(() => robot.Head.RotatePrevious()); //Error
        }

        //Assert
        Assert.Equal(OnlyMoveHeadIfNotInclinedDownward, exception.Message);
    }
    #endregion

    #region Ao realizar a progressão de estados, é necessário que sempre siga a ordem crescente ou decrescente, por exemplo, a partir do estado 4, pode-se ir para os estados 3 ou 5, nunca pulando um estado.
    #region Elbow
    [Theory(DisplayName = "Contract Elbow")]
    [InlineData(MemberState.AtRest, MemberState.SlightlyContracted)]
    [InlineData(MemberState.SlightlyContracted, MemberState.Contracted)]
    [InlineData(MemberState.Contracted, MemberState.StronglyContracted)]
    public void ContractElbow_ShouldTransitionToExpectedState(MemberState initialState, MemberState expectedState)
    {
        //Arrange
        var robot = new Robot().SetupRobotWithContractedElbow(initialState);

        //Act
        robot.RightArm.ContractElbow();
        robot.LeftArm.ContractElbow();

        //Assert
        robot.RightArm.Elbow.State.Should().Be(expectedState, $"right elbow should transition from {initialState} to {expectedState}");
        robot.LeftArm.Elbow.State.Should().Be(expectedState, $"left elbow should transition from {initialState} to {expectedState}");
    }

    [Theory(DisplayName = "Relax Elbow")]
    [InlineData(MemberState.SlightlyContracted, MemberState.AtRest)]
    [InlineData(MemberState.Contracted, MemberState.SlightlyContracted)]
    [InlineData(MemberState.StronglyContracted, MemberState.Contracted)]
    public void RelaxElbow_ShouldTransitionToExpectedState(MemberState initialState, MemberState expectedState)
    {
        //Arrange
        var robot = new Robot().SetupRobotWithContractedElbow(initialState);

        //Act
        robot.RightArm.RelaxElbow();
        robot.LeftArm.RelaxElbow();

        //Assert
        robot.RightArm.Elbow.State.Should().Be(expectedState, $"right elbow should transition from {initialState} to {expectedState}");
        robot.LeftArm.Elbow.State.Should().Be(expectedState, $"left elbow should transition from {initialState} to {expectedState}");
    }

    [Fact(DisplayName = "Contract Elbow When Already Strongly Contracted")]
    public void ContractElbow_WhenAlreadyStronglyContracted_ShouldThrowException()
    {
        //Arrange
        var robot = new Robot().SetupRobotWithContractedElbow(MemberState.StronglyContracted);

        //Act & Assert
        var exceptionRight = Assert.Throws<DomainException>(() => robot.RightArm.ContractElbow()); //Error 
        var exceptionLeft = Assert.Throws<DomainException>(() => robot.LeftArm.ContractElbow()); //Error 

        //Assert
        Assert.Equal(ElbowAlreadyStronglyContracted, exceptionRight.Message);
        Assert.Equal(ElbowAlreadyStronglyContracted, exceptionLeft.Message);
    }

    [Fact(DisplayName = "Relax Elbow When Already At Rest")]
    public void RelaxElbow_WhenAlreadyAtRest_ShouldThrowException()
    {
        //Arrange
        var robot = new Robot().SetupRobotWithContractedElbow(MemberState.AtRest);

        //Act & Assert
        var exceptionRight = Assert.Throws<DomainException>(() => robot.RightArm.RelaxElbow()); //Error 
        var exceptionLeft = Assert.Throws<DomainException>(() => robot.LeftArm.RelaxElbow()); //Error 

        //Assert
        Assert.Equal(ElbowAlreadyAtRest, exceptionRight.Message);
        Assert.Equal(ElbowAlreadyAtRest, exceptionLeft.Message);
    }
    #endregion

    #region Wrist
    [Theory(DisplayName = "Rotate Wrist Next")]
    [InlineData(MemberRotation.Negative90, MemberRotation.Negative45)]
    [InlineData(MemberRotation.Negative45, MemberRotation.AtRest)]
    [InlineData(MemberRotation.AtRest, MemberRotation.Positive45)]
    [InlineData(MemberRotation.Positive45, MemberRotation.Positive90)]
    [InlineData(MemberRotation.Positive90, MemberRotation.Positive135)]
    [InlineData(MemberRotation.Positive135, MemberRotation.Positive180)]
    public void RotateWristNext_ShouldTransitionToNextRotation(MemberRotation initialRotation, MemberRotation expectedRotation)
    {
        //Arrange
        var robot = new Robot().SetupRobotWithContractedElbow(MemberState.StronglyContracted);
        robot.SetupRobotWithRotateWrist(initialRotation);

        //Act
        robot.RightArm.RotateWristNext();
        robot.LeftArm.RotateWristNext();

        //Assert
        robot.RightArm.Wrist.Rotation.Should().Be(expectedRotation, $"right wrist should transition from {initialRotation} to {expectedRotation}");
        robot.LeftArm.Wrist.Rotation.Should().Be(expectedRotation, $"left wrist should transition from {initialRotation} to {expectedRotation}");
    }

    [Theory(DisplayName = "Rotate Wrist Previous")]
    [InlineData(MemberRotation.Positive180, MemberRotation.Positive135)]
    [InlineData(MemberRotation.Positive135, MemberRotation.Positive90)]
    [InlineData(MemberRotation.Positive90, MemberRotation.Positive45)]
    [InlineData(MemberRotation.Positive45, MemberRotation.AtRest)]
    [InlineData(MemberRotation.AtRest, MemberRotation.Negative45)]
    [InlineData(MemberRotation.Negative45, MemberRotation.Negative90)]
    public void RotateWristPrevious_ShouldTransitionToPreviousRotation(MemberRotation initialRotation, MemberRotation expectedRotation)
    {
        //Arrange
        var robot = new Robot().SetupRobotWithContractedElbow(MemberState.StronglyContracted);
        robot.SetupRobotWithRotateWrist(initialRotation);

        //Act
        robot.RightArm.RotateWristPrevious();
        robot.LeftArm.RotateWristPrevious();

        //Assert
        robot.RightArm.Wrist.Rotation.Should().Be(expectedRotation, $"right wrist should transition from {initialRotation} to {expectedRotation}");
        robot.LeftArm.Wrist.Rotation.Should().Be(expectedRotation, $"left wrist should transition from {initialRotation} to {expectedRotation}");
    }

    [Fact(DisplayName = "Rotate Wrist Next When Wrist Is At Max Rotation")]
    public void RotateWristNext_WhenWristIsAtMaxRotation_ShouldThrowException()
    {
        //Arrange
        var robot = new Robot().SetupRobotWithContractedElbow(MemberState.StronglyContracted);
        robot.SetupRobotWithRotateWrist(MemberRotation.Positive180);

        //Act & Assert
        var exceptionRight = Assert.Throws<DomainException>(() => robot.RightArm.RotateWristNext()); //Error 
        var exceptionLeft = Assert.Throws<DomainException>(() => robot.LeftArm.RotateWristNext()); //Error 

        //Assert
        Assert.Equal(WristAtMaxRotation, exceptionRight.Message);
        Assert.Equal(WristAtMaxRotation, exceptionLeft.Message);
    }

    [Fact(DisplayName = "Rotate Wrist Previous When Wrist Is At Min Rotation")]
    public void RotateWristPrevious_WhenWristIsAtMinRotation_ShouldThrowException()
    {
        //Arrange
        var robot = new Robot().SetupRobotWithContractedElbow(MemberState.StronglyContracted);
        robot.SetupRobotWithRotateWrist(MemberRotation.Negative90);

        //Act & Assert
        var exceptionRight = Assert.Throws<DomainException>(() => robot.RightArm.RotateWristPrevious()); //Error 
        var exceptionLeft = Assert.Throws<DomainException>(() => robot.LeftArm.RotateWristPrevious()); //Error 

        //Assert
        Assert.Equal(WristAtMinRotation, exceptionRight.Message);
        Assert.Equal(WristAtMinRotation, exceptionLeft.Message);
    }
    #endregion

    #region Rotate Head
    [Theory(DisplayName = "Rotate Head Next")]
    [InlineData(MemberRotation.Negative90, MemberRotation.Negative45)]
    [InlineData(MemberRotation.Negative45, MemberRotation.AtRest)]
    [InlineData(MemberRotation.AtRest, MemberRotation.Positive45)]
    [InlineData(MemberRotation.Positive45, MemberRotation.Positive90)]
    public void RotateHeadNext_ShouldTransitionToNextRotation(MemberRotation initialRotation, MemberRotation expectedRotation)
    {
        //Arrange
        var robot = new Robot().SetupRobotWithRotateHead(initialRotation);

        //Act
        robot.Head.RotateNext();

        //Assert
        robot.Head.Rotation.Should().Be(expectedRotation, $"head rotation should transition from {initialRotation} to {expectedRotation}");
    }

    [Theory(DisplayName = "Rotate Head Previous")]
    [InlineData(MemberRotation.Positive90, MemberRotation.Positive45)]
    [InlineData(MemberRotation.Positive45, MemberRotation.AtRest)]
    [InlineData(MemberRotation.AtRest, MemberRotation.Negative45)]
    [InlineData(MemberRotation.Negative45, MemberRotation.Negative90)]
    public void RotateHeadPrevious_ShouldTransitionToPreviousRotation(MemberRotation initialRotation, MemberRotation expectedRotation)
    {
        //Arrange
        var robot = new Robot().SetupRobotWithRotateHead(initialRotation);

        //Act
        robot.Head.RotatePrevious();

        //Assert
        robot.Head.Rotation.Should().Be(expectedRotation, $"head rotation should transition from {initialRotation} to {expectedRotation}");
    }

    [Fact(DisplayName = "Rotate Head Next When Head Is At Max Rotation")]
    public void RotateHeadNext_WhenHeadIsAtMaxRotation_ShouldThrowException()
    {
        //Arrange
        var robot = new Robot().SetupRobotWithRotateHead(MemberRotation.Positive90);

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => robot.Head.RotateNext()); //Error

        //Assert
        Assert.Equal(HeadAtMaxRotation, exception.Message);
    }

    [Fact(DisplayName = "Rotate Head Previous When Head Is At Min Rotation")]
    public void RotateHeadPrevious_WhenHeadIsAtMinRotation_ShouldThrowException()
    {
        //Arrange
        var robot = new Robot().SetupRobotWithRotateHead(MemberRotation.Negative90);

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => robot.Head.RotatePrevious()); //Error

        //Assert
        Assert.Equal(HeadAtMinRotation, exception.Message);
    }
    #endregion

    #region Inclinate Head
    [Theory(DisplayName = "Inclinate Head Next")]
    [InlineData(MemberInclination.Upward, MemberInclination.AtRest)]
    [InlineData(MemberInclination.AtRest, MemberInclination.Downward)]
    public void InclinateHeadNext_ShouldTransitionToNextInclination(MemberInclination initialInclination, MemberInclination expectedInclination)
    {
        //Arrange
        var robot = new Robot().SetupRobotWithInclinateHead(initialInclination);

        //Act
        robot.Head.InclinateNext();

        //Assert
        robot.Head.Inclination.Should().Be(expectedInclination, $"head inclination should transition from {initialInclination} to {expectedInclination}");
    }

    [Theory(DisplayName = "Inclinate Head Previous")]
    [InlineData(MemberInclination.Downward, MemberInclination.AtRest)]
    [InlineData(MemberInclination.AtRest, MemberInclination.Upward)]
    public void InclinateHeadPrevious_ShouldTransitionToPreviousInclination(MemberInclination initialInclination, MemberInclination expectedInclination)
    {
        //Arrange
        var robot = new Robot().SetupRobotWithInclinateHead(initialInclination);

        //Act
        robot.Head.InclinatePrevious();

        //Assert
        robot.Head.Inclination.Should().Be(expectedInclination, $"head should transition from {initialInclination} to {expectedInclination}");
    }

    [Fact(DisplayName = "Inclinate Head Next When Head Is At Min Inclination")]
    public void InclinateHeadNext_WhenHeadIsAtMinInclination_ShouldThrowException()
    {
        //Arrange
        var robot = new Robot().SetupRobotWithInclinateHead(MemberInclination.Downward);

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => robot.Head.InclinateNext()); //Error

        //Assert
        Assert.Equal(HeadAtMaxInclination, exception.Message);
    }

    [Fact(DisplayName = "Inclinate Head Previous When Head Is At Max Inclination")]
    public void InclinateHeadPrevious_WhenHeadIsAtMaxInclination_ShouldThrowException()
    {
        //Arrange
        var robot = new Robot().SetupRobotWithInclinateHead(MemberInclination.Upward);

        //Act & Assert
        var exception = Assert.Throws<DomainException>(() => robot.Head.InclinatePrevious()); //Error

        //Assert
        Assert.Equal(HeadAtMinInclination, exception.Message);
    }
    #endregion
    #endregion
}