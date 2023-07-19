namespace apsys.dynamic.filters
{
    public class QuickSearch
    {
        public string Value { get; set; }
        public IList<string> FieldNames { get; set; } = new List<string>();
    }
}
