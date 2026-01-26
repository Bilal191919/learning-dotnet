using Microsoft.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        // Aapka SQL Server ka address (Connection String)
        string connectionString = "Server=localhost;Database=LearningDB;Trusted_Connection=True;TrustServerCertificate=True;";

        Console.WriteLine("=== Stage 2: Database Connectivity Test ===");

        try 
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Connection kholna
                connection.Open();
                Console.WriteLine("\n[SUCCESS] SQL Server se rabta qaaim ho gaya hai!");

                // Data nikalne ki query
                string sql = "SELECT Name, Email FROM Users";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("\nTable 'Users' ka data:");
                    while (reader.Read())
                    {
                        // Jo data aapne SSMS mein dala tha, wo yahan show hoga
                        Console.WriteLine($"- Name: {reader["Name"]} | Email: {reader["Email"]}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("\n[ERROR] Masla aa gaya: " + ex.Message);
        }

        Console.WriteLine("\nBand karne ke liye koi bhi key dabayein...");
        Console.ReadKey();
    }
}
