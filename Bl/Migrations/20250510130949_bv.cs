using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bl.Migrations
{
    public partial class bv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW VwUsers AS
SELECT dbo.AspNetUsers.Lname, dbo.AspNetUsers.Fname, dbo.AspNetUsers.Email, dbo.AspNetRoles.Name
FROM     dbo.AspNetUsers INNER JOIN
dbo.AspNetUserRoles ON dbo.AspNetUsers.Id = dbo.AspNetUserRoles.UserId INNER JOIN
dbo.AspNetRoles ON dbo.AspNetUserRoles.RoleId = dbo.AspNetRoles.Id
              ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
