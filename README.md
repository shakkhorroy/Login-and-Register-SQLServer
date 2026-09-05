# Login and Register System - SQL Server

## About the Project

This is a C# Windows Forms application that provides Login, Registration, and Logout functionality. The original project used Microsoft Access, but I changed the database connection to Microsoft SQL Server.

The application contains three main forms:
- Login
- Registration
- Dashboard

## Database

The application uses SQL Server with a database named `db_users`.

The database contains a table called `tbl_users` with the following fields:

- `id` - Primary key and auto-increment
- `username` - User's username
- `password` - User's password

The SQL database script is included in this repository as:

`database.sql`

## How to Run

### 1. Create the Database

Open SQL Server Management Studio and run the `database.sql` script.

The script creates:

- `db_users` database
- `tbl_users` table
- Default test account

### 2. Test Login

Use the following account:

Username:
`admin`

Password:
`admin123`

### 3. Configure the Connection

The database connection string is stored in `App.config`.

My SQL Server uses:

`localhost`

If the SQL Server name is different on another computer, change the `Data Source` value in `App.config`.

## Changes I Made

### 1. Changed OleDb to SqlClient

The original application used:

`System.Data.OleDb`

I replaced it with:

`System.Data.SqlClient`

I made this change because the original application used a Microsoft Access database. Access is older technology and can cause compatibility problems on different computers. SQL Server is more suitable for this project.

### 2. Changed the Login System

The login form now connects to SQL Server using `SqlConnection` and `SqlCommand`.

The username and password are checked against the `tbl_users` table.

### 3. Changed the Registration System

The registration form now inserts new users into the SQL Server `tbl_users` table.

It also checks that:
- Username and password fields are not empty.
- Password and confirm password are the same.
- The username is not already taken.

### 4. Connection String in App.config

I stored the database connection string in `App.config` instead of writing it directly inside each form.

This makes the application easier to manage because the database connection information is kept in one place. If the SQL Server name changes, I only need to update the connection string instead of changing multiple forms.

The application reads the connection string using `ConfigurationManager`.

### 5. @username and @password

I used `@username` and `@password` as SQL parameters.

They allow the user's input to be passed safely to the SQL query instead of directly joining user input with the SQL statement.

This also helps protect the application from SQL injection.

### 6. Logout

I changed the Logout button so that it returns to the Login form instead of closing the entire application.

The Dashboard closes after the Login form is opened.

## Technologies Used

- C#
- Windows Forms
- .NET Framework
- Microsoft SQL Server
- SQL Server Management Studio
- Visual Studio

## Project Files

- `frmLogin.cs` - Handles user login
- `frmRegister.cs` - Handles new user registration
- `frmDashboard.cs` - Dashboard and logout functionality
- `Program.cs` - Starts the application with the Login form
- `App.config` - Stores the SQL Server connection string
- `database.sql` - Creates the database, table, and test user

## Test Results

The application was tested for:

1. Login with `admin / admin123`
2. Wrong password rejection
3. New user registration
4. Login with the newly registered user
5. Logout and return to the Login screen

All required functions were tested successfully.
