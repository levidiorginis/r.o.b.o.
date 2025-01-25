import { Cube, RobotContainer, Face, RobotWrapper, TroncoContainer, ButtonContainer, HeadContainer } from "./style";
import React, { useEffect, useState } from "react";
import { MemberInclination, MemberRotation, MemberState } from "./Enums";
import api from "../../services/api";
import Arm from "./Arm";
import Button from "../../components/Button";
import Seta from "../../assets/seta.png"
import SetaRight from "../../assets/seta-right.png"

interface GetRobotArm {
    elbow: {
        state: MemberState
    },
    wrist: {
        rotation: MemberRotation
    }
}

interface GetRobot {
    leftArm: GetRobotArm,
    rightArm: GetRobotArm,
    head: {
        rotation: MemberRotation,
        inclination: MemberInclination
    }
}

export const Robot: React.FC = () => {
    const [headInclination, setHeadInclination] = useState<number>(0);
    const [headRotation, setHeadRotation] = useState<number>(0);
    const [leftElbow, setLeftElbow] = useState<number>(0);
    const [rightElbow, setRightElbow] = useState<number>(0);
    const [leftWrist, setLeftWrist] = useState<number>(0);
    const [rightWrist, setRightWrist] = useState<number>(0);

    const getRobot = async () => {
        const response = await api.get('/api/v1/Robot');
        const getRobot = response.data as GetRobot;
        configureRobot(getRobot);
    }

    const configureRobot = (getRobot: GetRobot) => {
        if (getRobot.head.inclination == MemberInclination.Upward)
            setHeadInclination(15);
        else if (getRobot.head.inclination == MemberInclination.AtRest)
            setHeadInclination(0);
        else if (getRobot.head.inclination == MemberInclination.Downward)
            setHeadInclination(-15);

        if (getRobot.head.rotation == MemberRotation.Negative90)
            setHeadRotation(-90);
        else if (getRobot.head.rotation == MemberRotation.Negative45)
            setHeadRotation(-45);
        else if (getRobot.head.rotation == MemberRotation.AtRest)
            setHeadRotation(0);
        else if (getRobot.head.rotation == MemberRotation.Positive45)
            setHeadRotation(45);
        else if (getRobot.head.rotation == MemberRotation.Positive90)
            setHeadRotation(90);

        if (getRobot.leftArm.elbow.state == MemberState.AtRest)
            setLeftElbow(0);
        else if (getRobot.leftArm.elbow.state == MemberState.SlightlyContracted)
            setLeftElbow(45);
        else if (getRobot.leftArm.elbow.state == MemberState.Contracted)
            setLeftElbow(90);
        else if (getRobot.leftArm.elbow.state == MemberState.StronglyContracted)
            setLeftElbow(135);

        if (getRobot.leftArm.wrist.rotation == MemberRotation.Negative90)
            setLeftWrist(-90);
        else if (getRobot.leftArm.wrist.rotation == MemberRotation.Negative45)
            setLeftWrist(-45);
        else if (getRobot.leftArm.wrist.rotation == MemberRotation.AtRest)
            setLeftWrist(0);
        else if (getRobot.leftArm.wrist.rotation == MemberRotation.Positive45)
            setLeftWrist(45);
        else if (getRobot.leftArm.wrist.rotation == MemberRotation.Positive90)
            setLeftWrist(90);
        else if (getRobot.leftArm.wrist.rotation == MemberRotation.Positive135)
            setLeftWrist(135);
        else if (getRobot.leftArm.wrist.rotation == MemberRotation.Positive180)
            setLeftWrist(180);

        if (getRobot.rightArm.elbow.state == MemberState.AtRest)
            setRightElbow(0);
        else if (getRobot.rightArm.elbow.state == MemberState.SlightlyContracted)
            setRightElbow(45);
        else if (getRobot.rightArm.elbow.state == MemberState.Contracted)
            setRightElbow(90);
        else if (getRobot.rightArm.elbow.state == MemberState.StronglyContracted)
            setRightElbow(135);

        if (getRobot.rightArm.wrist.rotation == MemberRotation.Negative90)
            setRightWrist(-90);
        else if (getRobot.rightArm.wrist.rotation == MemberRotation.Negative45)
            setRightWrist(-45);
        else if (getRobot.rightArm.wrist.rotation == MemberRotation.AtRest)
            setRightWrist(0);
        else if (getRobot.rightArm.wrist.rotation == MemberRotation.Positive45)
            setRightWrist(45);
        else if (getRobot.rightArm.wrist.rotation == MemberRotation.Positive90)
            setRightWrist(90);
        else if (getRobot.rightArm.wrist.rotation == MemberRotation.Positive135)
            setRightWrist(135);
        else if (getRobot.rightArm.wrist.rotation == MemberRotation.Positive180)
            setRightWrist(180);
    }

    const changeStateElbow = async (contract: boolean, right: boolean) => {
        const response = await api.put('/api/v1/Robot/ChangeStateElbow', {}, {
            params: {
                contract,
                right
            }
        })
        const getRobot = response.data as GetRobot;
        configureRobot(getRobot);
    }

    const rotateWrist = async (next: boolean, right: boolean) => {
        const response = await api.put('/api/v1/Robot/RotateWrist', {}, {
            params: {
                next,
                right
            }
        })
        const getRobot = response.data as GetRobot;
        configureRobot(getRobot);
    }

    const inclinateHead = async (next: boolean) => {
        const response = await api.put('/api/v1/Robot/InclinateHead', {}, {
            params: {
                next
            }
        })
        const getRobot = response.data as GetRobot;
        configureRobot(getRobot);
    }

    const rotateHead = async (next: boolean) => {
        const response = await api.put('/api/v1/Robot/RotateHead', {}, {
            params: {
                next
            }
        })
        const getRobot = response.data as GetRobot;
        configureRobot(getRobot);
    }

    useEffect(() => {
        getRobot();
    }, [])

    return (
        <RobotContainer>
            <h1>Robot Controller</h1>

            <RobotWrapper>
                <HeadContainer>
                    <ButtonContainer>
                        <Button onClick={() => rotateHead(false)} disabled={headRotation == -90 || headInclination == -15}>
                            <p>Rotacionar</p>
                            <img src={Seta} width={30} />
                        </Button>
                        <Button onClick={() => rotateHead(true)} disabled={headRotation == 90 || headInclination == -15}>
                            <p>Rotacionar</p>
                            <img src={Seta} width={30} style={{ transform: 'scaleX(-1)' }} />
                        </Button>
                    </ButtonContainer>

                    {/* Cabeça */}
                    <Cube size={100} rotateX={headInclination} rotateY={headRotation} zIndex={3}>
                        <Face color="#D0E2FF" transform="translateZ(50px)" size={100}>
                            <svg width="70" height="70" viewBox="0 0 200 200">
                                <circle cx="60" cy="80" r="15" fill="#000" />
                                <circle cx="140" cy="80" r="15" fill="#000" />
                                <rect x="70" y="125" width="60" height="10" rx="5" ry="5" fill="#000" />
                            </svg>
                        </Face>
                        <Face color="#A0B9FF" transform="rotateY(180deg) translateZ(50px)" size={100} />
                        <Face color="#B0D0FF" transform="rotateX(90deg) translateZ(50px)" size={100} />
                        <Face color="#A0C8FF" transform="rotateX(-90deg) translateZ(50px)" size={100} />
                        <Face color="#98B2FF" transform="rotateY(-90deg) translateZ(50px)" size={100} />
                        <Face color="#B8D0FF" transform="rotateY(90deg) translateZ(50px)" size={100} />
                    </Cube>

                    <ButtonContainer style={{ minWidth: '175px' }}>
                        <Button onClick={() => inclinateHead(true)} disabled={headInclination == -15}>
                            <p>Inclinar</p>
                            <img src={SetaRight} width={30} style={{ transform: 'rotate(90deg)' }} />
                        </Button>
                        <Button onClick={() => inclinateHead(false)} disabled={headInclination == 15}>
                            <p>Inclinar</p>
                            <img src={SetaRight} width={30} style={{ transform: 'rotate(-90deg)' }} />
                        </Button>
                    </ButtonContainer>
                </HeadContainer>

                {/* Pescoço */}
                <Cube size={20} zIndex={1} style={{ margin: '10px 0' }}>
                    <Face color="#98B2FF" transform="translateZ(20px)" size={20} />
                </Cube>

                <TroncoContainer>
                    <ButtonContainer>
                        <Button onClick={() => changeStateElbow(true, false)} disabled={leftElbow == 135}>
                            <p>Contrair</p>
                        </Button>
                        <Button onClick={() => changeStateElbow(false, false)} disabled={leftElbow == 0}>
                            <p>Relaxar</p>
                        </Button>
                        <Button onClick={() => rotateWrist(true, false)} disabled={leftElbow != 135 || leftWrist == 180}>
                            <p>Rotacionar</p>
                            <img src={Seta} width={30} />
                        </Button>
                        <Button onClick={() => rotateWrist(false, false)} disabled={leftElbow != 135 || leftWrist == -90}>
                            <p>Rotacionar</p>
                            <img src={Seta} width={30} style={{ transform: 'scaleX(-1)' }} />
                        </Button>
                    </ButtonContainer>

                    {/* Braço esquerdo */}
                    <Arm elbowAngle={leftElbow} isLeftHand={true} handRotation={leftWrist} />

                    {/* Tronco */}
                    <Cube size={150} zIndex={2} style={{ margin: '0 13px 0 10px' }}>
                        <Face color="#D0E2FF" transform="translateZ(75px)" size={150} />
                    </Cube>

                    {/* Braço direito */}
                    <Arm elbowAngle={rightElbow} isLeftHand={false} handRotation={rightWrist} />

                    <ButtonContainer>
                        <Button onClick={() => changeStateElbow(true, true)} disabled={rightElbow == 135}>
                            <p>Contrair</p>
                        </Button>
                        <Button onClick={() => changeStateElbow(false, true)} disabled={rightElbow == 0}>
                            <p>Relaxar</p>
                        </Button>
                        <Button onClick={() => rotateWrist(true, true)} disabled={rightElbow != 135 || rightWrist == 180}>
                            <p>Rotacionar</p>
                            <img src={Seta} width={30} />
                        </Button>
                        <Button onClick={() => rotateWrist(false, true)} disabled={rightElbow != 135 || rightWrist == -90}>
                            <p>Rotacionar</p>
                            <img src={Seta} width={30} style={{ transform: 'scaleX(-1)' }} />
                        </Button>
                    </ButtonContainer>
                </TroncoContainer>
            </RobotWrapper>
        </RobotContainer>
    );
};