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
                    cmd.CommandText = "INSERT INTO Hardware (Name, Price, SellerId) VALUES (@Name, @Price, @SellerId)";



                    cmd.Parameters.AddWithValue("@Type", (object)hardware.Type ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Name", (object)hardware.Name ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@AttackSurface", (object)hardware.AttackSurface ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@State", (object)hardware.State ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Version", hardware.Version);
                    cmd.Parameters.AddWithValue("@Description", (object)hardware.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Price", hardware.Price);
                    cmd.Parameters.AddWithValue("@SellerId", hardware.SellerId);
                    cmd.Parameters.AddWithValue("@SellerName", (object)hardware.SellerName ?? DBNull.Value);

                    Console.WriteLine(hardware.Version);


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
                                    Type = reader["Type"] == DBNull.Value ? null : reader.GetString(reader.GetOrdinal("Type")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    AttackSurface = reader.IsDBNull(reader.GetOrdinal("AttackSurface")) ? null : reader.GetString(reader.GetOrdinal("AttackSurface")),
                                    State = reader.IsDBNull(reader.GetOrdinal("State")) ? null : reader.GetString(reader.GetOrdinal("State")),
                                    Version = reader.IsDBNull(reader.GetOrdinal("Version")) ? null : (double?)reader.GetDecimal(reader.GetOrdinal("Version")),
                                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString(reader.GetOrdinal("Description")),
                                    Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                    SellerId = reader.GetInt32(reader.GetOrdinal("SellerId")),
                                    SellerName = reader.IsDBNull(reader.GetOrdinal("SellerName")) ? null : reader.GetString(reader.GetOrdinal("SellerName"))
                                };

                                hardware.EnsureNotNullValues();
                                hardwareList.Add(hardware);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error creating Hardware object: {ex.Message}");
                                Console.WriteLine($"Problematic data - Type: {reader["Type"]}, HardwareId: {reader["HardwareId"]}");
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