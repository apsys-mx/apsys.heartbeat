namespace apsys.heartbeat.repositories
{
    /// <summary>
    /// Class representing a sorting criteria
    /// </summary>
    public class SortingCriteria
    {
        public string SortBy { get; set; } = string.Empty;
        public SortingCriteriaType Criteria { get; set; } = SortingCriteriaType.Ascending;

        /// <summary>
        /// Constructor
        /// </summary>
        public SortingCriteria()
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        public SortingCriteria(string sortBy)
        {
            this.SortBy = sortBy;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SortingCriteria(string sortBy, SortingCriteriaType criteria)
        {
            this.SortBy = sortBy;
            this.Criteria = criteria;
        }
    }

    /// <summary>
    /// The sorting criteria type enumeration
    /// </summary>
    public enum SortingCriteriaType
    {
        /// <summary>
        /// Sort ascending
        /// </summary>
        Ascending = 1,

        /// <summary>
        /// Sort descending
        /// </summary>
        Descending = 2
    }
}
