using Microsoft.EntityFrameworkCore.Migrations;

namespace MgebroStore.Migrations
{
    public partial class ReferrerID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Referral",
                table: "Consultants",
                newName: "ReferrerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReferrerID",
                table: "Consultants",
                newName: "Referral");
        }
    }
}
