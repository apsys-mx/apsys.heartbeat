using apsys.heartbeat.authorization;

namespace apsys.heartbeat.repositories
{
    public interface IApplicationRolesRepository : IRepository<ApplicationRole>
    {
        ApplicationRole GetByName(string name);
        void AddUserToRole(string userName, string roleName);
    }
}
