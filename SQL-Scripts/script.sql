-- Opret tabel for brugere
CREATE TABLE Users
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255),
    PhoneNumber NVARCHAR(20),
    Email NVARCHAR(255),
    Trusted BIT,
    ProductId INT FOREIGN KEY REFERENCES Products(Id)
);

-- Opret tabel for produkter
CREATE TABLE Products
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Type NVARCHAR(50),
    Name NVARCHAR(255),
    AttackSurface NVARCHAR(255),
    State NVARCHAR(255),
    Version FLOAT,
    Description NVARCHAR(MAX),
    Price DECIMAL(18,2)
);
