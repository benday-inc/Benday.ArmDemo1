using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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

            while(line is not null)
            {
                returnValue.Add(line.Trim());
                line = reader.ReadLine();
            }

            return returnValue;
        }
    }
}