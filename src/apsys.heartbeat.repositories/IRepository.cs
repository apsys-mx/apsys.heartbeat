using System.Linq.Expressions;

namespace apsys.heartbeat.repositories
{
    /// <summary>
    /// Defines the contract to be implemented by repositories's classes 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class, new()
    {

        /// <summary>
        /// Get an item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(string id);

        /// <summary>
        /// Get all the items 
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Get();

        /// <summary>
        /// Get all the items 
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Get(SortingCriteria sortingCriteria);

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
        /// Gets all items matching a query
        /// </summary>
        /// <param name="query">The linq query expression to match</param>
        /// <param name="sorting">The sorting criteria</param>
        /// <returns></returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> query, SortingCriteria sorting);

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
        /// Add an item in the repository
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        T Add(T item);

        /// <summary>
        /// Add an item in the repository asynchronous
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task AddAsync(T item);

        /// <summary>
        /// Saves or updates an item in the repository
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        T Save(T item);

        /// <summary>
        /// Saves or updates an item in the repository asynchronous
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task SaveAsync(T item);

        /// <summary>
        /// Delete an item from the repository
        /// </summary>
        /// <param name="item"></param>
        void Delete(T item);

        /// <summary>
        /// Delete an item from the repository asynchronous
        /// </summary>
        /// <param name="item"></param>
        Task DeleteAsync(T item);

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
