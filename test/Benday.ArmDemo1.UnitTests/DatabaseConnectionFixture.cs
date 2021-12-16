using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Benday.ArmDemo1.UnitTests;

[TestClass]
public class DatabaseConnectionFixture
{
    [TestMethod]
    // [Timeout(10000)]
    public void ConnectToDatabase()
    {
        Console.WriteLine("asdfasdf");

        // var connstr = "server=(localdb)\\MSSQLLocalDB; database=master; Integrated Security=True;";
        // var connstr = "server=(localdb)\\.; database=test1234; Integrated Security=True;";
        // var connstr = @"server=np:\\.\pipe\LOCALDB#SHF1EE43\tsql\query";
        // var connstr = @"Data Source=(localdb)\dev;Connect Timeout=10;";
        var connstr = @"server=(localdb)\dev;Connect Timeout=10;database=test123;";
        // var connstr = @"server=(localdb)\dev;Integrated Security=True;Connect Timeout=10;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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