

using FluentMigrator;

namespace apsys.heartbeat.migrations
{
    [Migration(3)]
    public class M003_CreateAuthorizationTables : Migration
    {
        public override void Down()
        {
            Delete.Table("RolesPermissions");
            Delete.Table("ApplicationUserRoles");
            Delete.Table("Permissions");
            Delete.Table("ApplicationRoles");
        }

        public override void Up()
        {
            Create.Table("ApplicationRoles")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("CreationDate").AsDateTime().NotNullable()
                .WithColumn("Name").AsString().NotNullable().Unique();

            Create.Table("ApplicationUserRoles")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("ApplicationUserId").AsGuid().ForeignKey("FK_ApplicationUserRole_To_Users", "ApplicationUsers", "Id")
                .WithColumn("RoleId").AsGuid().ForeignKey("FK_ApplicationUserRole_To_Roles", "ApplicationRoles", "Id");

            Create.Table("Permissions").WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable().Unique();

            Create.Table("RolesPermissions")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("PermissionsId").AsGuid().ForeignKey("FK_RolePermission_To_Permissions", "Permissions", "Id")
                .WithColumn("RoleId").AsGuid().ForeignKey("FK_RolePermission_To_Roles", "ApplicationRoles", "Id");
        }
    }
}
