using BlazorApp.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

public class DatabaseManager
{
    // Forbindelsesstrengen til databasen
    string connectionString = "Server=(localdb)\\Local;Database=master;Integrated Security=True;";


    // Metode til at hente alle sælgere fra databasen
    public List<Seller> GetSellers()
    {
        List<Seller> sellers = new List<Seller>();

        // Opretter en forbindelse til databasen
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();  // Åbner forbindelsen

            // SQL-kommando for at vælge alle rækker fra Seller-tabellen
            using (SqlCommand command = new SqlCommand("SELECT * FROM Seller", connection))
            using (SqlDataReader reader = command.ExecuteReader())  // Udfører kommandoen og returnerer en SqlDataReader
            {
                // Læser resultaterne og opretter Seller-objekter
                while (reader.Read())
                {
                    Seller seller = new Seller
                    {
                        SellerId = (int)reader["SellerId"],
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString()
                    };

                    sellers.Add(seller);  // Tilføjer hvert Seller-objekt til listen
                }
            }
        }

        return sellers;  // Returnerer listen med sælgere
    }

    // Metode til at hente alle hardwareenheder fra databasen
    public List<Hardware> GetHardware()
    {
        List<Hardware> hardwareList = new List<Hardware>();

        // Opretter en forbindelse til databasen
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();  // Åbner forbindelsen

            // SQL-kommando for at vælge alle rækker fra Hardware-tabellen
            using (SqlCommand command = new SqlCommand("SELECT * FROM Hardware", connection))
            using (SqlDataReader reader = command.ExecuteReader())  // Udfører kommandoen og returnerer en SqlDataReader
            {
                // Læser resultaterne og opretter Hardware-objekter
                while (reader.Read())
                {
                    Hardware hardware = new Hardware
                    {
                        HardwareId = (int)reader["HardwareId"],
                        Name = reader["Name"].ToString(),
                        Price = (decimal)reader["Price"],
                        SellerId = (int)reader["SellerId"]
                    };

                    hardwareList.Add(hardware);  // Tilføjer hver hardwareenhed til listen
                }
            }
        }

        return hardwareList;  // Returnerer listen med hardwareenheder
    }

    // Metode til at tilføje en ny sælger til databasen
    public void AddSeller(Seller seller)
    {
        // Opretter en forbindelse til databasen
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();  // Åbner forbindelsen

            // SQL-kommando for at indsætte data i Seller-tabellen
            using (SqlCommand command = new SqlCommand("INSERT INTO Seller (Name, Email) VALUES (@Name, @Email)", connection))
            {
                command.Parameters.AddWithValue("@Name", seller.Name);
                command.Parameters.AddWithValue("@Email", seller.Email);

                command.ExecuteNonQuery();  // Udfører kommandoen uden at returnere data
            }
        }
    }

    // Metode til at tilføje en ny hardwareenhed til databasen
    public void AddHardware(Hardware hardware)
    {
        // Opretter en forbindelse til databasen
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();  // Åbner forbindelsen

            // SQL-kommando for at indsætte data i Hardware-tabellen
            using (SqlCommand command = new SqlCommand("INSERT INTO Hardware (Name, Price, SellerId) VALUES (@Name, @Price, @SellerId)", connection))
            {
                command.Parameters.AddWithValue("@Name", hardware.Name);
                command.Parameters.AddWithValue("@Price", hardware.Price);
                command.Parameters.AddWithValue("@SellerId", hardware.SellerId);

                command.ExecuteNonQuery();  // Udfører kommandoen uden at returnere data
            }
        }
    }

    // Metode til at fjerne en sælger baseret på sælgerens ID
    public void RemoveSeller(int sellerId)
    {
        // Opretter en forbindelse til databasen
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();  // Åbner forbindelsen

            // SQL-kommando for at slette en sælger baseret på SellerId
            using (SqlCommand command = new SqlCommand("DELETE FROM Seller WHERE SellerId = @SellerId", connection))
            {
                command.Parameters.AddWithValue("@SellerId", sellerId);

                command.ExecuteNonQuery();  // Udfører kommandoen uden at returnere data
            }
        }
    }

    // Metode til at fjerne en hardwareenhed baseret på hardwareenhedens ID
    public void RemoveHardware(int hardwareId)
    {
        // Opretter en forbindelse til databasen
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();  // Åbner forbindelsen

            // SQL-kommando for at slette en hardwareenhed baseret på HardwareId
            using (SqlCommand command = new SqlCommand("DELETE FROM Hardware WHERE HardwareId = @HardwareId", connection))
            {
                command.Parameters.AddWithValue("@HardwareId", hardwareId);

                command.ExecuteNonQuery();  // Udfører kommandoen uden at returnere data
            }
        }
    }

    // Implementér andre CRUD-operationer efter behov (Opdater, Slet)
}
