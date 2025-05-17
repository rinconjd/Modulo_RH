using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modulo_RH.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioIdToEmpleado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Empleados",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_UsuarioId",
                table: "Empleados",
                column: "UsuarioId",
                unique: true,
                filter: "[UsuarioId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Usuarios_UsuarioId",
                table: "Empleados",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Usuarios_UsuarioId",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_UsuarioId",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Empleados");
        }
    }
}
