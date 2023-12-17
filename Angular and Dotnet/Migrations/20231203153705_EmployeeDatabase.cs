using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Angular_and_Dotnet.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsNarried",
                table: "Employees",
                newName: "IsMarried");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMarried",
                table: "Employees",
                newName: "IsNarried");
        }
    }
}
