

About web application Insurance_ASP

1. Clone this GitHub repository containing project Insurance_ASP to your local computer.

2. Open solution Project.sln in Visual Studio (developed in VS 2022).

3. Migrate application database to your local MSSQLLocalDb server:
    - Go to Menu Tools -> NuGet Package Manager -> Package Manager Console
    - In Package Manager Console window type these 2 commands:
      PM> Add-Migration Migrate_Database
      PM> Update-Database

4. Press F5 to launch the application.

5. To examine full application functionality login as Admin:
   User name: admin@email.cz
   Password:  Admin123

6. You can also login as one of following common users, with limited functionality:
   User name                    Password
   jan.novak@email.cz           Jan.novak0
   petr.benda@email.cz          Petr.benda0
   frantisek.fiala@email.cz     Frantisek.fiala0
   kvetoslav.zapadak@email.cz   Kvetoslav.zapadak0
    