using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace Leave_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRulesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedLeaves",
                table: "LeaveTypes");
            migrationBuilder.DropColumn(
                name: "CreditForAssociate",
                table: "LeaveTypes");
            migrationBuilder.DropColumn(
                name: "CreditForManager",
                table: "LeaveTypes");
            migrationBuilder.DropColumn(
                name: "DefaultBalanceAssociate",
                table: "LeaveTypes");
            migrationBuilder.DropColumn(
                name: "DefaultBalanceManager",
                table: "LeaveTypes");
            migrationBuilder.DropColumn(
                name: "IsApplicable",
                table: "LeaveTypes");
            migrationBuilder.DropColumn(
                name: "LeaveCreditFrequency",
                table: "LeaveTypes");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllowedLeaves",
                table: "LeaveTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<double>(
                name: "CreditForAssociate",
                table: "LeaveTypes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
            migrationBuilder.AddColumn<double>(
                name: "CreditForManager",
                table: "LeaveTypes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
            migrationBuilder.AddColumn<double>(
                name: "DefaultBalanceAssociate",
                table: "LeaveTypes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
            migrationBuilder.AddColumn<double>(
                name: "DefaultBalanceManager",
                table: "LeaveTypes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
            migrationBuilder.AddColumn<bool>(
                name: "IsApplicable",
                table: "LeaveTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<int>(
                name: "LeaveCreditFrequency",
                table: "LeaveTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
