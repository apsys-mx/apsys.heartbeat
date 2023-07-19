using apsys.heartbeat.authorization;
using apsys.heartbeat.repositories;
using MediatR;

namespace apsys.heartbeat.services
{
    public static class ApplicationInitializer
    {
        /// <summary>
        /// Command class
        /// </summary>
        public class Command : IRequest
        {
        }

        /// <summary>
        /// Handler class
        /// </summary>
        public class Handler : IRequestHandler<Command>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork uoW)
            {
                this._unitOfWork = uoW;
            }

            public Task Handle(Command request, CancellationToken cancellationToken)
            {
                // Add users
                var adimbptm = AddApplicationUser("adimbptm", "Administrador", "administrator@efemsa.com");

                // Add roles
                var administrator = AddRole(ApplicationRoles.Administrator);
                AddUserToRole(adimbptm, administrator);
                _unitOfWork.Commit();

                return Task.CompletedTask;
            }

            public ApplicationUser AddApplicationUser(string userName, string name, string email)
            {
                var existingUser = _unitOfWork.Users.GetByUserName(userName);
                if (existingUser == null)
                {
                    existingUser = new ApplicationUser() { UserName = userName, Name = name, Email = email };
                    _unitOfWork.Users.Add(existingUser);
                }
                return existingUser;
            }

            public ApplicationRole AddRole(string roleName)
            {
                var existingRole = this._unitOfWork.Roles.GetByName(roleName);
                if (existingRole == null)
                {
                    existingRole = new ApplicationRole() { Name = roleName };
                    this._unitOfWork.Roles.Add(existingRole);
                }
                return existingRole;
            }

            public void AddUserToRole(ApplicationUser user, ApplicationRole role)
            {
                this._unitOfWork.Roles.AddUserToRole(user.UserName, role.Name);
                this._unitOfWork.Users.Save(user);
            }
        }
    }
}
