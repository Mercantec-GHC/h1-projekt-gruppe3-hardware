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

                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = hardware.Name as object ?? DBNull.Value;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = hardware.Description;
                    cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = hardware.Price;
                    cmd.Parameters.Add("@SellerId", SqlDbType.Int).Value = hardware.SellerId;

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
                            Hardware hardware = new Hardware
                            {
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Price = (decimal)reader["Price"],
                                SellerId = (int)reader["SellerId"]
                            };

                            hardwareList.Add(hardware);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Handle the exception, e.g., log it or display an error message
            Console.WriteLine($"Error retrieving hardware: {ex.Message}");
        }

        return hardwareList;
    }
}
