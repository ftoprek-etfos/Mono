export default function Button({text, onClick, href="#"}) {
    return (
      <a onClick={onClick} className="Button" href={href}>
        <h1>{text}</h1>
      </a>
    );
  }