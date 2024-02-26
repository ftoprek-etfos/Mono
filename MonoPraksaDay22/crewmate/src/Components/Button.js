export default function Button({text, onClick, href="#", isBig=true}) {
  return (
    isBig ? 
    <a onClick={onClick} className="Button" href={href}>
      <h1>{text}</h1>
    </a> : (
      <a onClick={onClick} className="Button" href={href}>
        <h2>{text}</h2>
      </a>
    )
  );
}