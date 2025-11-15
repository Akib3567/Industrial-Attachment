
-- user table
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,      -- Unique ID for the user (Admin/Student)
    StudentId INT UNIQUE NOT NULL,     -- CUET Student ID
    Name NVARCHAR(100) NOT NULL,                -- Student's full name
    Email NVARCHAR(255) UNIQUE NOT NULL,        -- Student's email address
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(10) NOT NULL DEFAULT 'Student'
);



-- Bus table 
CREATE TABLE Buses (
    BusId INT IDENTITY(1,1) PRIMARY KEY,      -- Unique ID for the bus
    BusName NVARCHAR(100) NOT NULL,            -- Name of the bus (Padma, Jamuna, etc.)
    TotalSeats INT NOT NULL,                   -- Total number of seats on the bus
    ScheduleTime TIME NOT NULL,                -- Time for the bus to operate
    Route NVARCHAR(100) NOT NULL,              -- Route (Campus to City, City to Campus)
    CreatedAt DATETIME DEFAULT GETDATE()       -- Timestamp for when the bus was added
);




-- Booking table 
CREATE TABLE Bookings (
    BookingId INT IDENTITY(1,1) PRIMARY KEY,   -- Unique booking ID
    UserId INT NOT NULL,                        -- Foreign key to Users table
    BusId INT NOT NULL,                         -- Foreign key to Buses table
    SeatNumber INT NOT NULL,                    -- Seat number booked by the user
    BookingDate DATETIME DEFAULT GETDATE(),     -- Date and time of the booking
    FOREIGN KEY (UserId) REFERENCES Users(UserId), -- Link to the Users table
    FOREIGN KEY (BusId) REFERENCES Buses(BusId)   -- Link to the Buses table
);
