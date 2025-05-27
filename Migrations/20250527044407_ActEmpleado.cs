using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modulo_RH.Migrations
{
    /// <inheritdoc />
    public partial class ActEmpleado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Empleados");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "Empleados",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
