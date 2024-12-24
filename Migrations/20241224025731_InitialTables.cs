using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackAppPersonal.Migrations
{
    /// <inheritdoc />
    public partial class InitialTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Logradouro = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Numero = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Complemento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Cidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UF = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CEP = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CREF = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Especialidades = table.Column<List<string>>(type: "text[]", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoUsuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TIpo = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Academias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EnderecoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Academias_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademiaPersonais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AcademiaId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonalId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademiaPersonais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademiaPersonais_Academias_AcademiaId",
                        column: x => x.AcademiaId,
                        principalTable: "Academias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademiaPersonais_Personais_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "Personais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Senha = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PersonalId = table.Column<Guid>(type: "uuid", nullable: true),
                    TipoUsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    AcademiaId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Academias_AcademiaId",
                        column: x => x.AcademiaId,
                        principalTable: "Academias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuarios_Personais_PersonalId",
                        column: x => x.PersonalId,
                        principalTable: "Personais",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuarios_TipoUsuarios_TipoUsuarioId",
                        column: x => x.TipoUsuarioId,
                        principalTable: "TipoUsuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TipoUsuarios",
                columns: new[] { "Id", "CreatedAt", "TIpo" },
                values: new object[,]
                {
                    { new Guid("140e52dc-929f-447f-b674-46136c4cf632"), new DateTime(2024, 12, 24, 2, 57, 30, 876, DateTimeKind.Utc).AddTicks(4160), "Aluno" },
                    { new Guid("1a60e7ea-0304-4ae4-ad57-f48284c2746b"), new DateTime(2024, 12, 24, 2, 57, 30, 876, DateTimeKind.Utc).AddTicks(4164), "Academia" },
                    { new Guid("21960282-918e-4ce6-ba6c-49efb2c0500e"), new DateTime(2024, 12, 24, 2, 57, 30, 876, DateTimeKind.Utc).AddTicks(4166), "Personal" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademiaPersonais_AcademiaId",
                table: "AcademiaPersonais",
                column: "AcademiaId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademiaPersonais_PersonalId",
                table: "AcademiaPersonais",
                column: "PersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_Academias_EnderecoId",
                table: "Academias",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_AcademiaId",
                table: "Usuarios",
                column: "AcademiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PersonalId",
                table: "Usuarios",
                column: "PersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TipoUsuarioId",
                table: "Usuarios",
                column: "TipoUsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademiaPersonais");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Academias");

            migrationBuilder.DropTable(
                name: "Personais");

            migrationBuilder.DropTable(
                name: "TipoUsuarios");

            migrationBuilder.DropTable(
                name: "Enderecos");
        }
    }
}
