-- Create tables for Connect2Us3_01 database

CREATE TABLE Authors (
    AuthorId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Biography NVARCHAR(MAX)
);

CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE AspNetUsers (
    Id NVARCHAR(128) PRIMARY KEY,
    Email NVARCHAR(256),
    EmailConfirmed BIT NOT NULL DEFAULT 0,
    PasswordHash NVARCHAR(MAX),
    SecurityStamp NVARCHAR(MAX),
    PhoneNumber NVARCHAR(MAX),
    PhoneNumberConfirmed BIT NOT NULL DEFAULT 0,
    TwoFactorEnabled BIT NOT NULL DEFAULT 0,
    LockoutEndDateUtc DATETIME,
    LockoutEnabled BIT NOT NULL DEFAULT 1,
    AccessFailedCount INT NOT NULL DEFAULT 0,
    UserName NVARCHAR(256) NOT NULL
);

CREATE UNIQUE INDEX UserNameIndex ON AspNetUsers(UserName);

CREATE TABLE AspNetRoles (
    Id NVARCHAR(128) PRIMARY KEY,
    Name NVARCHAR(256) NOT NULL
);

CREATE TABLE Books (
    BookId INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    ISBN NVARCHAR(20),
    Price DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL,
    CategoryId INT NOT NULL,
    AuthorId INT NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId) ON DELETE CASCADE,
    FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId) ON DELETE CASCADE
);

CREATE INDEX IX_CategoryId ON Books(CategoryId);
CREATE INDEX IX_AuthorId ON Books(AuthorId);

CREATE TABLE Reviews (
    ReviewId INT IDENTITY(1,1) PRIMARY KEY,
    BookId INT NOT NULL,
    UserId NVARCHAR(128),
    Rating INT NOT NULL,
    Comment NVARCHAR(MAX),
    ReviewDate DATETIME NOT NULL,
    FOREIGN KEY (BookId) REFERENCES Books(BookId) ON DELETE CASCADE,
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id)
);

CREATE INDEX IX_BookId ON Reviews(BookId);
CREATE INDEX IX_UserId ON Reviews(UserId);

CREATE TABLE AspNetUserClaims (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(128) NOT NULL,
    ClaimType NVARCHAR(MAX),
    ClaimValue NVARCHAR(MAX),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

CREATE INDEX IX_UserId ON AspNetUserClaims(UserId);

CREATE TABLE AspNetUserLogins (
    LoginProvider NVARCHAR(128) NOT NULL,
    ProviderKey NVARCHAR(128) NOT NULL,
    UserId NVARCHAR(128) NOT NULL,
    PRIMARY KEY (LoginProvider, ProviderKey, UserId),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
);

CREATE INDEX IX_UserId ON AspNetUserLogins(UserId);

CREATE TABLE AspNetUserRoles (
    UserId NVARCHAR(128) NOT NULL,
    RoleId NVARCHAR(128) NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE,
    FOREIGN KEY (RoleId) REFERENCES AspNetRoles(Id) ON DELETE CASCADE
);

CREATE INDEX IX_UserId ON AspNetUserRoles(UserId);
CREATE INDEX IX_RoleId ON AspNetUserRoles(RoleId);

CREATE TABLE Wishlists (
    WishlistId INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(128),
    BookId INT NOT NULL,
    FOREIGN KEY (BookId) REFERENCES Books(BookId) ON DELETE CASCADE,
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id)
);

CREATE INDEX IX_UserId ON Wishlists(UserId);
CREATE INDEX IX_BookId ON Wishlists(BookId);

CREATE TABLE ShoppingCarts (
    ShoppingCartId INT IDENTITY(1,1) PRIMARY KEY
);

CREATE TABLE CartItems (
    CartItemId INT IDENTITY(1,1) PRIMARY KEY,
    BookId INT NOT NULL,
    Quantity INT NOT NULL,
    ShoppingCart_ShoppingCartId INT,
    FOREIGN KEY (BookId) REFERENCES Books(BookId) ON DELETE CASCADE,
    FOREIGN KEY (ShoppingCart_ShoppingCartId) REFERENCES ShoppingCarts(ShoppingCartId)
);

CREATE INDEX IX_BookId ON CartItems(BookId);
CREATE INDEX IX_ShoppingCart_ShoppingCartId ON CartItems(ShoppingCart_ShoppingCartId);

CREATE TABLE Orders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    UserId NVARCHAR(128),
    OrderDate DATETIME NOT NULL,
    TotalAmount DECIMAL(18,2) NOT NULL,
    Status NVARCHAR(MAX) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id)
);

CREATE INDEX IX_UserId ON Orders(UserId);

CREATE TABLE OrderDetails (
    OrderDetailId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    BookId INT NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId) ON DELETE CASCADE,
    FOREIGN KEY (BookId) REFERENCES Books(BookId)
);

CREATE INDEX IX_OrderId ON OrderDetails(OrderId);
CREATE INDEX IX_BookId ON OrderDetails(BookId);

CREATE TABLE Deliveries (
    DeliveryId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    DriverId NVARCHAR(128),
    Status NVARCHAR(MAX) NOT NULL,
    EstimatedDeliveryDate DATETIME NOT NULL,
    ActualDeliveryDate DATETIME,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId) ON DELETE CASCADE,
    FOREIGN KEY (DriverId) REFERENCES AspNetUsers(Id)
);

CREATE INDEX IX_OrderId ON Deliveries(OrderId);
CREATE INDEX IX_DriverId ON Deliveries(DriverId);