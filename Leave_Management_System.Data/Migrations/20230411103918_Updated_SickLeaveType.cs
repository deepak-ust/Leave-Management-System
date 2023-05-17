using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace Leave_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_SickLeaveType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllowedLeaves",
                table: "LeaveTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<bool>(
                name: "IsApplicable",
                table: "LeaveTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AlterColumn<string>(
                name: "FileUrl",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedLeaves",
                table: "LeaveTypes");
            migrationBuilder.DropColumn(
                name: "IsApplicable",
                table: "LeaveTypes");
            migrationBuilder.AlterColumn<string>(
                name: "FileUrl",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
