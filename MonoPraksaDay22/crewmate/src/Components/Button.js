import { NavLink } from 'react-router-dom' 

export default function Button({text, onClick, isBig=true, href=""}) {
  return (
    isBig ? 
    <NavLink onClick={onClick} className="Button" to={href}>
      <h1>{text}</h1>
    </NavLink> : (
      <a onClick={onClick} className="Button">
        <h2>{text}</h2>
      </a>
    )
  );
}