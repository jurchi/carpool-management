import { addDays } from 'date-fns';
import React from 'react';
import { useEffect, useState } from "react";
import { DateRangePicker } from 'react-date-range';
import Select from 'react-select';
import { useNavigate, useLocation } from "react-router-dom";
import 'react-date-range/dist/styles.css'; // main css file
import 'react-date-range/dist/theme/default.css'; // theme css file

const RideShareForm = () => {
    const navigate = useNavigate();

    const { search } = useLocation();
    const params = new URLSearchParams(search);
    const id = params.get('id');

    const [startLocation, setStartLocation] = useState('');
    const [endLocation, setEndLocation] = useState('');

    const [cars, setCars] = useState([]);
    const [carSeatsRemaining, setCarSeatsRemaining] = useState(Number);
    const [selectedCarPlate, setSelectedCarPlate] = useState(String);

    const [drivers, setDrivers] = useState([]);
    const [selectedDriver, setSelectedDriver] = useState(1);

    const [employees, setEmployees] = useState([]);
    const [selectedPassengers, setSelectedPassengers] = useState([]);
    const [passengerOptions, setPassengerOptions] = useState([]);

    const [dates, setDates] = useState([
        {
            startDate: new Date(),
            endDate: addDays(new Date(), 7),
            key: 'selection'
        }
    ]);

    const [options, setOptions] = useState([]);

    useEffect(() => {
        async function populateEmployeeOptions() {
            fetch('api/employee')
                .then(response => {
                    if (response.ok) {
                        return response.json();
                    }
                    throw Error('could not fetch the data for that resource');
                })
                .then(data => {
                    setEmployees(data);
                    let drivers = data?.filter(employee => employee.isDriver === true);
                    setDrivers(drivers);

                    let selectedDriverId = drivers[0].id;
                    setSelectedDriver(selectedDriverId);
                    filterAndSetPassengerOptions(data, selectedDriverId);

                    return data;
                })
                .catch(err => {
                    console.log(err);
                })
        }

        async function execute() {
            await Promise.all([populateEmployeeOptions()]);
        }

        const populateDefaults = ({ options }) => {
            fetch(`api/rideshare/id/${id}`)
                .then(response => {
                    if (response.ok) {
                        return response.json();
                    }
                    throw Error('could not fetch the data for that resource');
                })
                .then(data => {
                    setStartLocation(data.startLocation);
                    setEndLocation(data.endLocation);
                    setSelectedCarPlate(data.carPlate);
                    setSelectedDriver(data.driverId);

                    setSelectedPassengers(data.employeeIds.filter(id => id != data.driverId));
                    setDates([{ startDate: new Date(data.startDate), endDate: new Date(data.endDate), key: 'selection' }]);

                    setPassengerOptions(options);
                })
                .catch(err => {
                    console.log(err);
                })
        }

        execute();

        populateCarOptions();

        if (id) {
            populateDefaults(options);
        }

    }, []);

    const handleSubmit = (e) => {
        e.preventDefault();

        let url = '/api/rideshare'
        let httpMethod = 'POST';

        if (id) {
            httpMethod = 'PUT';
            url += `/id/${id}`;
        }

        let allPassengers = selectedPassengers.map(Number);
        allPassengers.push(selectedDriver);


        const request = {
            startLocation: startLocation,
            endLocation: endLocation,
            startDate: dates[0].startDate,
            endDate: dates[0].endDate,
            carPlate: selectedCarPlate,
            employeeIds: allPassengers,
            driverId: selectedDriver
        };

        console.log(JSON.stringify(request));

        fetch(url, {
            method: httpMethod,
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(request)
        }).then(response => {
            if (!response.ok) { // error coming back from server
                let error = response.json();
                throw {message: error.message};
            }
            return response.json();
        }).then(() => {
            navigate('/');
        })
        .catch(err => {
            console.log(err);
        });
    }

    const handleCarChange = (carPlate) => {
        setSelectedCarPlate(carPlate);
        let car = cars.find(car => car.plate == carPlate);
        
        // Car Seats - 1 driver
        setCarSeatsRemaining(car.numberOfSeats - 1);
    };

    const handleDriverChange = (driverId) => {
        setSelectedDriver(driverId);

        filterAndSetPassengerOptions(employees, driverId);

        setSelectedPassengers(
            selectedPassengers.filter(id => id != driverId)
        );
    }

    const filterAndSetPassengerOptions = (employees, driverId) => {
        let selectOptions = employees.filter(e => e.id != driverId).map(employee => ({ value: employee.id, label: employee.name }));
        setOptions(selectOptions);
    }

    const populateCarOptions = () => {
        fetch('api/car')
            .then(response => {
                if (response.ok) {
                    return response.json();
                }
                throw Error('could not fetch the data for that resource');
            })
            .then(data => {
                setCars(data);
                setSelectedCarPlate(data[0].plate);
                setCarSeatsRemaining(data[0].numberOfSeats - 1);
            })
            .catch(err => {
                console.log(err);
            })
    }

    return (
        <div className="container form">
            <form onSubmit={handleSubmit}>
                <label>Start Location:</label>
                <input
                    type="text"
                    className="form-control"
                    placeholder="Kosice"
                    required
                    value={startLocation}
                    onChange={(e) => setStartLocation(e.target.value)}
                />
                <label>End Location:</label>
                <input
                    type="text"
                    className="form-control"
                    placeholder="Presov"
                    required
                    value={endLocation}
                    onChange={(e) => setEndLocation(e.target.value)}
                />
                <label>Car:</label>
                <select
                    className="form-select"
                    value={selectedCarPlate}
                    onChange={(e) => handleCarChange(e.target.value)}
                    >
                    {cars.map(car =>
                        <option key={car.plate} value={car.plate}>{car.name}</option>
                        )}
                </select>
                <label>Driver:</label>
                <select
                    className="form-select"
                    value={selectedDriver}
                    defaultValue={drivers[0]?.id}
                    onChange={(e) => handleDriverChange(e.target.value)}
                >
                    {drivers.map(driver =>
                        <option key={driver.id} value={driver?.id}>{driver.name}</option>
                        )}
                </select>
                <label>Additional passengers:</label>
                <span className="note">You can select max {carSeatsRemaining} passengers for selected car</span>
                <Select                    
                    name="passengers"
                    isMulti
                    isClearable
                    className="basic-multi-select"
                    options={options}
                    defaultValue={passengerOptions}
                    onChange= {(e) => setSelectedPassengers(e.map(passenger => passenger.value))}
                    isOptionDisabled={() => selectedPassengers.length >= carSeatsRemaining}
                />
                <label>Select dates:</label>
                <div className="m-2">
                    <DateRangePicker
                        onChange={item => setDates([item.selection])}
                        moveRangeOnFirstSelection={false}
                        scroll={{ enabled: true }}
                        ranges={dates}
                        showTimeSelect
                    />
                </div>
                <button type="submit" className='btn btn-primary'>Save</button>
            </form>
        </div >
    );
}

export default RideShareForm;