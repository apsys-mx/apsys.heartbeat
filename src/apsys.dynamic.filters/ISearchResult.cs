namespace apsys.dynamic.filters
{
    public interface ISearchResult<T>
    {
        int Total { get; set; }
        int PageNumber { get; set; }
        int PageSize { get; set; }
        Sorting Sort { get; set; }
        IEnumerable<T> Items { get; set; }
    }
}
