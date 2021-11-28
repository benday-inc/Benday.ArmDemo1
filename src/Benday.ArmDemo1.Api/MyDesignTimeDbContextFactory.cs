﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace Benday.ArmDemo1.Api;

public class MyDesignTimeDbContextFactory :
IDesignTimeDbContextFactory<MyDbContext>
{
    public MyDbContext Create()
    {
        var environmentName =
   Environment.GetEnvironmentVariable(
    "ASPNETCORE_ENVIRONMENT");

        if (environmentName == null)
        {
            environmentName = string.Empty;
        }

        var basePath = AppContext.BaseDirectory;

        return Create(basePath, environmentName);
    }

    public MyDbContext CreateDbContext(string[] args)
    {
        return Create(
            Directory.GetCurrentDirectory(),
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty);
    }

    private MyDbContext Create(string basePath, string environmentName)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{environmentName}.json", true)
            .AddEnvironmentVariables();

        var config = builder.Build();

        var connstr = config.GetConnectionString("default");

        if (String.IsNullOrWhiteSpace(connstr) == true)
        {
            throw new InvalidOperationException(
                "Could not find a connection string named 'default'.");
        }
        else
        {
            return Create(connstr);
        }
    }

    private MyDbContext Create(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentException(
               $"{nameof(connectionString)} is null or empty.",
               nameof(connectionString));

        var optionsBuilder =
          new DbContextOptionsBuilder<MyDbContext>();

        Console.WriteLine(
         "MyDesignTimeDbContextFactory.Create(string): Connection string: {0}",
         connectionString);

        optionsBuilder.UseSqlServer(connectionString);

        return new MyDbContext(optionsBuilder.Options);
    }
}
