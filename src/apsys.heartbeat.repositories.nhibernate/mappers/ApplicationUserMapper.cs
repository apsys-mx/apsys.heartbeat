using apsys.heartbeat.authorization;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace apsys.heartbeat.repositories.nhibernate.Mappers
{
    public class ApplicationUserMapper : ClassMapping<ApplicationUser>
    {
        public ApplicationUserMapper()
        {
            Table("ApplicationUsers");
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Assigned);
                x.Column("Id");
            });
            Property(b => b.CreationDate, x => { x.Column("CreationDate"); });
            Property(b => b.UserName, x => { x.Column("UserName"); });
            Property(b => b.Name, x => { x.Column("Name"); });
            Property(b => b.Email, x => { x.Column("Email"); });
        }
    }
}
