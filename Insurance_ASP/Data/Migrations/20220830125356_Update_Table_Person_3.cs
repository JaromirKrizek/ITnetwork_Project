using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insurance_ASP.Data.Migrations
{
    public partial class Update_Table_Person_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAndNumber",
                table: "Person",
                newName: "HouseNumber");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Street",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "HouseNumber",
                table: "Person",
                newName: "StreetAndNumber");
        }
    }
}
