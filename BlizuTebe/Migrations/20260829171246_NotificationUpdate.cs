using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlizuTebe.Migrations
{
    /// <inheritdoc />
    public partial class NotificationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RelatedObjectId",
                table: "Notifications",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatedObjectId",
                table: "Notifications");
        }
    }
}
