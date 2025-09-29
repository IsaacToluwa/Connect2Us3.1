-- Create additional tables for entities referenced in DbContext but not in initial migration

CREATE TABLE Rentals (
    RentalId INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(128),
    BookId INT NOT NULL,
    RentalDate DATETIME NOT NULL,
    DueDate DATETIME NOT NULL,
    ReturnDate DATETIME,
    Status NVARCHAR(MAX) NOT NULL,
    FOREIGN KEY (BookId) REFERENCES Books(BookId),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id)
);

CREATE INDEX IX_UserId ON Rentals(UserId);
CREATE INDEX IX_BookId ON Rentals(BookId);

CREATE TABLE Reservations (
    ReservationId INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(128),
    BookId INT NOT NULL,
    ReservationDate DATETIME NOT NULL,
    ExpiryDate DATETIME NOT NULL,
    Status NVARCHAR(MAX) NOT NULL,
    FOREIGN KEY (BookId) REFERENCES Books(BookId),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id)
);

CREATE INDEX IX_UserId ON Reservations(UserId);
CREATE INDEX IX_BookId ON Reservations(BookId);

CREATE TABLE Payments (
    PaymentId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    PaymentDate DATETIME NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    PaymentMethod NVARCHAR(MAX) NOT NULL,
    Status NVARCHAR(MAX) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId)
);

CREATE INDEX IX_OrderId ON Payments(OrderId);

CREATE TABLE Notifications (
    NotificationId INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(128),
    Message NVARCHAR(MAX) NOT NULL,
    Type NVARCHAR(MAX) NOT NULL,
    IsRead BIT NOT NULL DEFAULT 0,
    CreatedDate DATETIME NOT NULL,
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id)
);

CREATE INDEX IX_UserId ON Notifications(UserId);