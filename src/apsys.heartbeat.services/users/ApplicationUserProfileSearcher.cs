using apsys.heartbeat.authorization;
using apsys.heartbeat.repositories;
using MediatR;

namespace apsys.heartbeat.services.users
{
    public static class ApplicationUserProfileSearcher
    {
        /// <summary>
        /// Query class
        /// </summary>
        public class Query : RequestBase<ApplicationUser>
        {
            public Query(string requestedBy, string userName)
                : base(requestedBy)
            {
                UserName = userName;
            }

            public string UserName { get; }
        }

        /// <summary>
        /// Handler class
        /// </summary>
        public class Handler : IRequestHandler<Query, ApplicationUser>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }

            public Task<ApplicationUser> Handle(Query request, CancellationToken cancellationToken)
            {
                var applicationUser = _unitOfWork.Users.GetByUserName(request.UserName);
                return Task.FromResult(applicationUser);
            }
        }
    }
}
