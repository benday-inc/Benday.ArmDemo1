using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.ArmDemo1.UnitTests;

[TestClass]
public class SqlLocalDbUtilFixture
{
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