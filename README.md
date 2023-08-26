# TimCo Retail Manager
A retail management system bulilt by TimCo Enterprise Solutions

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
- Upgrading the application from .NET Framework to .NET Core:
  - API Library -> .NET Standard (so it works with both previous and new app version)
  - UI Library -> .NET Standard (so it works with both previous and new app version)
  - Transfer API to a brand new ASP.NET Core MVC project
  - WPF project -> .NET Core 3.0

## 3rd Party
- MVVM: Caliburn.Micro
- DI: SimpleContainer (Caliburn.Micro)
- ORM: Dapper
- Doc: Swashbuckle Swagger 
- AutoMapper
- Icon: https://icons8.com/; https://icoconvert.com/

## Application screenshots
Login page  
![alt text](Screenshots/Login.png?raw=true)

Cart page  
![alt text](Screenshots/Cart.png?raw=true)

Users page  
![alt text](Screenshots/Users.png?raw=true)

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
	
### Course off-road: my own app development
 - Separate branch for changes
 - Adding products to inventory
 - Inventory actually storing the sales check outs
 - Create new user account
 - WiX installer?
 - Integrate API's (Google GeoAPI, AccuWeather, SunInfo etc).
   - Ask for localization (or type a city)
   - Display weather conditions, sunset sunrise etc.
 - Add a Web-based User Interface
 
	
	