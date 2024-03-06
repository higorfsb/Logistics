using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logistics.Insfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroPedido = table.Column<int>(type: "int", nullable: true),
                    HoraPedido = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IndCancelado = table.Column<bool>(type: "bit", nullable: false),
                    IndConcluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ocorrencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoOcorrencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndFinalizadora = table.Column<bool>(type: "bit", nullable: false),
                    HoraOcorrencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    IdPedidoNavigationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocorrencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ocorrencia_Pedidos_IdPedidoNavigationId",
                        column: x => x.IdPedidoNavigationId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencia_IdPedidoNavigationId",
                table: "Ocorrencia",
                column: "IdPedidoNavigationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ocorrencia");

            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
