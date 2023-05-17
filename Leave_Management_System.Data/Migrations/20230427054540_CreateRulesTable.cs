using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace Leave_Management_System.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateRulesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    Band = table.Column<int>(type: "int", nullable: false),
                    DefaultBalance = table.Column<double>(type: "float", nullable: false),
                    Credit = table.Column<double>(type: "float", nullable: false),
                    LeaveCreditFrequency = table.Column<int>(type: "int", nullable: false),
                    AllowedLeaves = table.Column<int>(type: "int", nullable: false),
                    IsApplicable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rules_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateIndex(
                name: "IX_Rules_LeaveTypeId",
                table: "Rules",
                column: "LeaveTypeId");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rules");
        }
    }
}
