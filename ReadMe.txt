
###################################################################################################
About web application Insurance_ASP - Jaromír Køížek 2022-09
###################################################################################################

This application was created by Jaromír Køížek as a part of an educational course 
focused on the development of web applications using C# .NET ASP MVC core.

You can try and test the application in the following steps:

1. Clone this GitHub repository containing project Insurance_ASP to your local computer:
   If you use GitBash, create an empty folder, open Git console window and type command:
   git clone https://github.com/JaromirKrizek/ITnetwork_Project.git

2. When the repository is downloaded to your local folder,
   open solution Project.sln in Visual Studio (developed in VS 2022).

3. Migrate application database to your local MSSQLLocalDb server:
    - In Visual Studio go to Menu Tools -> NuGet Package Manager -> Package Manager Console.
    - In Package Manager Console window type these 2 commands:
      PM> Add-Migration Migrate_Database
      PM> Update-Database

4. In Visual Studio press F5 to launch the application.

5. When the application is running, login to the application as admin to 
   examine full application functionality:
   Email:     admin@email.cz
   Password:  Admin123

6. You can also login as one of following common users, with limited functionality:
   Email:                       Password:
   jan.novak@email.cz           Jan.novak0
   petr.benda@email.cz          Petr.benda0
   frantisek.fiala@email.cz     Frantisek.fiala0
   kvetoslav.zapadak@email.cz   Kvetoslav.zapadak0
    