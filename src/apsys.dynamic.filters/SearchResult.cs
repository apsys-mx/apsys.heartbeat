using System.Dynamic;

namespace apsys.dynamic.filters
{
    public class SearchResult<T> : ISearchResult<T>
    {
        public SearchResult()
        { }

        public SearchResult(int total, int pageNumber, int pageSize, Sorting sort, IEnumerable<T> items)
        {
            Total = total;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Sort = sort;
            Items = items;
        }

        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Sorting Sort { get; set; } = new Sorting();
        public IEnumerable<T> Items { get; set; } = new List<T>();

        public dynamic ToDynamic()
        {
            dynamic dynObj = new ExpandoObject();
            dynObj.pageNumber = PageNumber;
            dynObj.pageSize = PageSize;
            dynObj.total = Total;
            dynObj.items = Items;
            return dynObj;
        }
    }
}
