using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace Leave_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_EmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees");
            migrationBuilder.DropIndex(
                name: "IX_Employees_RoleId",
                table: "Employees");
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Employees");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Employees",
                type: "int",
                nullable: true);
            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");
            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
