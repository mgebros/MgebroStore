using Microsoft.EntityFrameworkCore.Migrations;

namespace MgebroStore.Migrations
{
    public partial class referralid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Referral",
                table: "Consultants",
                newName: "ReferralID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReferralID",
                table: "Consultants",
                newName: "Referral");
        }
    }
}
