IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CuetTransportDB')
BEGIN
    CREATE DATABASE CuetTransportDB;
END
GO

USE CuetTransportDB;
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users' AND type = 'U')
BEGIN
    CREATE TABLE Users (
        UserId INT IDENTITY(1,1) PRIMARY KEY,
        StudentId INT UNIQUE NOT NULL,
        Name NVARCHAR(100) NOT NULL,
        Email NVARCHAR(255) UNIQUE NOT NULL,
        Password NVARCHAR(255) NOT NULL,
        Role NVARCHAR(10) NOT NULL DEFAULT 'Student'
    );
    PRINT 'Users table created successfully.';
END
ELSE
BEGIN
    PRINT 'Users table already exists.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Buses' AND type = 'U')
BEGIN
    CREATE TABLE Buses (
        BusId INT IDENTITY(1,1) PRIMARY KEY,
        BusName NVARCHAR(100) NOT NULL,
        TotalSeats INT NOT NULL,
        ScheduleTime NVARCHAR(100) NOT NULL
    );
    PRINT 'Buses table created successfully.';
END
ELSE
BEGIN
    PRINT 'Buses table already exists.';
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Bookings' AND type = 'U')
BEGIN
    CREATE TABLE Bookings (
        BookingId INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        BusId INT NOT NULL,
        SeatNumber INT NOT NULL,
        BookingDate DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (UserId) REFERENCES Users(UserId),
        FOREIGN KEY (BusId) REFERENCES Buses(BusId)
    );
    PRINT 'Bookings table created successfully.';
END
ELSE
BEGIN
    PRINT 'Bookings table already exists.';
END
GO

PRINT 'Database setup completed successfully!';