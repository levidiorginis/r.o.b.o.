import { ArmWrapper, Cube, Face, LowerArm, UpperArm } from "./style";

const Arm: React.FC<{ elbowAngle: number, handRotation: number, isLeftHand: boolean }> = ({ elbowAngle, handRotation, isLeftHand }) => {
    return (
        <ArmWrapper>
            {/* Braço superior fixo */}
            <UpperArm angle={0} />

            {/* Braço inferior com rotação e mão vinculada à extremidade inferior */}
            <LowerArm angle={elbowAngle} isLeftHand={isLeftHand}>
                {/* Mão vinculada ao LowerArm */}
                <Cube size={50} rotateX={0} rotateY={handRotation} zIndex={3} style={{ left: '-2px', height: '0' }}>
                    <Face color="#D0E2FF" transform="translateZ(27px)" size={50}>
                        <svg width="70" height="70" viewBox="0 0 200 200">
                            <rect x={isLeftHand ? "143" : "-3"} y="143" width="60" height="10" rx="5" ry="5" fill="#000" />
                            <rect x={isLeftHand ? "140" : "50"} y="143" width="10" height="60" rx="5" ry="5" fill="#000" />
                        </svg>
                    </Face>
                    <Face color="#A0B9FF" transform="rotateY(180deg) translateZ(27px)" size={50} />
                    <Face color="#B0D0FF" transform="rotateX(90deg) translateZ(27px)" size={50} />
                    <Face color="#A0C8FF" transform="rotateX(-90deg) translateZ(27px)" size={50} />
                    <Face color="#98B2FF" transform="rotateY(-90deg) translateZ(27px)" size={50} >
                        {isLeftHand ? null :
                            (
                                <svg width="70" height="70" viewBox="0 0 200 200">
                                    <rect x="143" y="143" width="60" height="10" rx="5" ry="5" fill="#000" />
                                    <rect x="140" y="143" width="10" height="60" rx="5" ry="5" fill="#000" />
                                </svg>
                            )}
                    </Face>
                    <Face color="#B8D0FF" transform="rotateY(90deg) translateZ(27px)" size={50} >
                        {isLeftHand ?
                            (
                                <svg width="70" height="70" viewBox="0 0 200 200">
                                    <rect x="-3" y="143" width="60" height="10" rx="5" ry="5" fill="#000" />
                                    <rect x="50" y="143" width="10" height="60" rx="5" ry="5" fill="#000" />
                                </svg>
                            ) : null}
                    </Face>
                </Cube>
            </LowerArm>
        </ArmWrapper>
    );
};

export default Arm;