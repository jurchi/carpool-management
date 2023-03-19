import { useState, useEffect } from "react";
import PassengerTable from './PassengerTable';
import CarDetails from "./CarDetails";

const RideShareDetail = ({ rideShare }) => {
    const [car, setCar] = useState(null);
    const [passengers, setPassengers] = useState(null);

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
    }, []);

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
            <CarDetails car={car} />
            <PassengerTable passengerList={passengers} />
        </div>
    );
}

export default RideShareDetail;

const formatDate = (dateString) => {
    const options = { day: "numeric", month: "numeric", year: "numeric", hour: 'numeric', hour12: false, minute: 'numeric' }
    return new Date(dateString).toLocaleTimeString("gb", options)
}