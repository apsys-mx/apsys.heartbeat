using apsys.dynamic.filters.extenders;

namespace apsys.dynamic.filters
{
    public class Sorting
    {
        public Sorting() { }

        public Sorting(string by, string direction)
        {
            By = by;
            Direction = direction;
        }

        private string _by;
        public string By
        {
            get { return string.IsNullOrEmpty(_by) ? _by : _by.ToPascalCase(); }
            set { _by = value; }
        }
        public string Direction { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(By) && !string.IsNullOrEmpty(Direction);
        }
    }
}
