import { useEffect, useState } from "react";

import CarDetails from "./CarDetails";
import PassengerTable from "./PassengerTable";

const RideShareView = () => {
    const [months, setMonths] = useState(Array.from({ length: 12 }, (_, i) => i + 1));
    const [selectedMonth, setSelectedMonth] = useState(Number);

    const [years, setYears] = useState([2020]);
    const [selectedYear, setSelectedYear] = useState(Number);

    const [carPlates, setCarPlates] = useState([]);
    const [selectedCarPlate, setSelectedCarPlate] = useState();

    const [loading, setLoading] = useState(true);
    const [reports, setReports] = useState([]);
    const [displayedReports, setDisplayedReports] = useState([]);

    useEffect(() => {
        fetch('/api/rideshare/report')
            .then(response => {
                if (response.ok) {
                    return response.json();
                }
                throw Error('could not fetch the data for that resource');
            })
            .then(reports => {
                setReports(reports);
                setDisplayedReports(reports);
                
                let reportYears = [...new Set(reports.map(report => report.year))];
                setYears(reportYears);

                let reportMonths = [...new Set(reports.map(report => report.month))];
                setMonths(reportMonths);

                let reportCarPlates = reports.map(report => report.car.plate);
                let filteredReportCarPlates = [...new Set(reportCarPlates.map(plate => plate))];
                setCarPlates(filteredReportCarPlates);                

                setLoading(false);
            })
            .catch(err => {
                console.log(err);
            })
    }, []);

    const handleMonthChange = (changedMonth) => {
        setSelectedMonth(changedMonth);
        filterReports(changedMonth, selectedYear);
    };
    
    const handleYearChange = (changedYear) => {
        setSelectedYear(changedYear);
        filterReports(selectedMonth, changedYear);
    };
    
    const handleCarPlateChange = (changedCarPlate) => {
        setSelectedCarPlate(changedCarPlate);
        filterReports(selectedMonth, selectedYear, changedCarPlate);
    };
    
    const filterReports = (monthFilter, yearFilter, carPlate) => {
        let filteredReports = reports;

        if(!!monthFilter)
        {
            filteredReports = filteredReports.filter(report => report.month == monthFilter);
        }

        if(!!yearFilter)
        {
            filteredReports = filteredReports.filter(report => report.year == yearFilter);
        }

        if(!!carPlate)
        {
            filteredReports = filteredReports.filter(report => report.car.plate == carPlate);
        }

        setDisplayedReports(filteredReports);
    };

    return (
        <div>
            <h3>RideShareView</h3>
            <div className="row m-2">
                <div className="col-md-4">
                    <label>Year</label>
                    <select
                        className="form-select"
                        value={selectedYear}
                        onChange={(e) => handleYearChange(e.target.value)}
                    >
                        <option hidden value="">Select year</option>
                        {years.map(year =>
                            <option key={year} value={year}>{year}</option>
                        )}
                    </select>
                </div>
                <div className="col-md-4">
                    <label>Month</label>
                    <select
                        className="form-select"
                        value={selectedMonth}
                        onChange={(e) => handleMonthChange(e.target.value)}
                    >
                        <option  hidden value="">Select month</option>
                        {months.map(m =>
                            <option key={m} value={m}>{m}</option>
                        )}
                    </select>
                </div>
                <div className="col-md-4">
                    <label>Car Plate</label>
                    <select
                        className="form-select"
                        value={selectedCarPlate}
                        onChange={(e) => handleCarPlateChange(e.target.value)}
                    >
                        <option hidden value="">Select Car</option>
                        {carPlates.map(plate =>
                            <option key={plate} value={plate}>{plate}</option>
                        )}
                    </select>
                </div>
            </div>
            <div className="row m-2 p-2">
                {loading && <div>Loading...</div>}
                {displayedReports.length <=0 && <div>No reports for current selection</div>}
                {displayedReports?.length > 0 &&
                    <table className='table table-striped' aria-labelledby="tabelLabel">
                        <tbody>
                            {displayedReports.map(report =>
                                <tr key={`${report.year}${report.month}${report.car.plate}`}>
                                    <td>
                                        <div className="container row m-1">
                                            <h5 className="col-md-10">Number of trips: {report.trips}</h5>
                                            <h5 className="col-md-2">{report.month} / {report.year}</h5>
                                        </div>

                                        <CarDetails car={report.car} />
                                        <PassengerTable passengerList={report.passengers} />
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                }
            </div>
        </div>
    )
}

export default RideShareView;
