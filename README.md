# TimCo Retail Manager
A retail management system built by TimCo Enterprise Solutions.

## Project's background and goal
This project was started following the YouTube Course by Tim Corey regarding building a full-scale application supporting a store's Retail Management System from scratch. The course involved designing and setting up the database, creating a web API and a desktop UI in MVVM pattern, all in legacy .NET Framework (4.7.2). Subsequently transition to .NET Core 3.1 was performed.
After accomplishing that part I created second branch for my own development. This started with doing some modifications, which the original course author has initially omitted, i.e. with the store's inventory and the infamous legacy AppSettings amongst others, so that application was fully operational back again. Then I decided to add a web UI to the project and created a very simple, but functioning one, based solely on my previous experience with web development. It was initially built in ASP.NET MVC 5 Framework (NET 4.7.2) for the backwards compability (AppSettings again). For now it re-uses existing DesktopUI library, which required adding an external DI system (Ninject).

Further steps will include:
- adding another UI in ASP.NET Core technology of choice
- providing a proper unit testing
- providing some integration testing
- implement Repository Pattern
- perhaps integrating some external APIs.

The remaining undone part of the Course involves transfering the application into the Azure cloud, which I am leaving for the final part of development because of the additional cost of the Azure SQL Database maintanace (unfortunately I have already used the free Azure credit in the past).

## Topics covered (not in chronological order)
### Database (SQL)
- Database schema design from the scratch
- SQL DB creation in Visual Studio
- Two separate databases: one for the store data and the EF DB for ASP.NET authentication
- Stored procedures to handle requests from API
- Wrap DB access operations in transactions using C# code
### API (ASP.NET Web API)
- Web API as a layer between DB and UI (DAL)
- Handle requests from UI to DB with micro ORM
- ASP.NET Identity for JWT Authentication
- Seeding an initial user via Postman
- Extending Swagger parameters to enable authentication endpoints with the bearer token
- Assigning User Roles and restricting access
### User Interface (WPF)
- User Interface in WPF framework with MVVM achitecture
- MVVM custom messaging system
### Miscellaneous 
- Logging events with built-in logger
- Upgrading the application from .NET Framework to .NET Core:
  - API Library -> .NET Standard (so it works with both previous and new app version)
  - UI Library -> .NET Standard (so it works with both previous and new app version)
  - Transfer API to a brand new ASP.NET Core MVC project
  - WPF project -> .NET Core 3.0
- Using Azure DevOps for application development management

## 3rd Party
- MVVM: Caliburn.Micro
- DI: SimpleContainer (Caliburn.Micro), Ninject
- ORM: Dapper
- Doc: Swashbuckle Swagger 
- AutoMapper
- Icon: https://icons8.com/; https://icoconvert.com/

## Application screenshots

### Desktop User Interface
Login page:
<img src="/Wengorito/TimCoRetailManager/raw/Course-off-road/Screenshots/Login.png?raw=true width=60% height=60%">
<img src="/Wengorito/TimCoRetailManager/raw/Course-off-road/Screenshots/Login.png width=60% height=60%">
<img src="/Wengorito/TimCoRetailManager/raw/Course-off-road/Screenshots/Login.png">

Sales page:
![alt text](Screenshots/Cart.png?raw=true)

Users page:
![alt text](Screenshots/Users.png?raw=true)

### REST API
Endpoints documentation:
![alt text](Screenshots/Swagger.png?raw=true)

## Application further development ideas:
	* Actually interact with the inventory
	* Move the API to Azure
	* CI/CD process
	* Deploy the Desktop App to Blob Storage for download
	* Introduce a web-based inventory control system (Blazor?)
	* Web-based reporting
	* A Xamarin Forms app for a mobile register

## Phase 2 Roadmap
	1. Take advantage of .NET Core
	2. Set up a simple task board in Azure DevOps
	3. CI/CD with Azure DevOps (move source code to Azure DevOps?)
	4. Move the API to Azure
	5. Move the database to Azure SQL
	6. Deploy the desktop app to Azure Blob storage
	7. Web-based inventory control system
