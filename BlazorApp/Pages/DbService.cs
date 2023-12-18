// DbService.cs
using BlazorApp.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public class DbService
{
    private readonly string _connectionString;

    public DbService(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
    }

    public async Task AddHardwareAsync(Hardware hardware)
    {
        if (hardware == null)
        {
            throw new ArgumentNullException(nameof(hardware), "Hardware object cannot be null.");
        }

        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO Hardware (Name, Description, Price, SellerId) VALUES (@Name, @Description, @Price, @SellerId)";

                    cmd.Parameters.AddWithValue("@Name", (object)hardware.Name ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", (object)hardware.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price", hardware.Price);
                    cmd.Parameters.AddWithValue("@SellerId", hardware.SellerId);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception, e.g., log it or display an error message
            Console.WriteLine($"Error adding hardware: {ex.Message}");
        }
    }

    public async Task<List<Hardware>> GetAllHardwareAsync()
    {
        List<Hardware> hardwareList = new List<Hardware>();

        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM Hardware";

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            try
                            {
                                Hardware hardware = new Hardware
                                {
                                    HardwareId = reader.GetInt32(reader.GetOrdinal("HardwareId")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                    SellerId = reader.GetInt32(reader.GetOrdinal("SellerId"))
                                };

                                hardware.EnsureNotNullValues();
                                hardwareList.Add(hardware);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error creating Hardware object: {ex.Message}");
                                Console.WriteLine($"Problematic data - HardwareId: {reader["HardwareId"]}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving hardware: {ex.Message}");
        }

        return hardwareList;
    }
}
