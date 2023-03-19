const CarDetails = ({ car }) => {    
    return (
        <div className="container row m-1">
            <h5>Car Details</h5>
            <div className="col-sm-9">
                <div className="row">
                    <div className="col-xs-8 col-sm-6">{car?.name ?? "Name N/A"}</div>
                    <div className="col-xs-8 col-sm-6">{car?.type ?? "Type N/A"}</div>
                </div>
                <div className="row">
                    <div className="col-xs-8 col-sm-6">Plate: {car?.plate ?? "N/A"}</div>
                    <div className="col-xs-8 col-sm-6">Number Of Seats: {car?.numberOfSeats ?? "N/A"}</div>
                </div>
            </div>
        </div>
    );
}

export default CarDetails;