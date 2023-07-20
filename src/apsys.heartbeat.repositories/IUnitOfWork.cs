namespace apsys.heartbeat.repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationUsersRepository Users { get; }
        IApplicationRolesRepository Roles { get; }
        IMonitorServiceRepository Monitors { get; }
        void Commit();
        void Rollback();
        void ResetTransaction();
        bool IsActiveTransaction();
    }
}
