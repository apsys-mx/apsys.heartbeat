using apsys.heartbeat.repositories.nhibernate.extenders;
using NHibernate;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace apsys.heartbeat.repositories.nhibernate
{
    /// <summary>
    /// NHibernate Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Repository<T> : IRepository<T> where T : class, new()
    {

        protected internal readonly ISession _session;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="session"></param>
        public Repository(ISession session)
        {
            this._session = session;
        }

        /// <summary>
        /// Get an item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(string id)
        {
            return this._session.Get<T>(id);
        }

        /// <summary>
        /// Get all the items 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Get()
        {
            return this._session.Query<T>();
        }

        /// <summary>
        /// Get all the items with a sorting criteria
        /// </summary>
        /// <param name="sortingCriteria"></param>
        /// <returns></returns>
        public IEnumerable<T> Get(SortingCriteria sortingCriteria)
        {
            return this._session.Query<T>()
                .OrderBy(sortingCriteria.ToExpression());
        }

        /// <summary>
        /// Gets all items matching a query
        /// </summary>
        /// <param name="query">The linq query expression to match</param>
        /// <returns></returns>
        public IEnumerable<T> Get(Expression<Func<T, bool>> query)
        {
            return this._session.Query<T>()
                .Where(query);
        }

        /// <summary>
        /// Gets all items matching a query with a sorting criteria
        /// </summary>
        /// <param name="query">The linq query expression to match</param>
        /// <returns></returns>
        public IEnumerable<T> Get(Expression<Func<T, bool>> query, SortingCriteria sorting)
        {
            return this._session.Query<T>()
                .OrderBy(sorting.ToExpression())
                .Where(query);
        }

        /// <summary>
        /// Get all items paged
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public IEnumerable<T> Get(int page, int pageSize)
        {
            return this._session.Query<T>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        /// <summary>
        /// Get all items paged with a sorting criteria
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortingCriteria"></param>
        /// <returns></returns>
        public IEnumerable<T> Get(int page, int pageSize, SortingCriteria sortingCriteria)
        {
            return this._session.Query<T>()
                .OrderBy(sortingCriteria.ToExpression())
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        /// <summary>
        /// Get all paged query results
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<T> Get(Expression<Func<T, bool>> query, int page, int pageSize)
        {
            return this._session.Query<T>()
                .Where(query)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        /// <summary>
        /// Get all paged query results with a sorting criteria
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<T> Get(Expression<Func<T, bool>> query, int page, int pageSize, SortingCriteria sortingCriteria)
        {
            return this._session.Query<T>()
                .OrderBy(sortingCriteria.ToExpression())
                .Where(query)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        /// <summary>
        /// Add an item in the repository
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public T Add(T item)
        {
            this._session.Save(item);
            if (!this._session.Transaction.IsActive)
                this._session.Flush();
            return item;
        }

        /// <summary>
        /// Add an item in the repository asynchronous
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task AddAsync(T item)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Updates an item in the repository
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public T Save(T item)
        {
            this._session.Update(item);
            if (!this._session.Transaction.IsActive)
                this._session.Flush();
            return item;
        }

        /// <summary>
        /// Updates an item in the repository asynchronous
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task SaveAsync(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete an item from the repository
        /// </summary>
        /// <param name="item"></param>
        public void Delete(T item)
        {
            this._session.Delete(item);
            if (!this._session.Transaction.IsActive)
                this._session.Flush();
        }


        /// <summary>
        /// Delete an item from the repository asynchronous
        /// </summary>
        /// <param name="item"></param>
        public Task DeleteAsync(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Count all the records
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return this._session.QueryOver<T>().RowCount();
        }

        /// <summary>
        /// Count all the record matching with a query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public int Count(Expression<Func<T, bool>> query)
        {
            return this._session.Query<T>().Where(query).Count();
        }

    }
}
