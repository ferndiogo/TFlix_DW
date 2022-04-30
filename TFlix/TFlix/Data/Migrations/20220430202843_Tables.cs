using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFlix.Data.Migrations
{
    public partial class Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filmes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sinopse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diretor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Atores = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Género = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NIF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Morada = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aluguers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilizadorFK = table.Column<int>(type: "int", nullable: false),
                    FilmeFK = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<double>(type: "float", nullable: false),
                    TempoAluguer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluguers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aluguers_Filmes_FilmeFK",
                        column: x => x.FilmeFK,
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Aluguers_Utilizadores_UtilizadorFK",
                        column: x => x.UtilizadorFK,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gestao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminFK = table.Column<int>(type: "int", nullable: false),
                    UtilizadorFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gestao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gestao_Admins_AdminFK",
                        column: x => x.AdminFK,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gestao_Utilizadores_UtilizadorFK",
                        column: x => x.UtilizadorFK,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscricoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilizadorFK = table.Column<int>(type: "int", nullable: false),
                    Duracao = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<double>(type: "float", nullable: false),
                    DataSub = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscricoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscricoes_Utilizadores_UtilizadorFK",
                        column: x => x.UtilizadorFK,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sinopse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diretor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Atores = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Género = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscricaoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Series_Subscricoes_SubscricaoId",
                        column: x => x.SubscricaoId,
                        principalTable: "Subscricoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubFilme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubcricaoFK = table.Column<int>(type: "int", nullable: false),
                    SubcricaoId = table.Column<int>(type: "int", nullable: false),
                    FilmeFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubFilme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubFilme_Filmes_FilmeFK",
                        column: x => x.FilmeFK,
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubFilme_Subscricoes_SubcricaoId",
                        column: x => x.SubcricaoId,
                        principalTable: "Subscricoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubSerie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubcricaoFK = table.Column<int>(type: "int", nullable: false),
                    SubcricaoId = table.Column<int>(type: "int", nullable: false),
                    SerieFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSerie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubSerie_Series_SerieFK",
                        column: x => x.SerieFK,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubSerie_Subscricoes_SubcricaoId",
                        column: x => x.SubcricaoId,
                        principalTable: "Subscricoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluguers_FilmeFK",
                table: "Aluguers",
                column: "FilmeFK");

            migrationBuilder.CreateIndex(
                name: "IX_Aluguers_UtilizadorFK",
                table: "Aluguers",
                column: "UtilizadorFK");

            migrationBuilder.CreateIndex(
                name: "IX_Gestao_AdminFK",
                table: "Gestao",
                column: "AdminFK");

            migrationBuilder.CreateIndex(
                name: "IX_Gestao_UtilizadorFK",
                table: "Gestao",
                column: "UtilizadorFK");

            migrationBuilder.CreateIndex(
                name: "IX_Series_SubscricaoId",
                table: "Series",
                column: "SubscricaoId");

            migrationBuilder.CreateIndex(
                name: "IX_SubFilme_FilmeFK",
                table: "SubFilme",
                column: "FilmeFK");

            migrationBuilder.CreateIndex(
                name: "IX_SubFilme_SubcricaoId",
                table: "SubFilme",
                column: "SubcricaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscricoes_UtilizadorFK",
                table: "Subscricoes",
                column: "UtilizadorFK");

            migrationBuilder.CreateIndex(
                name: "IX_SubSerie_SerieFK",
                table: "SubSerie",
                column: "SerieFK");

            migrationBuilder.CreateIndex(
                name: "IX_SubSerie_SubcricaoId",
                table: "SubSerie",
                column: "SubcricaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aluguers");

            migrationBuilder.DropTable(
                name: "Gestao");

            migrationBuilder.DropTable(
                name: "SubFilme");

            migrationBuilder.DropTable(
                name: "SubSerie");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Filmes");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Subscricoes");

            migrationBuilder.DropTable(
                name: "Utilizadores");
        }
    }
}
