using apsys.dynamic.filters.exceptions;
using apsys.dynamic.filters.extenders;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace apsys.dynamic.filters
{
    public class QueryStringParser
    {
        private const string _pageNumber = "pageNumber";
        private const string _pageSize = "pageSize";
        private const string _sortBy = "sortBy";
        private const string _sortDirection = "sortDirection";
        private const string _query = "query";
        private const string _query_ColumnsToSearch = "query_ColumnsToSearch";
        private readonly string _queryString;
        private readonly string _descending = "desc";
        private readonly string _ascending = "asc";
        private readonly string[] _excludedKeys = new string[] { _pageNumber, _pageSize, _sortBy, _sortDirection, _query };

        public QueryStringParser(string queryString)
        {
            _queryString = HttpUtility.UrlDecode(queryString);
        }

        public int ParsePageNumber()
        {
            int pageNumber = 0;
            if (string.IsNullOrEmpty(_queryString))
                return pageNumber;

            QueryStringArgs parameters = new QueryStringArgs(_queryString);
            if (parameters.ContainsKey(_pageNumber))
            {
                if (!int.TryParse(parameters[_pageNumber], out pageNumber))
                    throw new InvalidQueryStringArgumentException(_pageNumber);
            }
            if (pageNumber < 0)
                throw new InvalidQueryStringArgumentException(_pageNumber);
            return pageNumber;
        }

        public int ParsePageSize()
        {
            int pageSize = 25;
            if (string.IsNullOrEmpty(_queryString))
                return pageSize;
            QueryStringArgs parameters = new QueryStringArgs(_queryString);
            if (parameters.ContainsKey(_pageSize))
            {
                if (!int.TryParse(parameters[_pageSize], out pageSize))
                    throw new InvalidQueryStringArgumentException(_pageSize);
            }
            if (pageSize <= 0)
                throw new InvalidQueryStringArgumentException(_pageSize);
            return pageSize;
        }

        public Sorting ParseSorting<T>(string defaultFieldName)
        {
            string sortByField = defaultFieldName;
            string sortDirection = _descending;

            QueryStringArgs parameters = new QueryStringArgs(_queryString);
            if (parameters.ContainsKey(_sortBy))
            {
                sortByField = parameters[_sortBy];
                PropertyInfo[] properties = typeof(T).GetProperties();
                if (!properties.Any(p => p.Name.ToLower() == sortByField.ToLower()))
                    throw new InvalidQueryStringArgumentException(_sortBy);
            }
            if (parameters.ContainsKey(_sortDirection))
            {
                sortDirection = parameters[_sortDirection];
                if (sortDirection != _descending && sortDirection != _ascending)
                    throw new InvalidQueryStringArgumentException(_sortDirection);
            }
            return new Sorting(sortByField, sortDirection);
        }

        public IList<FilterOperator> ParseFilterOperators<T>()
        {
            IList<FilterOperator> filterOperatorsResult = new List<FilterOperator>();
            QueryStringArgs parameters = new QueryStringArgs(_queryString);
            IEnumerable<KeyValuePair<string, string>> allFilters = parameters.Where(parameter => !_excludedKeys.Contains(parameter.Key));
            foreach (var filter in allFilters)
            {
                string[] filterData = filter.Value.Split("||");
                string[] filterValues = filterData[0].Split("|");
                var fileterOperator = filterData[1];
                var operatorFieldName = filter.Key.ToPascalCase();
                filterOperatorsResult.Add(new FilterOperator(operatorFieldName, filterValues, fileterOperator));
            }
            return filterOperatorsResult;
        }

        public QuickSearch ParseQuery<T>()
        {
            /// - Declare variables
            string? query = string.Empty;
            string? fieldsString = string.Empty;
            IList<string> fields = new List<string>();
            QuickSearch quickSearch = new QuickSearch();

            /// - Verify _queryString has a value
            if (string.IsNullOrEmpty(_queryString))
                throw new InvalidQueryStringArgumentException(_query);

            /// - Parse _queryString into parameters
            QueryStringArgs parameters = new QueryStringArgs(_queryString);

            /// - Check that parameters contains a value for query string
            if (!parameters.ContainsKey(_query))
                throw new InvalidQueryStringArgumentException(_query);

            /// - Check that the value for query isn't null or a white space
            if (string.IsNullOrWhiteSpace(parameters[_query]))
                throw new InvalidQueryStringArgumentException(_query);

            /// - Get the value to use in the quick search
            query = parameters[_query].Split("||").FirstOrDefault();

            /// - Verify the value to use in the quick search is not null or empty
            if (string.IsNullOrEmpty(query))
                throw new InvalidQueryStringArgumentException(_query);

            /// - Get the properties of the inherited class
            PropertyInfo[] properties = typeof(T).GetProperties();

            /// - Case where there are no columns to search
            if (parameters[_query].Split("||").Count() <= 1)
            {
                /// - Define a collection of strings that will contain all fields that are
                /// -   of string type
                ICollection<string> stringFields = new List<string>();

                /// - Set the quick search value
                quickSearch.Value = parameters[_query];

                /// - Iterate over the inherited class's properties
                foreach (PropertyInfo property in properties)
                    /// - Check if the current property is of string type, and isn't the Id property
                    if (property.PropertyType == typeof(string) && property.Name != "Id")
                        /// - Add value to stringFields
                        stringFields.Add(property.Name);

                /// - Set the stringFields as the columns to use in quick search
                quickSearch.FieldNames = stringFields.ToList();

                /// - Return the quick search
                return quickSearch;
            }

            /// - Set query as the quick search value
            quickSearch.Value = query;

            /// - Verify that the columns to search has a value
            if (string.IsNullOrWhiteSpace(parameters[_query].Split("||")[1]))
                throw new InvalidQueryStringArgumentException(_query_ColumnsToSearch);

            /// - Get the columns to search into an array
            fields = parameters[_query].Split("||")[1].Split("|");

            /// - Verify if there is a value that isn't a property of the inherited class
            foreach (string field in fields)
                if (!properties.Any(p => p.Name.ToLower() == field.ToLower()))
                    throw new InvalidQueryStringArgumentException(_query_ColumnsToSearch);

            /// - Set fields as the columns to search in quick search
            quickSearch.FieldNames = fields;

            /// - Return the quick search
            return quickSearch;
        }
    }

    internal class QueryStringArgs : Dictionary<string, string>
    {
        private const string Pattern = @"(?<argName>\w+)=(?<argValue>.+)";
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
        public QueryStringArgs(string query)
        {
            var args = query.Split('&');
            foreach (var match in args.Select(arg => _regex.Match(arg)).Where(m => m.Success))
            {
                try
                {
                    this.Add(match.Groups["argName"].Value, match.Groups["argValue"].Value);
                }
                catch
                {
                    // Continues execution
                }
            }
        }
    }
}
