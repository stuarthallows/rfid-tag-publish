import './TagId.css';

interface Props {
    value: string;
}

export const TagId = ({ value }: Props) => {
    const firstPart = value.slice(0, 8);
    const secondPart = value.slice(8);

    return <div className="tagId">
        <span className="firstPart">{firstPart}</span>{secondPart}
    </div>;
}
