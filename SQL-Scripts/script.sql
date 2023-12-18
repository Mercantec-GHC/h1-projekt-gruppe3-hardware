CREATE TABLE Seller (
    SellerId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
);

CREATE TABLE Hardware (
    HardwareId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX), 
    Price DECIMAL(18,2) NOT NULL,
    SellerId INT,
    Category NVARCHAR(50), 
    FOREIGN KEY (SellerId) REFERENCES Seller(SellerId)
);


