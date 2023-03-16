import React, { Component } from 'react';
import RideShareDetail from './RideShareDetail';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = {
      rideShares: [],
      employees: [],
      cars: [],
      loading: true
    };
  }

  componentDidMount() {
    this.populateRideShareData();
    if (this.state.employees.length <= 0)
    {
      this.populateEmployeesData();
    }

    if (this.state.cars.length <= 0)
    {
      this.populateCarData();
    }
  }

  render() {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        {this.state.loading ||
          <tbody>
            {this.state.rideShares.map(rideShare =>
              <tr key={rideShare.id}>
                <td>
                  <RideShareDetail rideShare={rideShare}/>
                </td>
                <td>
                  <button className="btn btn-primary m-1">UPDATE</button>
                  <button className="btn btn-danger m-1" onClick={() => {this.deleteRecord(rideShare.id)}}>DELETE</button>
                </td>
              </tr>
            )}
          </tbody>
        }
      </table>
    );
  }

  async populateRideShareData() {
    const response = await fetch('api/rideshare');
    const data = await response.json();
    this.setState({ rideShares: data, loading: false });
  }
  async populateCarData() {
    const response = await fetch('api/car');
    const data = await response.json();
    this.setState({ cars: data });
  }
  async populateEmployeesData() {
    const response = await fetch('api/employee');
    const data = await response.json();
    this.setState({ employees: data });
  }

  deleteRecord(id) {
    fetch('api/rideshare/id/' + id, {
      method: 'DELETE',
      headers: { "Content-Type": "application/json" },
    });

    this.populateRideShareData();
  }
}