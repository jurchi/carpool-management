import { useState, useEffect } from "react";

const RideShareDetail = ({ rideShare }) => {
    const [car, setCar] = useState(null)
    const [passengers, setPassengers] = useState(null)

    useEffect(() => {
        const empoyeeRequest = { ids: rideShare.employeeIds }
        fetch('api/employee', {
            method: 'POST',
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(empoyeeRequest)
        }).then((response) => {
            if (!response.ok) {
                throw Error('Could not fetch car data');
            }
            return response.json();
        }).then((data) => {
            setPassengers(data);
        }).catch(error => {
            console.log(error.message);
        });

        const carRequest = { plate: rideShare.carPlate };
        fetch('api/car', {
            method: 'POST',
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(carRequest)
        }).then((response) => {
            if (!response.ok) {
                throw Error('Could not fetch car data');
            }
            return response.json();
        }).then((data) => {
            setCar(data);
        }).catch(error => {
            console.log(error.message);
        });
    }, [])

    return (
        <div className="container">
            <div className="row">
                <h3>Ride ID#: {rideShare.id}</h3>
            </div>
            <div className="container row m-1">
                <h5>Route Details</h5>               
                <div className="col-md-4">From: {rideShare.startLocation} on {formatDate(rideShare.startDate)}</div>
                <div className="col-md-4">To: {rideShare.endLocation} on {formatDate(rideShare.endDate)}</div>
            </div>
            <div className="container row m-1">
                <h5>Car Details</h5>
                <div className="col-sm-9">
                    <p>{car?.name}</p>
                    <div className="row">
                        <div className="col-xs-8 col-sm-4">Plate: {car?.plate}</div>
                        <div className="col-xs-8 col-sm-4">Number Of Seats: {car?.numberOfSeats}</div>
                    </div>
                </div>
            </div>
            <div className="container row m-1">
                <h5>Passengers</h5>
                <div>
                    <table className='table table-striped' aria-labelledby="tabelLabel">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Is Driver</th>
                            </tr>
                        </thead>
                        <tbody>
                            {passengers?.map(passenger =>
                                <tr key={passenger.id}>
                                    <td>{passenger.name}</td>
                                    <td>{String(passenger.isDriver)}</td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
}

export default RideShareDetail;

const formatDate = (dateString) => {
    const options = { day: "numeric", month: "numeric", year: "numeric", hour: 'numeric', hour12: false, minute: 'numeric'}
    return new Date(dateString).toLocaleTimeString("gb", options)
}