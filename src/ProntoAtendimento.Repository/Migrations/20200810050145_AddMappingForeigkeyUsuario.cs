using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProntoAtendimento.Repository.Migrations
{
    public partial class AddMappingForeigkeyUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ocorrencia_Usuario_Id",
                table: "Ocorrencia");

            migrationBuilder.DropForeignKey(
                name: "FK_Tramite_Usuario_Id",
                table: "Tramite");

            migrationBuilder.DropForeignKey(
                name: "FK_Turno_Usuario_Id",
                table: "Turno");

            migrationBuilder.UpdateData(
                table: "Ativo",
                keyColumn: "Id",
                keyValue: new Guid("6c2b2734-ea6b-4c00-afc1-75f06de09eee"),
                columns: new[] { "Data_Hora_Cadastro", "Data_Hora_Ultima_Atualizacao" },
                values: new object[] { new DateTime(2020, 8, 10, 2, 1, 44, 0, DateTimeKind.Unspecified), new DateTime(2020, 8, 10, 2, 1, 44, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Perfil",
                keyColumn: "Id",
                keyValue: new Guid("0cc9ce69-e164-4435-9e96-b59877366973"),
                column: "ConcurrencyStamp",
                value: "7d819348-5723-42f2-80b8-ffb5bd984c61");

            migrationBuilder.UpdateData(
                table: "Perfil",
                keyColumn: "Id",
                keyValue: new Guid("8205cf8b-fb97-449d-8810-69cbfa1dacf7"),
                column: "ConcurrencyStamp",
                value: "460a9cd7-221b-411a-88c3-092f6b43e659");

            migrationBuilder.UpdateData(
                table: "Perfil",
                keyColumn: "Id",
                keyValue: new Guid("a1ba9685-8860-46d6-b6e5-a72ce3965970"),
                column: "ConcurrencyStamp",
                value: "034217d3-edc8-49b7-80b9-9cc35018308b");

            migrationBuilder.UpdateData(
                table: "Perfil",
                keyColumn: "Id",
                keyValue: new Guid("c32b2335-2166-4342-a7cb-ad37c47b9b02"),
                column: "ConcurrencyStamp",
                value: "d84419f4-a8a5-42c3-8687-68d58d79195a");

            migrationBuilder.UpdateData(
                table: "Perfil",
                keyColumn: "Id",
                keyValue: new Guid("d289114a-f651-43a7-956b-feebbd6d85cc"),
                column: "ConcurrencyStamp",
                value: "3ead4919-f9a9-4a37-b0dd-231930880cca");

            migrationBuilder.UpdateData(
                table: "Perfil",
                keyColumn: "Id",
                keyValue: new Guid("fedf42c6-7c71-4b96-83eb-05ceaa7a6bbc"),
                column: "ConcurrencyStamp",
                value: "62aaa8a0-5e2e-48d4-bd83-a6ff02de0fb2");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: new Guid("db8ca864-3473-4fa1-bfee-55440dd56ebf"),
                columns: new[] { "ConcurrencyStamp", "DataHoraCadastro", "LockoutEnabled", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c1462f5-0f0a-489e-8361-c804dc2d3b6f", new DateTime(2020, 8, 10, 2, 1, 44, 981, DateTimeKind.Local).AddTicks(3060), true, "AQAAAAEAACcQAAAAEA70SoMDl8QD9nAYq9iEptyLgO0ZppgYtqpby4fWRdio6T167P/SWvswC3K8QA4BSg==", "1b044ca3-d60f-4612-84fb-e98724599d9c" });

            migrationBuilder.CreateIndex(
                name: "IX_Turno_Usuario_Id",
                table: "Turno",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tramite_Usuario_Id",
                table: "Tramite",
                column: "Usuario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ocorrencia_Usuario_Id",
                table: "Ocorrencia",
                column: "Usuario_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ocorrencia_Usuario_Usuario_Id",
                table: "Ocorrencia",
                column: "Usuario_Id",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tramite_Usuario_Usuario_Id",
                table: "Tramite",
                column: "Usuario_Id",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turno_Usuario_Usuario_Id",
                table: "Turno",
                column: "Usuario_Id",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ocorrencia_Usuario_Usuario_Id",
                table: "Ocorrencia");

            migrationBuilder.DropForeignKey(
                name: "FK_Tramite_Usuario_Usuario_Id",
                table: "Tramite");

            migrationBuilder.DropForeignKey(
                name: "FK_Turno_Usuario_Usuario_Id",
                table: "Turno");

            migrationBuilder.DropIndex(
                name: "IX_Turno_Usuario_Id",
                table: "Turno");

            migrationBuilder.DropIndex(
                name: "IX_Tramite_Usuario_Id",
                table: "Tramite");

            migrationBuilder.DropIndex(
                name: "IX_Ocorrencia_Usuario_Id",
                table: "Ocorrencia");

            migrationBuilder.UpdateData(
                table: "Ativo",
                keyColumn: "Id",
                keyValue: new Guid("6c2b2734-ea6b-4c00-afc1-75f06de09eee"),
                columns: new[] { "Data_Hora_Cadastro", "Data_Hora_Ultima_Atualizacao" },
                values: new object[] { new DateTime(2020, 8, 3, 23, 11, 25, 421, DateTimeKind.Local).AddTicks(8), new DateTime(2020, 8, 3, 23, 11, 25, 421, DateTimeKind.Local).AddTicks(8335) });

            migrationBuilder.UpdateData(
                table: "Perfil",
                keyColumn: "Id",
                keyValue: new Guid("0cc9ce69-e164-4435-9e96-b59877366973"),
                column: "ConcurrencyStamp",
                value: "66181dee-3d39-4709-acba-d1d349f93cad");

            migrationBuilder.UpdateData(
                table: "Perfil",
                keyColumn: "Id",
                keyValue: new Guid("8205cf8b-fb97-449d-8810-69cbfa1dacf7"),
                column: "ConcurrencyStamp",
                value: "ab42ff38-0ecf-4674-8bde-4782b0d855c5");

            migrationBuilder.UpdateData(
                table: "Perfil",
                keyColumn: "Id",
                keyValue: new Guid("a1ba9685-8860-46d6-b6e5-a72ce3965970"),
                column: "ConcurrencyStamp",
                value: "276664af-d3eb-410c-bffd-d560382bd1ed");

            migrationBuilder.UpdateData(
                table: "Perfil",
                keyColumn: "Id",
                keyValue: new Guid("c32b2335-2166-4342-a7cb-ad37c47b9b02"),
                column: "ConcurrencyStamp",
                value: "c3c72eb9-d231-456e-af6a-66e5f4a33a61");

            migrationBuilder.UpdateData(
                table: "Perfil",
                keyColumn: "Id",
                keyValue: new Guid("d289114a-f651-43a7-956b-feebbd6d85cc"),
                column: "ConcurrencyStamp",
                value: "e6e24243-9498-4a9b-800a-03acb5989d05");

            migrationBuilder.UpdateData(
                table: "Perfil",
                keyColumn: "Id",
                keyValue: new Guid("fedf42c6-7c71-4b96-83eb-05ceaa7a6bbc"),
                column: "ConcurrencyStamp",
                value: "84e9069f-78cc-468b-8107-997f243df17c");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: new Guid("db8ca864-3473-4fa1-bfee-55440dd56ebf"),
                columns: new[] { "ConcurrencyStamp", "DataHoraCadastro", "LockoutEnabled", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c448bf97-ef06-4873-86e0-827f77c88e25", new DateTime(2020, 8, 3, 23, 11, 25, 459, DateTimeKind.Local).AddTicks(2098), false, "AQAAAAEAACcQAAAAEN66dpM+Jr6hPmbzirMVrdjpjG1aHlIu6jmYz6ObBsiVKfGn55kylDtRd5umObHX/g==", "94191e6e-f82e-47bf-8522-a8445b401183" });

            migrationBuilder.AddForeignKey(
                name: "FK_Ocorrencia_Usuario_Id",
                table: "Ocorrencia",
                column: "Id",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tramite_Usuario_Id",
                table: "Tramite",
                column: "Id",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turno_Usuario_Id",
                table: "Turno",
                column: "Id",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
