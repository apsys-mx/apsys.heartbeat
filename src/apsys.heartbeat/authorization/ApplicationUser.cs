using apsys.heartbeat.contracts;
using apsys.heartbeat.validators;
using FluentValidation;
using System.Data;
using System.Dynamic;

namespace apsys.heartbeat.authorization
{
    public class ApplicationUser : AbstractDomainObject, ITestable, ISerializable
    {
        public virtual string UserName { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string UserType { get; set; }
        public virtual IList<ApplicationRole> Roles { get; set; } = new List<ApplicationRole>();

        /// <summary>
        /// Determine if the user has a role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual bool HasRole(string roleName)
            => this.Roles.Any(x => x.Name.Trim().ToLower() == roleName.Trim().ToLower());

        /// <summary>
        /// Empty constructor
        /// </summary>
        public ApplicationUser() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationUser(string userName)
        {
            this.Id = Guid.NewGuid();
            this.CreationDate = DateTime.Now;
            this.UserName = userName;
        }

        /// <summary>
        /// Add a role
        /// </summary>
        /// <param name="role"></param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void AddRole(ApplicationRole role)
        {
            var adderRole = this.Roles.FirstOrDefault(x => x.Name == role.Name);
            if (adderRole == null)
                this.Roles.Add(role);
        }

        public virtual void RemoveRole(ApplicationRole role)
        {
            var adderRole = this.Roles.FirstOrDefault(x => x.Name == role.Name);
            if (adderRole != null)
                this.Roles.Remove(role);
        }

        /// <summary>
        /// Determine if the user is in a role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public virtual bool IsInRole(string roleName)
            => this.Roles.Any(x => x.Name == roleName);

        public virtual dynamic ToDynamic()
        {
            dynamic result = new ExpandoObject();
            result.UserName = this.UserName;
            result.Name = this.Name;
            result.Email = this.Email;
            result.UserType = this.UserType;
            result.Roles = this.Roles.Select(x => x.Name);
            return result;
        }

        public virtual void CopyFrom(ApplicationUser user)
        {
            this.Email = user.Email;
            this.Name = user.Name;
        }

        public override IValidator GetValidator()
            => new ApplicationUserValidator();

        public virtual void SetMockData()
        {
            this.UserName = "adimbptm";
            this.Name = "Administrador";
            this.Email = "adimbptm@company.com";
        }

    }
}
