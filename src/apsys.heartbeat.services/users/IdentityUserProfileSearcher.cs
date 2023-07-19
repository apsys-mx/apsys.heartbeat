using apsys.heartbeat.authorization.daos;
using apsys.heartbeat.repositories;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace apsys.heartbeat.services.users
{
    public static class IdentityUserProfileSearcher
    {
        /// <summary>
        /// Query class
        /// </summary>
        public class Query : RequestBase<IdentityUserDao>
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
        public class Handler : IRequestHandler<Query, IdentityUserDao>
        {
            private readonly IConfiguration configuration;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                this._unitOfWork = unitOfWork;
            }

            public Task<IdentityUserDao> Handle(Query request, CancellationToken cancellationToken)
            {
                var identityUser = _unitOfWork.Users.GetFromIdentityServer(request.UserName);
                return Task.FromResult(identityUser);
            }
        }
    }
}
