using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Benday.ArmDemo1.UnitTests;

public class SqlLocalDbUtility
{
    private List<string>? _instanceNames;

    public List<string> InstanceNames
    {
        get
        {
            if (_instanceNames == null)
            {
                _instanceNames = ReadInstanceNames();
            }

            return _instanceNames;
        }
    }

    private static List<string> ReadInstanceNames()
    {
        var startInfo = new ProcessStartInfo("sqllocaldb", "i")
        {
            RedirectStandardOutput = true
        };

        return RunProcessAndGetOutputLines(startInfo);
    }

    private static List<string> RunProcessAndGetOutputLines(ProcessStartInfo startInfo)
    {
        var process = Process.Start(startInfo);

        if (process is null)
        {
            throw new InvalidOperationException("Process was null");
        }
        else
        {
            var returnValue = new List<string>();

            var output = process.StandardOutput.ReadToEnd();

            using var reader = new StringReader(output);

            string? line;

            line = reader.ReadLine();

            while (line is not null)
            {
                returnValue.Add(line.Trim());
                line = reader.ReadLine();
            }

            return returnValue;
        }
    }

    public static bool IsInstanceRunning(string instanceName)
    {
        var startInfo = new ProcessStartInfo("sqllocaldb")
        {
            RedirectStandardOutput = true
        };

        startInfo.ArgumentList.Add("i");
        startInfo.ArgumentList.Add(instanceName);

        var lines = RunProcessAndGetOutputLines(startInfo);

        var value = GetValueFromLines(lines, "State:");

        if (value is null)
        {
            return false;
        }
        else
        {
            if (value == "Running")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private static string? GetValueFromLines(List<string> lines, string startingValueForLine)
    {
        var match = lines.Where(
            x => x.StartsWith(startingValueForLine)).FirstOrDefault();

        if (match == null)
        {
            return null;
        }
        else
        {
            var returnValue =
                match.Replace(startingValueForLine, string.Empty).Trim();

            return returnValue;
        }
    }
}