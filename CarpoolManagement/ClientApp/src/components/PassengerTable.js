const PassengerTable = ({ passengerList }) => {
    return (
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
                        {passengerList?.map(passenger =>
                            <tr key={passenger.id}>
                                <td>{passenger.name}</td>
                                <td>{String(passenger.isDriver)}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default PassengerTable;