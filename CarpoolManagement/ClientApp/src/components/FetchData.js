import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
      this.state = { cars: [], loading: true };
  }

  componentDidMount() {
    this.populateCarsData();
  }

  static renderCarsTable(cars) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Plate</th>
            <th>Name</th>
            <th>Type</th>
            <th>Color</th>
            <th>Number Of Seats</th>
          </tr>
        </thead>
        <tbody>
          {cars.map(car =>
            <tr key={car.plate}>
              <td>{car.plate}</td>
              <td>{car.name}</td>
              <td>{car.type}</td>
              <td>{car.color}</td>
              <td>{car.numberOfSeats}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : FetchData.renderCarsTable(this.state.cars);

    return (
      <div>
        <h1 id="tabelLabel" >Car Pool</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateCarsData() {
    const response = await fetch('car');
    const data = await response.json();
    this.setState({ cars: data, loading: false });
  }
}
