// See https://aka.ms/new-console-template for more information
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
ConnectToDatabase();

static void ConnectToDatabase()
{
    var connstr = "server=(localdb)\\MSSQLLocalDB; database=test1234; Integrated Security=True;";

    using var conn = new SqlConnection(connstr);

    Console.WriteLine("Calling open...");

    conn.Open();

    Console.WriteLine("Opened.");

    using var command = conn.CreateCommand();

    command.CommandText = "sp_tables";

    var results = command.ExecuteNonQuery();

    Console.WriteLine($"Results: {results}");
}