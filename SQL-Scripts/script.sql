CREATE TABLE Seller (
    SellerId INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
	PhoneNumber INT NOT NULL
);

CREATE TABLE Hardware (
    HardwareId INT PRIMARY KEY IDENTITY(1,1),
    Type NVARCHAR(50) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    AttackSurface NVARCHAR(100),
    State NVARCHAR(50),
    Version DECIMAL(18,2),
    Description NVARCHAR(MAX),
    Price DECIMAL(18,2) NOT NULL,
    SellerId INT,
    SellerName NVARCHAR(MAX),
    FOREIGN KEY (SellerId) REFERENCES Seller(SellerId)
);
