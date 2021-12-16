using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace Benday.ArmDemo1.UnitTests;

[TestClass]
public class DatabaseConnectionFixture
{
    [TestMethod]
    // [Timeout(10000)]
    public void ConnectToDatabase_ConnectionString()
    {
        var instanceName = "MSSQLLocalDB";

        SqlLocalDbUtility.Start(instanceName);

        Thread.Sleep(1000);
        
        var connstr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=15;";

        Connect(connstr);
    }

    [TestMethod]
    // [Timeout(10000)]
    public void ConnectToDatabase_NamedPipe()
    {
        var instanceName = "MSSQLLocalDB";

        SqlLocalDbUtility.Start(instanceName);

        Thread.Sleep(1000);

        var pipeInfo = SqlLocalDbUtility.GetNamedPipeInfo(instanceName);

        var builder = new SqlConnectionStringBuilder();
        builder.TrustServerCertificate = true;
        // builder.InitialCatalog = "test1234";
        builder.DataSource = pipeInfo;
        builder.Encrypt = false;
        
        var connstr = builder.ConnectionString;
        // var connstr = @$"server={pipeInfo};Initial Catalog=master;Integrated Security=True;Connect Timeout=15;TrustServerCertificate=true;";
        // var connstr = @$"server={pipeInfo}; Trust Server Certificate=true;";

        Connect(connstr);
    }
    
    private static void Connect(string connstr)
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
