# Carpool Management
This was created from React ASP .NET template.
Application's requirements were:
- Provide Support for Ride Share management:
    - Create Ride Share
    - Update Ride Share
    - Delete Ride Share
- Display Usage Of Cars with all Passengers By Month

## Pre-requisites:
- .NET 6
- React 18.2.0

## Startup:
Appplication can be started multiple ways:
- With Visual Studio
    - Open the project in Visual Studio click on run app will open in new browser window.
- With *'dotnet run'* command
    - To run *'dotnet run'* command you'll need to install [.NET SDK](https://dotnet.microsoft.com/en-us/download)
    - Open terminal in project folder ( .\CarpoolManagement ) and run command *'dotnet run'*
    - Then navigate to: "https://localhost:7096" to see the app
        - localhost url can be modified in *profiles* section of project's launchSettings.json file

## Description:
(WIP)
FE application is divided into three screens.

- Landing page (HOME)  
    - This page provides summary of all ride share records  
    - It gives you ability to create/update/delete ride share records  

- Form page  
    - This page is accessed by *Create ...* / *Update* buttons  
    - It ~~is~~ should be dynamically populated when Update is clicked  
    - Start and End location text inputs are required  
    - Driver and Car selection is automatically selected for first available option  
    - Passenger selection is constrained by car seat count - 1 for driver  
    - Currently lacks the ability to select time for ride share  

- Ride Share View page  
    - Provides report view  
    - It can be filtered by Year / Month / Car identification plate  
    - Currently filter takes in to account only Start Date of Ride Share  

- ***Application has [Swagger](https://swagger.io/docs/specification/2-0/what-is-swagger/) set up. You can access it on '/swagger' url***

## Known Issues:
- Delete button does not react on first click
- The selected passengers are not populated for "Upadte form"
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
    - Organize code into more components
    - Style!