using apsys.heartbeat.migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.RegularExpressions;

public class Program
{
    static int Main()
    {
        try
        {
            CommandLineArgs parameter = new CommandLineArgs();
            if (!parameter.ContainsKey("cnn"))
                throw new ArgumentException("No [cnn] parameter received. You need pass the connection string in order to execute the migrations");

            bool isRollBack = parameter.ContainsKey("rollback");

            string connectionString = parameter["cnn"];
            var serviceProvider = CreateServices(connectionString);
            using (var scope = serviceProvider.CreateScope())
            {
                if (isRollBack)
                {
                    long rollBackToVersion = 0;
                    if (parameter["rollback"].ToLower().Trim() == "one")
                    {
                        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
                        var lastMigration = runner.MigrationLoader.LoadMigrations().LastOrDefault();
                        rollBackToVersion = lastMigration.Value.Version - 1;
                    }
                    else if (!long.TryParse(parameter["rollback"], out rollBackToVersion))
                        throw new ArgumentException($"Invalid rollback version value: [{parameter["rollback"]}]");

                    // Execute rollback
                    RollbackDatabase(scope.ServiceProvider, rollBackToVersion);
                }
                else
                    UpdateDatabase(scope.ServiceProvider);
            }
            return (int)ExitCode.Success;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error updating the database schema: {ex.Message}");
            Console.ResetColor();
            return (int)ExitCode.UnknownError;
        }
    }
    /// <summary>
    /// Configure the dependency injection services
    /// </sumamry>
    private static IServiceProvider CreateServices(string connectionString)
    {
        return new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSqlServer2016()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(typeof(M001_Sandbox).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);
    }

    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    private static void RollbackDatabase(IServiceProvider serviceProvider, long rollbackVersion)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateDown(rollbackVersion);
    }
}

/// <summary>
/// Enumerate the exit codes
/// </summary>
internal enum ExitCode
{
    Success = 0,
    UnknownError = 1
}

/// <summary>
/// Dictionary with input parameters of console application
/// </summary>
internal class CommandLineArgs : Dictionary<string, string>
{
    private const string Pattern = @"\/(?<argname>\w+):(?<argvalue>.+)";
    private readonly Regex _regex = new Regex(Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <summary>
    /// Determine if the user pass at least one valid parameter
    /// </summary>
    /// <returns></returns>
    public bool ContainsValidArguments()
    {
        return (this.ContainsKey("cnn"));
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public CommandLineArgs()
    {
        var args = Environment.GetCommandLineArgs();
        foreach (var match in args.Select(arg => _regex.Match(arg)).Where(m => m.Success))
        {
            try
            {
                this.Add(match.Groups["argname"].Value, match.Groups["argvalue"].Value);
            }
            catch (Exception) { }
        }
    }
}