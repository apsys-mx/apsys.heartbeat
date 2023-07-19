namespace apsys.dynamic.filters
{
    public class FilterOperator
    {
        public FilterOperator()
        { }

        public FilterOperator(string fileldName, IEnumerable<string> values, string relationalOperatorType)
        {
            FieldName = fileldName;
            RelationalOperatorType = relationalOperatorType;
            Values = values.ToList();
        }

        public string FieldName { get; set; }
        public string RelationalOperatorType { get; set; }
        public IList<string> Values { get; set; }
    }
}
