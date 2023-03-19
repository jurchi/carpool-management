import React, { Component } from 'react';
import { Link } from "react-router-dom";
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
  }

  render() {
    return (
      <div>
        <Link to={'/form'}>
          <button className="btn btn-primary">
            Create Ride Share
          </button>
        </Link>
        <table className='table table-striped' aria-labelledby="tabelLabel">
          {this.state.loading ||
            <tbody>
              {this.state.rideShares.map(rideShare =>
                <tr key={rideShare.id}>
                  <td>
                    <RideShareDetail rideShare={rideShare} />
                  </td>
                  <td>
                    <Link to={`/form?id=${rideShare.id}`}>
                      <button className="btn btn-primary">
                        UPDATE
                      </button>
                    </Link>
                    <button className="btn btn-danger" onClick={() => { this.deleteRecord(rideShare.id) }}>DELETE</button>
                  </td>
                </tr>
              )}
            </tbody>
          }
        </table>
      </div>
    );
  }

  async populateRideShareData() {
    const response = await fetch('api/rideshare');
    const data = await response.json();
    this.setState({ rideShares: data, loading: false });
  }

  deleteRecord(id) {
    fetch('api/rideshare/id/' + id, {
      method: 'DELETE',
      headers: { "Content-Type": "application/json" },
    });

    this.populateRideShareData();
  }
}