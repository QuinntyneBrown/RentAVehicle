# goalsetter
Goalsetter Challenge

Create a back-end project for a car rental company. Use .NET Framework or .NET Core, C#, WebAPI for the HTTP layer, SQL Server and EF Code First for storage.

Requirements:
1. Add and remove vehicles.
2. Add and remove clients.
3. A removed vehicle should not be available for new rentals.
4. Create (and cancel) rentals, only with available vehicles for the selected date range.
5. Each vehicle must have a price per day that should be used to calculate the rental final charge.
6. Use the Entity Framework Seed mechanism to populate the database with some vehicles, clients and rentals as examples.
7. A rental should have at least the following information: client, date range, vehicle, price.

Notes:
Apply all the patterns and best practices that you would apply on a production-ready solution.
Provide a GitHub link to your solution. It should compile and run without any extra steps on any Windows 10 computer with VS2019 and SQL Server installed.