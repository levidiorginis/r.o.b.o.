// Componente Button
interface ButtonProps {
    onClick: () => void;
    disabled?: boolean;
    children: React.ReactNode;
}

const Button: React.FC<ButtonProps> = ({ onClick, disabled = false, children }) => (
    <button className="button" onClick={onClick} disabled={disabled} >
        {children}
    </button>
);

export default Button;