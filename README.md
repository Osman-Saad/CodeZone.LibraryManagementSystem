# Library Management System
A complete Library Management System developed as part of CodeZone LLC Task, using ASP.NET MVC, Entity Framework (Code-First), and an In-Memory Database.
All business logic is fully handled inside the Service Layer, ensuring clean separation of concerns.
Controllers contain zero business logic.
## Features
- Add / Edit / Delete / List Authors
- Add / Edit / Delete / List Books
- Borrow , Return and List Books Transaction 
- Dynamic Book status update with jQuery
- Filtering books with (status, dates)
## Technologies
- ASP.NET Core MVC
- C#
- Entity Framework Core (In-Memory Database)
- LINQ
- N-Tier Architecture
- AutoMapper
- Bootstrap
- jQuery
## Project Structure
```
LibraryManagementSystem/
├── LibraryManagementSystem.PL/        Presentation Layer (Controllers + Views + ViewModels)
├── LibraryManagementSystem.BLL/       Business Logic Layer (Services + Interfaces)
└── LibraryManagementSystem.DAL/       Data Access Layer (DbContext + Seed Data + Models)
```
## How to Run the Project
1. Clone the repository 
  ``` git clone https://github.com/Osman-Saad/CodeZone.LibraryManagementSystem.git ```
2. Open in Visual Studio
  Open the .sln file.
3. Run the application
   - Press F5
   - Or run using CLI: ``` dotnet run ```
The system loads with full seeded data.
## Developed for CodeZone LLC
Website: [CodeZone](https://www.codezone-eg.com/)
## Developer
Osman Saad  
Android, KMP & .NET Developer
