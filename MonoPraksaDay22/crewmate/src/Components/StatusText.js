

export default function StatusText({status}) {
    
    const renderContent = () => {
        switch (status) {
        case 'add':
            return (
                <p className="StatusText">
                    Crewmate added successfully!
                </p>
            );
        case 'delete':
            return (
                <p className="StatusText">
                Crewmate deleted successfully!
                </p>
            );
        case 'edit':
            return (
                <p className="StatusText">
                Crewmate edited successfully!
                </p>
            );
        default:
            return null;
        }
    };

    return (
        renderContent()
    );
}