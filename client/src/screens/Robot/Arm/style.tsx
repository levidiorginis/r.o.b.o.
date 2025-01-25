import styled from "styled-components";

export const ArmWrapper = styled.div`
  position: relative;
  width: 50px; /* Tamanho do braço */
  height: 200px; /* Altura total do braço */
`;

export const UpperArm = styled.div<{ angle: number }>`
  position: absolute;
  width: 50px;
  height: 100px;
  background-color: #D0E2FF; /* Cor igual ao tronco/cabeça */
  top: 0;
  left: 0;
  transform-origin: top;
  transform: rotate(${({ angle }) => angle}deg);
  transition: transform 0.3s ease;
`;

export const LowerArm = styled.div<{ angle: number; isLeftHand: boolean }>`
  position: absolute;
  width: 50px;
  height: 100px;
  background-color: #A0B9FF; /* Cor do antebraço */
  top: 100px;
  left: 0;
  transform-origin: top;
  transform: rotate(${({ angle, isLeftHand }) => isLeftHand ? angle : -angle}deg); /* Rotações do cotovelo */
  transition: transform 0.3s ease;

  /* Mão vinculada ao LowerArm */
  display: flex;
  justify-content: center;
  align-items: flex-end;  /* Alinhando a mão na ponta do antebraço */
`;

export const Cube = styled.div<{ size: number; rotateX?: number; rotateY?: number; zIndex?: number }>`
  width: ${({ size }) => size}px;
  height: ${({ size }) => size}px;
  position: relative;
  transform-style: preserve-3d;
  transform: ${({ rotateX, rotateY }) => `rotateX(${rotateX || 0}deg) rotateY(${rotateY || 0}deg)`};
  transition: transform 0.3s ease-out;
  z-index: ${({ zIndex }) => zIndex || 0}; /* Permite configurar o z-index */
`;

export const Face = styled.div<{ color: string; transform: string; size: number }>`
  position: absolute;
  width: ${({ size }) => size}px;
  height: ${({ size }) => size}px;
  background-color: ${({ color }) => color};
  border: 2px solid #000;
  transform: ${({ transform }) => transform};
  display: flex;
  justify-content: center;
  align-items: center;
`;








