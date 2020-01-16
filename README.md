# c4pe

# Feedback API

## Installation

To install and deploy the API you need the `.NET Core SDK 3.1` and `MariaDB 10.4`.

Create a user `feedback` with a password of your choice:

`CREATE USER 'feedback'@'localhost' IDENTIFIED BY '<password>';`

Create a database 'FeedbackDB':

`CREATE DATABASE FeedbackDB;`

Grant the user feedback all permissions on the feedback database:

`GRANT ALL PRIVILEGES ON FeedbackDB.* TO 'feedback'@'localhost';`

Additionally you need to save the MariaDB connection string in the environment variable `FeedbackDB_ConnectionString`:

`Server=localhost;Database=FeedbackDB;Uid=feedback;Pwd=<password>;`

Now open the project in Visual Studio 2019 and open the `Package Manager Console` (View -> Other Windows). 
Here you can use the command `Update-Database` and your database should be filled with the default data now.

