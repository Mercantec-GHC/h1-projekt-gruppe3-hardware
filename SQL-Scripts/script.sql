--@block Her kan i skrive SQL scripts til at oprette jeres DB

CREATE TABLE [dbo].[Seller]
(
	SellerID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Trusted BIT NOT NULL
)

CREATE TABLE [dbo].[Hardware]
(
	HardwareID INT PRIMARY KEY IDENTITY(1,1),
    Type NVARCHAR(255) NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    AttackSurface NVARCHAR(255),
    State NVARCHAR(255) NOT NULL,
    Version DECIMAL(5,2) NOT NULL,
    Description NVARCHAR(255),
    Price DECIMAL(10,2) NOT NULL,
    SellerID INT,
    FOREIGN KEY (SellerID) REFERENCES Seller(SellerID)
)

CREATE TABLE [dbo].[ShoppingCartItem]
(
	ShoppingCartItemID INT PRIMARY KEY IDENTITY(1,1),
    SellerID INT,
    HardwareID INT,
    FOREIGN KEY (SellerID) REFERENCES Seller(SellerID),
    FOREIGN KEY (HardwareID) REFERENCES Hardware(HardwareID)
)
