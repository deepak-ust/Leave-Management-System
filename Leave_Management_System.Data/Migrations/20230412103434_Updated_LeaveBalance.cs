using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace Leave_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_LeaveBalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "CreditForManager",
                table: "LeaveTypes",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
            migrationBuilder.AlterColumn<double>(
                name: "CreditForAssociate",
                table: "LeaveTypes",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
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
            migrationBuilder.CreateTable(
                name: "EmployeeLeaveBalances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaveBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaveBalances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaveBalances_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveBalances_EmployeeId",
                table: "EmployeeLeaveBalances",
                column: "EmployeeId");
            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveBalances_LeaveTypeId",
                table: "EmployeeLeaveBalances",
                column: "LeaveTypeId");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeLeaveBalances");
            migrationBuilder.DropColumn(
                name: "DefaultBalanceAssociate",
                table: "LeaveTypes");
            migrationBuilder.DropColumn(
                name: "DefaultBalanceManager",
                table: "LeaveTypes");
            migrationBuilder.AlterColumn<int>(
                name: "CreditForManager",
                table: "LeaveTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
            migrationBuilder.AlterColumn<int>(
                name: "CreditForAssociate",
                table: "LeaveTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
