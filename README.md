# TimCo Retail Manager
A retail management system bulilt by TimCo Enterprise Solutions

## Topics covered (not in chronological order)
### Database (SQL)
- Database schema design from the scratch
- SQL DB creation in Visual Studio
- Stored procedures to handle requests from API
- Wrap DB access operations in transactions using C# code
### API (ASP.NET WebAPI 2)
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
