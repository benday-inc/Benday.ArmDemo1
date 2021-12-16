using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.ArmDemo1.UnitTests;

[TestClass]
public class SqlLocalDbUtilFixture
{
    [TestInitialize]
    public void OnTestInitialize()
    {
        _systemUnderTest = null;
    }
    private SqlLocalDbUtility? _systemUnderTest;
    public SqlLocalDbUtility SystemUnderTest
    {
        get
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = new SqlLocalDbUtility();
            }

            return _systemUnderTest;
        }
    }

    [TestMethod]
    public void GetInstanceNames()
    {
        var actual = SystemUnderTest.InstanceNames;

        Assert.IsNotNull(actual, "InstanceNames should not be empty");
        Assert.AreNotEqual<int>(0, actual.Count, "InstanceNames count should not be zero");
    }

    [TestMethod]
    public void IsInstanceRunning_InstancesThatExist()
    {
        var instances = SystemUnderTest.InstanceNames;

        Assert.AreNotEqual<int>(0, instances.Count, "There should be instances");

        foreach (var instanceName in instances)
        {
            var isRunning = SqlLocalDbUtility.IsInstanceRunning(instanceName);

            Console.WriteLine($"Instance '{instanceName}' is running '{isRunning}'");
        }
    }

    [TestMethod]
    public void IsInstanceRunning_InstancesThatDoesntExist_ReturnFalse()
    {
        var actual = SqlLocalDbUtility.IsInstanceRunning("completelybogus");

        Assert.IsFalse(actual, "Value should be false.");
    }

    [TestMethod]
    public void CallSqlLocalDbAndReadResults()
    {
        // arrange

        var startInfo = new ProcessStartInfo("sqllocaldb", "i")
        {
            RedirectStandardOutput = true
        };

        // act
        var process = Process.Start(startInfo);

        if (process is null)
        {
            Assert.Fail("Process was null");
        }
        else
        {
            // assert
            var actualOutput = process.StandardOutput.ReadToEnd();

            Console.WriteLine($"{actualOutput}");

            Assert.AreNotEqual<string>(string.Empty, actualOutput, "Output should not be empty.");
        }
    }
}
