using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeaStyleOficial.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Administradores",
                columns: new[] { "UsuarioId", "Cargo", "Email", "Nome", "Senha" },
                values: new object[] { 1L, "Gerente", "mariaclara4290@gmail.com", "Admin Master", "admin123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Administradores",
                keyColumn: "UsuarioId",
                keyValue: 1L);
        }
    }
}
