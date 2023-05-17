using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace Leave_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_LeaveRequestFIle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "LeaveRequests");
        }
    }
}
