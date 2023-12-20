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
            throw new ArgumentNullException(nameof(hardware), "Hardware-objekt kan ikke være null.");
        }

        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand cmd = new SqlCommand())
                {
                    //Oprettelse af produkt
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
            Console.WriteLine($"Fejl ved tilføjelse af hardware: {ex.Message}");
        }
    }

    public async Task<List<Hardware>> GetAllHardwareAsync(decimal? minPrice = null, decimal? maxPrice = null, string sortBy = null)
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

                    string query = "SELECT * FROM Hardware WHERE 1=1";

                    if (minPrice.HasValue)
                    {
                        query += " AND Price >= @MinPrice";
                        cmd.Parameters.Add("@MinPrice", SqlDbType.Decimal).Value = minPrice.Value;
                    }
                    if (maxPrice.HasValue)
                    {
                        query += " AND Price <= @MaxPrice";
                        cmd.Parameters.Add("@MaxPrice", SqlDbType.Decimal).Value = maxPrice.Value;
                    }

                    cmd.CommandText = query + GetOrderByClause(sortBy);

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
            Console.WriteLine($"Fejl ved hentning af hardware: {ex.Message}");
        }

        return hardwareList;
    }

    public async Task<List<Hardware>> FilterHardwareAsync(decimal? minPrice, decimal? maxPrice)
    {
        return await GetAllHardwareAsync(minPrice, maxPrice, sortBy: null);
    }

    private string GetOrderByClause(string sortBy)
    {
        if (!string.IsNullOrEmpty(sortBy))
        {
            switch (sortBy.ToLower())
            {
                case "price":
                    return " ORDER BY Price";
                case "name":
                default:
                    return " ORDER BY Name";
            }
        }

        return string.Empty;
    }
}
