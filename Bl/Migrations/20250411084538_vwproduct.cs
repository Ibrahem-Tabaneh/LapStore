using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bl.Migrations
{
    public partial class vwproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW VwProducts as
                 SELECT dbo.Products.Id, dbo.Products.ProductName, dbo.Products.ProductPrice, dbo.Products.ProductImg, dbo.Products.HardDisk, dbo.Products.RamSize, dbo.Products.ScreenReselution, dbo.Categories.CategoryName, dbo.Colors.ColorName, 
                  dbo.Typies.TypeName
                  FROM     dbo.Products INNER JOIN
                  dbo.Categories ON dbo.Products.CategoryId = dbo.Categories.Id INNER JOIN
                  dbo.Colors ON dbo.Products.ColorId = dbo.Colors.Id INNER JOIN
                  dbo.Oss ON dbo.Products.OsId = dbo.Oss.Id INNER JOIN
                  dbo.Typies ON dbo.Products.TypeId = dbo.Typies.Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
