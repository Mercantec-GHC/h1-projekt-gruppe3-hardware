-- Opret tabel for produkter
CREATE TABLE Products
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Type NVARCHAR(50) NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    AttackSurface NVARCHAR(255),
    State NVARCHAR(255) NOT NULL,
    Version FLOAT,
    Description NVARCHAR(MAX) NOT NULL,
    Price DECIMAL(18,2) NOT NULL, 
);

-- Opret tabel for brugere
CREATE TABLE Users
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL, 
    PhoneNumber NVARCHAR(20) NOT NULL, 
    Email NVARCHAR(255) NOT NULL, 
    Trusted BIT,
    ProductId INT FOREIGN KEY REFERENCES Products(Id)
);

CREATE TABLE Seller
(
  SellerID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Trusted BIT NOT NULL
)