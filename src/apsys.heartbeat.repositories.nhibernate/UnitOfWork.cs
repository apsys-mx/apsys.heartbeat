using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NHibernate;

namespace apsys.heartbeat.repositories.nhibernate
{
    /// <summary>
    /// Unit of work implementation with NHibernate
    /// </summary>
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ILogger<UnitOfWork> _logger;
        private readonly ISession _session;
        private readonly IConfiguration _configuration;
        private ITransaction _transaction;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="session"></param>
        /// <param name="logger"></param>
        public UnitOfWork(ISession session, ILogger<UnitOfWork> logger, IConfiguration configuration)
        {
            this._session = session;
            this._logger = logger;
            this._configuration = configuration;
            this._transaction = session.BeginTransaction();

            this.Users = new ApplicationUsersRepository(session, configuration);
            this.Roles = new ApplicationRolesRepository(session);
            this.Monitors = new MonitorServiceRepository(session);
        }

        public IApplicationUsersRepository Users { get; private set; }
        public IApplicationRolesRepository Roles { get; private set; }
        public IMonitorServiceRepository Monitors { get; private set; }

        public void Commit()
        {
            if (_transaction != null && _transaction.IsActive)
                _transaction.Commit();
            else
                throw new TransactionException("The actual transaction is not longer active");
        }

        public void ResetTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public bool IsActiveTransaction()
        {
            return _transaction != null && _transaction.IsActive;
        }

        public void Rollback()
        {
            _transaction.Dispose();
            _session.Dispose();
        }

        public void Dispose()
        {
            if (_transaction != null && _transaction.IsActive)
                _transaction.Rollback();
            if (_session != null)
                _session.Dispose();
        }
    }
}