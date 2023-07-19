using apsys.heartbeat.authorization;
using FluentAssertions;
using System.Data;

namespace apsys.heartbeat.repositories.nhibernate.testing
{
    internal class ApplicationUsersRepositoryTests : UnitOfWorkTestingBase
    {
        [Test]
        public void Add_EmptyTable_OneUserInserted()
        {
            /// Arrange
            ApplicationUser user = new ApplicationUser();
            user.SetMockData();
            /// Act
            this.unitOfWork.Users.Add(user);
            this.unitOfWork.Commit();
            /// Assert
            var dataSet = this.nDbUnitTest.GetDataSetFromDb();
            var table = dataSet.Tables["ApplicationUsers"];
            table.Should().NotBeNull();
            table.Rows.Count.Should().Be(1);
            var userRow = table.Rows[0];
            userRow.Should().NotBeNull();
            userRow.Field<Guid>("Id").Should().Be(user.Id);
            userRow.Field<DateTime>("CreationDate").Date.Should().Be(user.CreationDate.Date);
            userRow.Field<string>("UserName").Should().Be(user.UserName);
            userRow.Field<string>("Name").Should().Be(user.Name);
            userRow.Field<string>("Email").Should().Be(user.Email);
        }
    }
}
