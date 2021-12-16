using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Benday.ArmDemo1.UnitTests;

[TestClass]
public class DatabaseConnectionFixture
{
    [TestMethod]
    // [Timeout(10000)]
    public void ConnectToDatabase_ConnectionString()
    {
        var connstr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=test1234;Integrated Security=True;Connect Timeout=15;";

        Connect(connstr);
    }

    [TestMethod]
    // [Timeout(10000)]
    public void ConnectToDatabase_NamedPipe()
    {
        var connstr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=test1234;Integrated Security=True;Connect Timeout=15;";

        Connect(connstr);
    }

    // var connstr = @"server=np:\\.\pipe\LOCALDB#SHF1EE43\tsql\query";
        

    private void Connect(string connstr)
    {
        Console.WriteLine($"Attempting to connect using connection string: {Environment.NewLine}{connstr}");

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
