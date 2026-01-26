using Microsoft.Data.SqlClient;

// Establishing connection using the database credentials from Stage 2
string connectionString = "Server=localhost;Database=LearningDB;Trusted_Connection=True;TrustServerCertificate=True;";

bool isRunning = true;

while (isRunning)
{
	Console.WriteLine("\n======================================");
	Console.WriteLine("   STAGE 3: FULL DATABASE CRUD SYSTEM   ");
	Console.WriteLine("======================================");
	Console.WriteLine("1. Create (Add New User)");
	Console.WriteLine("2. Read   (View All Users)");
	Console.WriteLine("3. Update (Edit Existing User)");
	Console.WriteLine("4. Delete (Remove User)");
	Console.WriteLine("5. Exit Application");
	Console.Write("\nPlease select an option (1-5): ");

	string choice = Console.ReadLine();

	switch (choice)
	{
		case "1":
			CreateUser(connectionString);
			break;
		case "2":
			ReadUsers(connectionString);
			break;
		case "3":
			UpdateUser(connectionString);
			break;
		case "4":
			DeleteUser(connectionString);
			break;
		case "5":
			isRunning = false;
			Console.WriteLine("Exiting...");
			break;
		default:
			Console.WriteLine("Invalid selection. Please enter a number between 1 and 5.");
			break;
	}
}

// --- CRUD OPERATIONS ---

static void CreateUser(string connString)
{
	Console.Write("\nEnter Name: ");
	string name = Console.ReadLine();
	Console.Write("Enter Email: ");
	string email = Console.ReadLine();

	using (SqlConnection conn = new SqlConnection(connString))
	{
		string sql = "INSERT INTO Users (Name, Email) VALUES (@name, @email)";
		SqlCommand cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@name", name);
		cmd.Parameters.AddWithValue("@email", email);

		conn.Open();
		cmd.ExecuteNonQuery();
		Console.WriteLine("\n[SUCCESS] User created successfully.");
	}
}

static void ReadUsers(string connString)
{
	using (SqlConnection conn = new SqlConnection(connString))
	{
		string sql = "SELECT * FROM Users";
		SqlCommand cmd = new SqlCommand(sql, conn);

		conn.Open();
		using (SqlDataReader reader = cmd.ExecuteReader())
		{
			Console.WriteLine("\nID | Name | Email");
			Console.WriteLine("--------------------------------------");
			while (reader.Read())
			{
				Console.WriteLine($"{reader["Id"]} | {reader["Name"]} | {reader["Email"]}");
			}
		}
	}
}

static void UpdateUser(string connString)
{
	Console.Write("\nEnter User ID to update: ");
	string id = Console.ReadLine();
	Console.Write("Enter New Name: ");
	string name = Console.ReadLine();
	Console.Write("Enter New Email: ");
	string email = Console.ReadLine();

	using (SqlConnection conn = new SqlConnection(connString))
	{
		string sql = "UPDATE Users SET Name=@name, Email=@email WHERE Id=@id";
		SqlCommand cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@name", name);
		cmd.Parameters.AddWithValue("@email", email);
		cmd.Parameters.AddWithValue("@id", id);

		conn.Open();
		int rows = cmd.ExecuteNonQuery();
		if (rows > 0) Console.WriteLine("\n[SUCCESS] User updated successfully.");
		else Console.WriteLine("\n[ERROR] User ID not found.");
	}
}

static void DeleteUser(string connString)
{
	Console.Write("\nEnter User ID to delete: ");
	string id = Console.ReadLine();

	using (SqlConnection conn = new SqlConnection(connString))
	{
		string sql = "DELETE FROM Users WHERE Id=@id";
		SqlCommand cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@id", id);

		conn.Open();
		int rows = cmd.ExecuteNonQuery();
		if (rows > 0) Console.WriteLine("\n[SUCCESS] User deleted successfully.");
		else Console.WriteLine("\n[ERROR] User ID not found.");
	}
}
