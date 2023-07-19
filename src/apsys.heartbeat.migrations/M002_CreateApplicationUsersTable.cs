using FluentMigrator;

namespace apsys.heartbeat.migrations
{
    [Migration(2)]
    public class M002_CreateApplicationUsersTable : Migration
    {
        public override void Down()
        {
            Delete.Table("ApplicationUsers");
        }

        public override void Up()
        {
            Create.Table("ApplicationUsers")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("CreationDate").AsDateTime().NotNullable()
                .WithColumn("UserName").AsString().NotNullable().Unique()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable();
        }
    }
}
