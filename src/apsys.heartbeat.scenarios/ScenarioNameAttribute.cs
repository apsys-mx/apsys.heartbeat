namespace apsys.heartbeat.scenarios
{
    /// <summary>
    /// Scenario name attribute in order to identify the scenario to execute
    /// </summary>
    public class ScenarioNameAttribute : Attribute
    {
        /// <summary>
        /// Gets or set the name of scenario
        /// </summary>
        public string ScenarioName { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="scenarioName"></param>
        public ScenarioNameAttribute(string scenarioName)
        {
            this.ScenarioName = scenarioName;
        }
    }
}