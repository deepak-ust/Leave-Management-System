using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace Leave_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarryForward",
                table: "LeaveTypes");
            migrationBuilder.DropColumn(
                name: "Manager",
                table: "Employees");
            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "LeaveRequests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
            migrationBuilder.AddColumn<int>(
                name: "Band",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "EmployeeLeaveBalances",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });
            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");
            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees");
            migrationBuilder.DropTable(
                name: "Roles");
            migrationBuilder.DropIndex(
                name: "IX_Employees_RoleId",
                table: "Employees");
            migrationBuilder.DropColumn(
                name: "Band",
                table: "Employees");
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Employees");
            migrationBuilder.AddColumn<int>(
                name: "CarryForward",
                table: "LeaveTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "LeaveRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "LeaveRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
            migrationBuilder.AddColumn<string>(
                name: "Manager",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "EmployeeLeaveBalances",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
