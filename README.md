# Carpool Management
This was created from React ASP .NET template.
Application's requirements were:
- Provide Support for Ride Share management:
    - Create Ride Share
    - Update Ride Share
    - Delete Ride Share
- Display Usage Of Cars with all Passengers By Month

## Pre-requisites:
- [.NET SDK](https://dotnet.microsoft.com/en-us/download)
- [Node.js](https://nodejs.org/en/download/current)
- React ^18.2.0
- [rimraf](https://www.npmjs.com/package/rimraf)

## Startup:
Considering you meet app's pre-requisites, appplication can be started multiple ways:
- With Visual Studio
    - Open the project in Visual Studio click on run app will open in new browser window.
- With *'dotnet run'* command  
To run *'dotnet run'* command you'll need to install [.NET SDK](https://dotnet.microsoft.com/en-us/download)
    1. Open terminal in project folder ( .\CarpoolManagement ) 
    2. Run command  
        *dotnet run*
    3. Navigate to: "https://localhost:7096" to see the app
        - localhost url can be modified in *profiles* section of project's launchSettings.json file
- With Docker  
To run application with Docker you need to install it. More information is [here](https://docs.docker.com/get-docker/)
    1. Open terminal in project folder ( .\CarpoolManagement )
    2. Build the docker image with the following command  
        *docker build -t carpool-management-image .*
    3. Run Carpool Management app with command  
        *docker run -p 80:80 carpool-management-image*
    4. Open browser and navigate to: [http://localhost:80](http://localhost:80) (Check, whether port is set to 80).

## Description:

***You can use [Swagger](https://swagger.io/docs/specification/2-0/what-is-swagger/) for endpoints preview and testing.*** It's accessable on url: *"/swagger"* 

### Project structure:
- **ClientApp** contains React app based on [Create React App](https://create-react-app.dev/)
- **Controllers** contians the API edpoints
- **Models** *in root directory* contains DTOs
- **Persistance** folder contains database related objects (e.g. repository classes)
- **Source** stores all domain related logic, models

#### UI Structure:

- Landing page (HOME)  
    - This page provides summary of all ride share records  
    - It gives you ability to create/update/delete ride share records  

- Form page  
    - This page is accessed by *Create...* / *Update* buttons  
    - It ~~is~~ should be dynamically populated when Update is clicked  
    - Start and End location text inputs are required  
    - Driver and Car selection is automatically selected for first available option  
    - Passenger selection is constrained by car seat count - 1 for driver  
    - Currently lacks the ability to select time for ride share  

- Ride Share View page  
    - Provides report view  
    - It can be filtered by Year / Month / Car identification plate  
    - Currently filter takes in to account only Start Date of Ride Share  

## Known Issues:
- Delete button does not react on first click
- The selected passengers are not populated for "Update form"
- Form validation Alert is not always populated
- Overall fetching data is glitchy

## Todos:
- **Back End (ordered by severity, highest first):**
    - Unit tests
    - Replace mockup database
    - Sort out naming *ride share* vs *car pool*; *employee* vs *passenger*
    - Automapper

- **Front End (ordered by severity, highest first):**
    - Fix data fetching
    - Add option to pick time for ride share
    - Add reset button to Ride Share View
    - Organize code into more components
    - Style!