using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProntoAtendimento.Repository.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perfil",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfil", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Inc_Setor = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 480, nullable: false),
                    Coordenacao = table.Column<string>(maxLength: 480, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setor", x => x.Id);
                    table.UniqueConstraint("Inc_Setor", x => x.Inc_Setor);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(120)", nullable: true),
                    StatusUsuario = table.Column<int>(type: "int(1)", nullable: false),
                    DataHoraCadastro = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_Perfil_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ativo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Inc_Ativo = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 480, nullable: false),
                    Descricao = table.Column<string>(maxLength: 6000, nullable: false),
                    Data_Hora_Cadastro = table.Column<DateTime>(nullable: false),
                    Data_Hora_Ultima_Atualizacao = table.Column<DateTime>(nullable: false),
                    Status_Ativo = table.Column<int>(nullable: false),
                    Criticidade = table.Column<int>(maxLength: 1, nullable: false),
                    Setor_Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ativo", x => x.Id);
                    table.UniqueConstraint("Inc_Ativo", x => x.Inc_Ativo);
                    table.ForeignKey(
                        name: "FK_Ativo_Setor_Setor_Id",
                        column: x => x.Setor_Id,
                        principalTable: "Setor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Usuario_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Usuario_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Usuario_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ocorrencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Inc_Ocorrencia = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(maxLength: 300, nullable: false),
                    Data_Hora_Inicio = table.Column<DateTime>(maxLength: 120, nullable: false),
                    Data_Hora_Fim = table.Column<DateTime>(nullable: true),
                    Data_Hora_Ultima_Atualizacao = table.Column<DateTime>(nullable: false),
                    Chamado_TI = table.Column<string>(maxLength: 30, nullable: true),
                    Chamado_Fornecedor = table.Column<string>(maxLength: 60, nullable: true),
                    Ocorrencia_CCM = table.Column<string>(maxLength: 30, nullable: true),
                    Acionamento = table.Column<bool>(nullable: false),
                    Impacto = table.Column<bool>(nullable: false),
                    Descricao_Impacto = table.Column<string>(maxLength: 3600, nullable: true),
                    Status_Atual_Ocorrencia = table.Column<int>(maxLength: 1, nullable: false),
                    Usuario_Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocorrencia", x => x.Id);
                    table.UniqueConstraint("Inc_Ocorrencia", x => x.Inc_Ocorrencia);
                    table.ForeignKey(
                        name: "FK_Ocorrencia_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turno",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Inc_Turno = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data_Hora_Inicio = table.Column<DateTime>(maxLength: 120, nullable: false),
                    Data_Hora_Fim = table.Column<DateTime>(nullable: false),
                    Status_Turno = table.Column<int>(maxLength: 1, nullable: false),
                    Usuario_Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno", x => x.Id);
                    table.UniqueConstraint("Inc_Turno", x => x.Inc_Turno);
                    table.ForeignKey(
                        name: "FK_Turno_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioPerfil",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPerfil", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UsuarioPerfil_Perfil_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioPerfil_Usuario_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ocorrencia_Ativo",
                columns: table => new
                {
                    Ocorrencia_Id = table.Column<Guid>(nullable: false),
                    Ativo_Id = table.Column<Guid>(nullable: false),
                    Principal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocorrencia_Ativo", x => new { x.Ocorrencia_Id, x.Ativo_Id });
                    table.ForeignKey(
                        name: "FK_Ocorrencia_Ativo_Ativo_Ativo_Id",
                        column: x => x.Ativo_Id,
                        principalTable: "Ativo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ocorrencia_Ativo_Ocorrencia_Ocorrencia_Id",
                        column: x => x.Ocorrencia_Id,
                        principalTable: "Ocorrencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tramite",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Inc_Tramite = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(maxLength: 6000, nullable: false),
                    Data_Hora = table.Column<DateTime>(nullable: false),
                    Solucao = table.Column<bool>(nullable: false),
                    Ocorrencia_Id = table.Column<Guid>(nullable: false),
                    Usuario_Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tramite", x => x.Id);
                    table.UniqueConstraint("Inc_Tramite", x => x.Inc_Tramite);
                    table.ForeignKey(
                        name: "FK_Tramite_Ocorrencia_Id",
                        column: x => x.Id,
                        principalTable: "Ocorrencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tramite_Usuario_Id",
                        column: x => x.Id,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turno_Ocorrencia",
                columns: table => new
                {
                    Turno_Id = table.Column<Guid>(nullable: false),
                    Ocorrencia_Id = table.Column<Guid>(nullable: false),
                    Status_Turno_Ocorrencia = table.Column<int>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno_Ocorrencia", x => new { x.Turno_Id, x.Ocorrencia_Id });
                    table.ForeignKey(
                        name: "FK_Turno_Ocorrencia_Ocorrencia_Ocorrencia_Id",
                        column: x => x.Ocorrencia_Id,
                        principalTable: "Ocorrencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turno_Ocorrencia_Turno_Turno_Id",
                        column: x => x.Turno_Id,
                        principalTable: "Turno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Perfil",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("c32b2335-2166-4342-a7cb-ad37c47b9b02"), "c3c72eb9-d231-456e-af6a-66e5f4a33a61", "ADMIN", "ADMIN" },
                    { new Guid("a1ba9685-8860-46d6-b6e5-a72ce3965970"), "276664af-d3eb-410c-bffd-d560382bd1ed", "ADMIN_TI", "ADMIN_TI" },
                    { new Guid("8205cf8b-fb97-449d-8810-69cbfa1dacf7"), "ab42ff38-0ecf-4674-8bde-4782b0d855c5", "CONSULTA", "CONSULTA" },
                    { new Guid("d289114a-f651-43a7-956b-feebbd6d85cc"), "e6e24243-9498-4a9b-800a-03acb5989d05", "DEV", "DEV" },
                    { new Guid("fedf42c6-7c71-4b96-83eb-05ceaa7a6bbc"), "84e9069f-78cc-468b-8107-997f243df17c", "PA", "PA" },
                    { new Guid("0cc9ce69-e164-4435-9e96-b59877366973"), "66181dee-3d39-4709-acba-d1d349f93cad", "TI", "TI" }
                });

            migrationBuilder.InsertData(
                table: "Setor",
                columns: new[] { "Id", "Coordenacao", "Inc_Setor", "Nome" },
                values: new object[] { new Guid("dd84e876-d3d7-418b-85e0-5aee403a2ceb"), "PADRAO", 1000, "PADRAO" });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DataHoraCadastro", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NomeCompleto", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StatusUsuario", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("db8ca864-3473-4fa1-bfee-55440dd56ebf"), 0, "c448bf97-ef06-4873-86e0-827f77c88e25", new DateTime(2020, 8, 3, 23, 11, 25, 459, DateTimeKind.Local).AddTicks(2098), "grupo.operacao@mrs.com.br", true, false, null, "ADMIN_I", "GRUPO.OPERACAO@MRS.COM.BR", "11111111", "AQAAAAEAACcQAAAAEN66dpM+Jr6hPmbzirMVrdjpjG1aHlIu6jmYz6ObBsiVKfGn55kylDtRd5umObHX/g==", null, false, "94191e6e-f82e-47bf-8522-a8445b401183", 1, false, "11111111" });

            migrationBuilder.InsertData(
                table: "Ativo",
                columns: new[] { "Id", "Criticidade", "Data_Hora_Cadastro", "Data_Hora_Ultima_Atualizacao", "Descricao", "Inc_Ativo", "Nome", "Setor_Id", "Status_Ativo" },
                values: new object[] { new Guid("6c2b2734-ea6b-4c00-afc1-75f06de09eee"), 0, new DateTime(2020, 8, 3, 23, 11, 25, 421, DateTimeKind.Local).AddTicks(8), new DateTime(2020, 8, 3, 23, 11, 25, 421, DateTimeKind.Local).AddTicks(8335), "PADRAO", 1000, "PADRAO", new Guid("dd84e876-d3d7-418b-85e0-5aee403a2ceb"), 1 });

            migrationBuilder.InsertData(
                table: "UsuarioPerfil",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("db8ca864-3473-4fa1-bfee-55440dd56ebf"), new Guid("c32b2335-2166-4342-a7cb-ad37c47b9b02") });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ativo_Setor_Id",
                table: "Ativo",
                column: "Setor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencia_Ativo_Ativo_Id",
                table: "Ocorrencia_Ativo",
                column: "Ativo_Id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Perfil",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turno_Ocorrencia_Ocorrencia_Id",
                table: "Turno_Ocorrencia",
                column: "Ocorrencia_Id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Usuario",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Usuario",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPerfil_RoleId",
                table: "UsuarioPerfil",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Ocorrencia_Ativo");

            migrationBuilder.DropTable(
                name: "Tramite");

            migrationBuilder.DropTable(
                name: "Turno_Ocorrencia");

            migrationBuilder.DropTable(
                name: "UsuarioPerfil");

            migrationBuilder.DropTable(
                name: "Ativo");

            migrationBuilder.DropTable(
                name: "Ocorrencia");

            migrationBuilder.DropTable(
                name: "Turno");

            migrationBuilder.DropTable(
                name: "Perfil");

            migrationBuilder.DropTable(
                name: "Setor");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
