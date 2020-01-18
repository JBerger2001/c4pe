# c4pe

# Feedback API

## Installation

To install and deploy the API you need the `.NET Core SDK 3.1` and `MariaDB 10.4`.

Open the mysql shell and create a user `feedback` with a password of your choice:

`CREATE USER 'feedback'@'localhost' IDENTIFIED BY '<password>';`

Grant the user 'feedback' all permissions on the feedback database:

`GRANT ALL PRIVILEGES ON FeedbackDB.* TO 'feedback'@'localhost';`

Additionally you need to save the MariaDB connection string in the environment variable `FeedbackDB_ConnectionString`:

`Server=localhost;Database=FeedbackDB;Uid=feedback;Pwd=<password>;`

When the project is run it will create the database if it doesn't exist yet.
