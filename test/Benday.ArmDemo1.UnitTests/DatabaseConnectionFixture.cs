using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace Benday.ArmDemo1.UnitTests;

[TestClass]
public class DatabaseConnectionFixture
{
    [TestMethod]
    [Timeout(5000)]
    public void ConnectToDatabase_ConnectionString_LocalDb_NamedInstance()
    {
        var instanceName = "MSSQLLocalDB";

        SqlLocalDbUtility.Start(instanceName);

        Thread.Sleep(1000);

        var connstr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=15;";

        Connect(connstr);
    }

    [TestMethod]
    [Timeout(5000)]
    public void ConnectToDatabase_ConnectionString_LocalDb_NamedInstance_UseManagedNetworkingOnWindows()
    {
        Console.WriteLine("Setting use managed networking on windows...");
        AppContext.SetSwitch("Switch.Microsoft.Data.SqlClient.UseManagedNetworkingOnWindows", true);

        var instanceName = "MSSQLLocalDB";

        SqlLocalDbUtility.Start(instanceName);

        Thread.Sleep(1000);

        var connstr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=15;";

        Connect(connstr);
    }

    [TestMethod]
    [Timeout(5000)]
    public void ConnectToDatabase_ConnectionString_LocalDb_DefaultInstance()
    {
        var instanceName = "MSSQLLocalDB";

        SqlLocalDbUtility.Start(instanceName);

        Thread.Sleep(1000);

        var connstr = @"Data Source=(localdb)\.;Initial Catalog=master;Integrated Security=True;Connect Timeout=15;";

        Connect(connstr);
    }

    [TestMethod]
    [Timeout(5000)]
    public void ConnectToDatabase_NamedPipe()
    {
        var instanceName = "MSSQLLocalDB";

        SqlLocalDbUtility.Start(instanceName);

        Thread.Sleep(1000);

        var pipeInfo = SqlLocalDbUtility.GetNamedPipeInfo(instanceName);

        var connstr = @$"Data Source={pipeInfo}; Encrypt=false;";

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
