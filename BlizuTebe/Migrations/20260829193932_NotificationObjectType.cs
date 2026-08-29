using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlizuTebe.Migrations
{
    /// <inheritdoc />
    public partial class NotificationObjectType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RelatedObjectType",
                table: "Notifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatedObjectType",
                table: "Notifications");
        }
    }
}
