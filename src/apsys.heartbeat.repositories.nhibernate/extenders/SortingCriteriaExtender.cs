namespace apsys.heartbeat.repositories.nhibernate.extenders
{
    public static class SortingCriteriaExtender
    {
        public static string ToExpression(this SortingCriteria sort)
        {
            string orderExpression = sort.Criteria == SortingCriteriaType.Ascending ? $"{sort.SortBy}" : $"{sort.SortBy} descending";
            return orderExpression;
        }
    }
}
