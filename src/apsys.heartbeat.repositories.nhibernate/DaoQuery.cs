using apsys.heartbeat.repositories.nhibernate.extenders;
using NHibernate;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace apsys.heartbeat.repositories.nhibernate
{
    public class DaoQuery<T> : IDaoQuery<T> where T : class, new()
    {
        protected internal readonly ISession _session;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="session"></param>
        public DaoQuery(ISession session)
        {
            this._session = session;
        }

        public T Get(string id)
        {
            return this._session.Get<T>(id);
        }

        public IEnumerable<T> Get(int page, int pageSize)
        {
            return this._session.Query<T>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        public IEnumerable<T> Get(int page, int pageSize, SortingCriteria sortingCriteria)
        {
            return this._session.Query<T>()
                .OrderBy(sortingCriteria.ToExpression())
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> query)
        {
            return this._session.Query<T>()
                .Where(query);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> query, int page, int pageSize)
        {
            return this._session.Query<T>()
                .Where(query)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> query, int page, int pageSize, SortingCriteria sortingCriteria)
        {
            return this._session.Query<T>()
                .OrderBy(sortingCriteria.ToExpression())
                .Where(query)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
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
