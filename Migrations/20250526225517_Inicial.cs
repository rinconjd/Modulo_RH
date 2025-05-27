using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modulo_RH.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cargo",
                table: "Empleados",
                newName: "Rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rol",
                table: "Empleados",
                newName: "Cargo");
        }
    }
}
