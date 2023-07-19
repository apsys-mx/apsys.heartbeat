using apsys.heartbeat.authorization;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace apsys.heartbeat.repositories.nhibernate.mappers
{
    public class ApplicationRoleMapper : ClassMapping<ApplicationRole>
    {
        public ApplicationRoleMapper()
        {
            Table("ApplicationRoles");
            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Assigned);
                x.Column("Id");
            });
            Property(b => b.CreationDate, x => { x.Column("CreationDate"); });
            Property(b => b.Name, x => { x.Column("Name"); });
        }
    }
}
