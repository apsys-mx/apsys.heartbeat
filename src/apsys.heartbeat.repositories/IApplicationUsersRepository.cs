using apsys.heartbeat.authorization;
using apsys.heartbeat.authorization.daos;

namespace apsys.heartbeat.repositories
{
    public interface IApplicationUsersRepository : IRepository<ApplicationUser>
    {
        ApplicationUser GetByUserName(string userName);
        IdentityUserDao GetFromIdentityServer(string userName);
    }
}
