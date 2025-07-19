using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlizuTebe.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "isVerified",
                table: "Users",
                newName: "IsVerified");

            migrationBuilder.RenameColumn(
                name: "dateOfBirth",
                table: "Users",
                newName: "DateOfBirth");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "IsVerified",
                table: "Users",
                newName: "isVerified");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Users",
                newName: "dateOfBirth");
        }
    }
}
