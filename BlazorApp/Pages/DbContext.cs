using Microsoft.Data.SqlClient;
using System;
using System.Data;

public class DatabaseManager
{
    private readonly string connectionString = "Data Source=(local);Initial Catalog=WebshopDB;Integrated Security=True";

    public void AddUser(string userName, string email)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "INSERT INTO Users (UserName, Email) VALUES (@UserName, @Email)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Email", email);

                command.ExecuteNonQuery();
            }
        }
    }

    public void AddProduct(string productName, decimal price)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "INSERT INTO Products (ProductName, Price) VALUES (@ProductName, @Price)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ProductName", productName);
                command.Parameters.AddWithValue("@Price", price);

                command.ExecuteNonQuery();
            }
        }
    }

    public DataTable GetUsers()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Users";
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
        }
    }

    public DataTable GetProducts()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Products";
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
        }
    }

    // Implement methods for updating and deleting users and products if needed
}