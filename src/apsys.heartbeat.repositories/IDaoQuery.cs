using System.Linq.Expressions;

namespace apsys.heartbeat.repositories
{
    /// <summary>
    /// Define contract for IQuery
    /// </summary>
    public interface IDaoQuery<T> where T : class, new()
    {
        /// <summary>
        /// Get an item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(string id);

        /// <summary>
        /// Get a paginated result from all results
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<T> Get(int page, int pageSize);

        /// <summary>
        /// Get a paginated result from all results
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<T> Get(int page, int pageSize, SortingCriteria sortingCriteria);

        /// <summary>
        /// Gets all items matching a query
        /// </summary>
        /// <param name="query">The linq query expression to match</param>
        /// <returns></returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> query);

        /// <summary>
        /// Get a paginated result from all results where the query was applied
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> query, int page, int pageSize);

        /// <summary>
        /// Get a paginated result from all results where the query was applied
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> query, int page, int pageSize, SortingCriteria sortingCriteria);

        /// <summary>
        /// Count all the records
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Count all the record matching with a query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> query);
    }
}
