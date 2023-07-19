using apsys.heartbeat.authorization;
using FluentAssertions;
using System.Data;

namespace apsys.heartbeat.repositories.nhibernate.testing
{
    internal class ApplicationRolesRepositoryTests : UnitOfWorkTestingBase
    {
        [Test]
        public void Add_EmptyTable_OneRoleInserted()
        {
            /// Arrange
            ApplicationRole role = new ApplicationRole();
            role.SetMockData();
            /// Act
            this.unitOfWork.Roles.Add(role);
            this.unitOfWork.Commit();
            /// Assert
            var dataSet = this.nDbUnitTest.GetDataSetFromDb();
            var table = dataSet.Tables["ApplicationRoles"];
            table.Should().NotBeNull();
            table.Rows.Count.Should().Be(1);
            var roleRow = table.Rows[0];
            roleRow.Should().NotBeNull();
            roleRow.Field<Guid>("Id").Should().Be(role.Id);
            roleRow.Field<DateTime>("CreationDate").Date.Should().Be(role.CreationDate.Date);
            roleRow.Field<string>("Name").Should().Be(role.Name);
        }

    }
}
