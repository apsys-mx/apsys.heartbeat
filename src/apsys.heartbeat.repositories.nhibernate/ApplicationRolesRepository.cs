using apsys.heartbeat.authorization;
using NHibernate;
using System.Data;

namespace apsys.heartbeat.repositories.nhibernate
{
    public class ApplicationRolesRepository : Repository<ApplicationRole>, IApplicationRolesRepository
    {
        public ApplicationRolesRepository(ISession session)
            : base(session)
        {
        }

        /// <summary>
        /// Add user to role
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddUserToRole(string userName, string roleName)
        {
            var user = this._session.Query<ApplicationUser>().Where(x => x.UserName == userName).FirstOrDefault();
            var role = this.GetByName(roleName);
            user.AddRole(role);
        }

        /// <summary>
        /// Get role by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ApplicationRole GetByName(string name)
            => this.Get(x => x.Name == name).FirstOrDefault();
    }
}
