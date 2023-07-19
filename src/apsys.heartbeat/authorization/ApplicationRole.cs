using apsys.heartbeat.contracts;
using apsys.heartbeat.validators;
using FluentValidation;

namespace apsys.heartbeat.authorization
{
    public class ApplicationRole : AbstractDomainObject, ITestable
    {
        public virtual string Name { get; set; }

        public override IValidator GetValidator()
            => new ApplicationRoleValidator();

        public virtual void SetMockData()
        {
            Name = "ADMINISTRATOR";
        }
    }
}
