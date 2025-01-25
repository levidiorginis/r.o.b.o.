import styled from "styled-components";

export const RobotContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
`;

export const TroncoContainer = styled.div`
  display: flex;
`;

export const HeadContainer = styled.div`
  display: flex;
  align-items: flex-end;
`;

export const RobotWrapper = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  perspective: 1000px; /* Mantém a perspectiva 3D */
  position: relative; /* Necessário para o controle do z-index */
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

export const ButtonContainer = styled.div`
  margin: 0 140px;
  display: flex;
  flex-direction: column;
`;