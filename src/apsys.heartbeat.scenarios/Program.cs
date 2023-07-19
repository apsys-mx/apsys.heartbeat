using apsys.heartbeat.scenarios;
using System.Text.RegularExpressions;

try
{
    CommandLineArgs parameter = new CommandLineArgs();
    if (!(parameter.ContainsKey("load")))
        throw new ArgumentException("No valid parameter received. You need pass a [/load] command in order to execute a scenario");

    ScenarioBuilder builder = new ScenarioBuilder();
    string scenarioName = parameter["load"];
    Console.WriteLine($"Loading scenario [{scenarioName}]");
    var scenario = builder.Build(scenarioName);
    scenario.SeedData();

    Console.ResetColor();
    return 0;
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Error creating scenario -->{ex}");
    Console.ResetColor();
    return -1;
}

/// <summary>
/// Command line arguments
/// </summary>
internal class CommandLineArgs : Dictionary<string, string>
{
    private const string Pattern = @"\/(?<argname>\w+):(?<argvalue>.+)";
    private readonly Regex _regex = new Regex(Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <summary>
    /// Constructor
    /// </summary>
    public CommandLineArgs()
    {
        var args = Environment.GetCommandLineArgs();
        foreach (var match in args.Select(arg => _regex.Match(arg)).Where(m => m.Success))
            this.Add(match.Groups["argname"].Value, match.Groups["argvalue"].Value);
    }
}