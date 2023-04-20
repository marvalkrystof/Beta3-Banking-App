# Web Banking Application - MVC
Simple "Banking" application using ASP.NET and MSSQL datadabase built on the MVC architecture.

## Contents
Banking app MVC web project <br>
Logging library project <br>
Database model + SQL scripts <br>
Unit tests <br>

## Banking app - Features

### Login and Roles
Customer/Employee can have a user account in the web app, which can be assigned various roles, that are used for gatekeeping to different parts of the application.

### Admin panel
User role management <br>
Creating/Updating web app accounts
### Accounts panel
Account information
### Meetings panel
Meeting information <br>
Meeting creation / updation
### Transaction panel 
Transaction creation - sending money between bank accounts..
### Users panel
Employee information listing,creation,updation <br>
Customer information listing,creation,updation


## Architecture
Architectural pattern used - MVC <br>
Design patterns used - Repository, UnitOfWork, Singleton, Strategy

## Database model


![Relational_Diagram](https://user-images.githubusercontent.com/84131825/233353052-ffee2985-78d4-45cd-b935-ecdca12ec465.png)

## Unit tests
The unit tests cover basic database crud operations and database anomalies which occur when using different Isolation Levels (Phantom Read etc.)


## Requirements
Internet connection <br>
Web hosting able to host ASP.NET application <br>
MSSSQL Database server <br>
