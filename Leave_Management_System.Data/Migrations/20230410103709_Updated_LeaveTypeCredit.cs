using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace Leave_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_LeaveTypeCredit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Credit",
                table: "LeaveTypes",
                newName: "CreditForManager");
            migrationBuilder.AddColumn<int>(
                name: "CreditForAssociate",
                table: "LeaveTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditForAssociate",
                table: "LeaveTypes");
            migrationBuilder.RenameColumn(
                name: "CreditForManager",
                table: "LeaveTypes",
                newName: "Credit");
        }
    }
}
