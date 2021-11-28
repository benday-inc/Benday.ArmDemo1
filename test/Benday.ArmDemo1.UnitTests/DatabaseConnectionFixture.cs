using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Benday.ArmDemo1.UnitTests;

[TestClass]
public class DatabaseConnectionFixture
{
    [TestMethod]
    [Timeout(10000)]
    public void ConnectToDatabase()
    {
        Console.WriteLine("asdfasdf");

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
}