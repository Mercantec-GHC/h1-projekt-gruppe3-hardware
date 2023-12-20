using Microsoft.Data.SqlClient;
using System.Data;

// Brugermodel klasse
public class UserModel
{
    // Brugerens ID
    public int UserId { get; set; }

    // Brugerens brugernavn
    public string UserName { get; set; }

    // Brugerens email-adresse
    public string Email { get; set; }
}


// Brugertjeneste
public class UserService
{
    // Forbindelsesstreng til databasen
    string connectionString = "Server=(localdb)\\Local;Database=master;Integrated Security=True;";

    // Metode til at hente brugere fra databasen
    public List<UserModel> GetUsers()
    {
        // Liste til at gemme brugerobjekter
        List<UserModel> users = new List<UserModel>();

        // Opretter forbindelse til databasen
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            // SQL-forespørgsel for at hente alle brugere
            string query = "SELECT * FROM Users";

            // Bruger en SqlDataAdapter til at udføre forespørgslen og fylde data i en DataTable
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Gennemgår hver række i DataTable og opretter et UserModel-objekt for hver bruger
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

        // Returnerer listen af brugerobjekter
        return users;
    }
}
