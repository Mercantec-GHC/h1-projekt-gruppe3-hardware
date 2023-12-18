using BlazorApp.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

public class DatabaseManager
{
    string connectionString = "Server=(localdb)\\Local;Database=master;Integrated Security=True;";

    public List<Seller> GetSellers()
    {
        return GetEntities<Seller>("SELECT * FROM Seller");
    }

    public List<Hardware> GetHardware()
    {
        return GetEntities<Hardware>("SELECT * FROM Hardware");
    }

    public List<Seller> GetSellersByName(string name)
    {
        return GetEntities<Seller>("SELECT * FROM Seller WHERE Name LIKE @Name", ("@Name", $"%{name}%"));
    }

    public List<Hardware> GetHardwareByName(string name)
    {
        return GetEntities<Hardware>("SELECT * FROM Hardware WHERE Name LIKE @Name", ("@Name", $"%{name}%"));
    }

    public void AddSeller(Seller seller)
    {
        ExecuteNonQuery("INSERT INTO Seller (Name, Email) VALUES (@Name, @Email)",
            ("@Name", seller.Name), ("@Email", seller.Email));
    }

    public void AddHardware(Hardware hardware)
    {
        ExecuteNonQuery("INSERT INTO Hardware (Name, Description, Price, SellerId) VALUES (@Name, @Description, @Price, @SellerId)",
           ("@Name", hardware.Name),
           ("@Description", (object)hardware.Description ?? DBNull.Value),
           ("@Price", hardware.Price),
           ("@SellerId", hardware.SellerId));
    }

    public void RemoveSeller(int sellerId)
    {
        ExecuteNonQuery("DELETE FROM Seller WHERE SellerId = @SellerId", ("@SellerId", sellerId));
    }

    public void RemoveHardware(int hardwareId)
    {
        ExecuteNonQuery("DELETE FROM Hardware WHERE HardwareId = @HardwareId", ("@HardwareId", hardwareId));
    }

    private List<T> GetEntities<T>(string query, params (string, object)[] parameters)
    {
        List<T> entities = new List<T>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Set command type as text (Optional)
                command.CommandType = CommandType.Text;

                foreach (var (paramName, paramValue) in parameters)
                {
                    command.Parameters.AddWithValue(paramName, paramValue ?? DBNull.Value);
                }

                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T entity = Activator.CreateInstance<T>();

                            foreach (var property in typeof(T).GetProperties())
                            {
                                string columnName = property.Name;

                                try
                                {
                                    int columnIndex = reader.GetOrdinal(columnName);

                                    if (!reader.IsDBNull(columnIndex))
                                    {
                                        if (property.PropertyType == typeof(int))
                                        {
                                            property.SetValue(entity, Convert.ToInt32(reader[columnIndex]));
                                        }
                                        else if (property.PropertyType == typeof(decimal))
                                        {
                                            property.SetValue(entity, Convert.ToDecimal(reader[columnIndex]));
                                        }
                                        else if (property.PropertyType == typeof(byte))
                                        {
                                            property.SetValue(entity, Convert.ToByte(reader[columnIndex]));
                                        }
                                        else
                                        {
                                            property.SetValue(entity, reader[columnIndex]?.ToString());
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    // Log or print error details for troubleshooting
                                    Console.WriteLine($"Error setting property {property.Name} from column {columnName}: {ex.Message}");
                                }
                            }

                            entities.Add(entity);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log or print error details for troubleshooting
                    Console.WriteLine($"Error executing reader: {ex.Message}");
                }
            }
        }

        return entities;
    }

    private void ExecuteNonQuery(string query, params (string, object)[] parameters)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                foreach (var (paramName, paramValue) in parameters)
                {
                    command.Parameters.AddWithValue(paramName, paramValue ?? DBNull.Value);
                }

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Log or print error details for troubleshooting
                    Console.WriteLine($"Error executing non-query: {ex.Message}");
                }
            }
        }
    }
}
