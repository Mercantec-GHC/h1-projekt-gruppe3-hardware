using BlazorApp.Data;
using Microsoft.Data.SqlClient;
using System.Data;

public class DatabaseManager
{
    private string connectionString = "Server=(localdb)\\local;Database=master;Integrated Security=True;";

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
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    // Step 1: Insert seller information into Seller table
                    ExecuteNonQuery(
                        "INSERT INTO Seller (Name, Email) VALUES (@Name, @Email); SELECT SCOPE_IDENTITY()",
                        ("@Name", seller.Name),
                        ("@Email", seller.Email)
                    );

                    // Step 2: Retrieve the newly inserted SellerId
                    int sellerId = Convert.ToInt32(ExecuteScalar("SELECT CAST(SCOPE_IDENTITY() AS INT)"));

                    // Step 3: If there is associated product information, insert it into Hardware table
                    if (seller.Product != null)
                    {
                        ExecuteNonQuery(
                            "INSERT INTO Hardware (Name, Description, Price, SellerId) VALUES (@Name, @Description, @Price, @SellerId)",
                            ("@Name", seller.Product.Name),
                            ("@Description", seller.Product.Description),
                            ("@Price", seller.Product.Price),
                            ("@SellerId", sellerId)
                        );
                    }

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Log or handle exceptions
                    Console.WriteLine($"Error adding seller: {ex.Message}");

                    // Rollback the transaction in case of an exception
                    transaction.Rollback();
                }
            }
        }
    }

    public void AddHardware(Hardware hardware)
    {
        ExecuteNonQuery(
            "INSERT INTO Hardware (Name, Description, Price, SellerId) VALUES (@Name, @Description, @Price, @SellerId)",
            ("@Name", hardware.Name),
            ("@Description", hardware.Description),
            ("@Price", hardware.Price),
            ("@SellerId", hardware.SellerId)
        );
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
                command.CommandType = CommandType.Text;

                foreach (var (paramName, paramValue) in parameters)
                {
                    command.Parameters.AddWithValue(paramName, paramValue ?? DBNull.Value);
                }

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
                                if (property.PropertyType == typeof(int))
                                {
                                    if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
                                    {
                                        property.SetValue(entity, Convert.ToInt32(reader[columnName]));
                                    }
                                }
                                else if (property.PropertyType == typeof(decimal))
                                {
                                    if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
                                    {
                                        property.SetValue(entity, Convert.ToDecimal(reader[columnName]));
                                    }
                                }
                                else if (property.PropertyType == typeof(byte))
                                {
                                    if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
                                    {
                                        property.SetValue(entity, Convert.ToByte(reader[columnName]));
                                    }
                                }
                                else
                                {
                                    property.SetValue(entity, reader[columnName]?.ToString());
                                }
                            }
                            catch (IndexOutOfRangeException ex)
                            {
                                // Log or print error details for troubleshooting
                                Console.WriteLine($"Error setting property {property.Name} from column {columnName}: {ex.Message}");
                            }
                        }

                        entities.Add(entity);
                    }
                }
            }
        }

        return entities;
    }

    private void ExecuteNonQuery(string query, params (string, object)[] parameters)
    {
        Console.WriteLine($"Executing query: {query}");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                foreach (var (paramName, paramValue) in parameters)
                {
                    command.Parameters.AddWithValue(paramName, paramValue ?? DBNull.Value);
                }

                command.ExecuteNonQuery();
            }
        }
    }

    private object ExecuteScalar(string query)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                return command.ExecuteScalar();
            }
        }
    }
}
