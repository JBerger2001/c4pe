# c4pe

# Feedback API

## Installation

To install and deploy the API you need the `.NET Core SDK 3.1` and `MariaDB 10.4`.

Open the mysql shell and create a user `feedback` with a password of your choice:

`CREATE USER 'feedback'@'localhost' IDENTIFIED BY '<password>';`

Grant the user 'feedback' all permissions on the feedback database:

`GRANT ALL PRIVILEGES ON FeedbackDB.* TO 'feedback'@'localhost';`

This is how your `appsettings.json` should look like. If it doesn't exist yet, create it and copy the JSON below. You need to replace the placeholders marked with `<...>`, e.g. with your secret JWT token and your database user password.

```json
{
  "AppSettings": {
    "Token": "<secret token>"
  },
  "ConnectionStrings": {
    "FeedbackDB": "Server=localhost;Database=FeedbackDB;Uid=feedback;Pwd=<password>;"
  },
  "SwaggerConfig": {
    "JsonRoute": "swagger/{documentName}/swagger.json",
    "Description": "Feedback API",
    "UIEndpoint": "v1/swagger.json"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

When the project is run it will create the database if it doesn't exist yet.
