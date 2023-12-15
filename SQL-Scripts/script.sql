CREATE TABLE Seller (
    SellerId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
	PhoneNumber INT NOT NULL
);

CREATE TABLE Hardware (
    HardwareId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Condition NVARCHAR(50),
    Description NVARCHAR(MAX),
    Price DECIMAL(18,2) NOT NULL,
    SellerId INT,
    Category NVARCHAR(50) NOT NULL, -- Added Category column
    FOREIGN KEY (SellerId) REFERENCES Seller(SellerId)
);
