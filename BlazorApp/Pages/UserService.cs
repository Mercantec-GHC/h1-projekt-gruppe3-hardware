// User model class
using Microsoft.Data.SqlClient;
using System.Data;

// User model class
public class UserModel
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}


// UserService
public class UserService
{
    private readonly string connectionString = "Data Source=(local);Initial Catalog=WebshopDB;Integrated Security=True";

    public List<UserModel> GetUsers()
    {
        List<UserModel> users = new List<UserModel>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM Users";
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    users.Add(new UserModel
                    {
                        UserId = Convert.ToInt32(row["UserId"]),
                        UserName = row["UserName"].ToString(),
                        Email = row["Email"].ToString()
                    });
                }
            }
        }

        return users;
    }
}
