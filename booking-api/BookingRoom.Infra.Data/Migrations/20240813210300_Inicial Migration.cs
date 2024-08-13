using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingRoom.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InicialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sala",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sala", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    senha = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tipo_usuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reserva",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    id_usuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    data_criacao_reserva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_sala = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reserva", x => x.id);
                    table.ForeignKey(
                        name: "FK_reserva_sala_id_sala",
                        column: x => x.id_sala,
                        principalTable: "sala",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reserva_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sala_horario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    data = table.Column<DateOnly>(type: "date", nullable: false),
                    hora = table.Column<TimeOnly>(type: "time", nullable: false),
                    id_sala = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    id_reserva = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    reservado = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sala_horario", x => x.id);
                    table.ForeignKey(
                        name: "FK_sala_horario_reserva_id_reserva",
                        column: x => x.id_reserva,
                        principalTable: "reserva",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_sala_horario_sala_id_sala",
                        column: x => x.id_sala,
                        principalTable: "sala",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "sala",
                columns: new[] { "id", "nome" },
                values: new object[,]
                {
                    { new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), "Sala Diretoria" },
                    { new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), "Sala segundo piso" },
                    { new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), "Sala principal" }
                });

            migrationBuilder.InsertData(
                table: "usuario",
                columns: new[] { "id", "email", "nome", "senha", "tipo_usuario" },
                values: new object[,]
                {
                    { new Guid("0f0b085e-7338-4fd0-8700-be5005d29481"), "user1@gmail.com", "Usuário 1", "1234", "admin" },
                    { new Guid("1e4aca3b-688a-4c63-8b9e-79145289ae99"), "admin2@gmail.com", "Administrador 2", "1234", "admin" },
                    { new Guid("7ccb1541-d696-4031-9dbd-01eaa10564e4"), "admin1@gmail.com", "Administrador 1", "1234", "admin" }
                });

            migrationBuilder.InsertData(
                table: "sala_horario",
                columns: new[] { "id", "id_reserva", "data", "reservado", "id_sala", "hora" },
                values: new object[,]
                {
                    { new Guid("00bde81f-219d-4e0e-add3-9c137171803c"), null, new DateOnly(2024, 8, 15), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(11, 0, 0) },
                    { new Guid("029ff878-eaca-47c7-a801-8df41aa90887"), null, new DateOnly(2024, 8, 14), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(9, 0, 0) },
                    { new Guid("04c04c2c-eee4-4c29-b79e-94c0645c1e2b"), null, new DateOnly(2024, 8, 16), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(15, 0, 0) },
                    { new Guid("04f895df-3a92-42bb-af75-c0dea1476e55"), null, new DateOnly(2024, 8, 16), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(12, 0, 0) },
                    { new Guid("0c6dddcf-ea03-4085-882e-3cc677827c38"), null, new DateOnly(2024, 8, 14), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(17, 0, 0) },
                    { new Guid("0cb43083-e986-459c-bd29-4eecb319e7ef"), null, new DateOnly(2024, 8, 14), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(10, 0, 0) },
                    { new Guid("0e477af3-d1fd-424b-aad0-845d751d43ab"), null, new DateOnly(2024, 8, 15), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(11, 0, 0) },
                    { new Guid("152a9f60-2a11-4439-803b-fd8b56a1ecb7"), null, new DateOnly(2024, 8, 12), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(9, 0, 0) },
                    { new Guid("16708e40-4a7a-4c53-a3ed-1b084f24a8de"), null, new DateOnly(2024, 8, 15), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(12, 0, 0) },
                    { new Guid("1b3af216-26fc-4faf-b75f-81c36e9427a2"), null, new DateOnly(2024, 8, 14), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(9, 0, 0) },
                    { new Guid("1b57ddd3-fab3-42f6-af9b-7424a3bd05a5"), null, new DateOnly(2024, 8, 14), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(13, 0, 0) },
                    { new Guid("20919768-77f4-4956-a26d-544a2b4fd031"), null, new DateOnly(2024, 8, 13), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(12, 0, 0) },
                    { new Guid("20ac5589-698f-41bf-9401-c91ae3313474"), null, new DateOnly(2024, 8, 15), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(17, 0, 0) },
                    { new Guid("21e11426-97ec-4785-b343-28aa42d0a8dc"), null, new DateOnly(2024, 8, 16), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(9, 0, 0) },
                    { new Guid("23a48383-b4c6-4f9d-858a-012a6cf49f97"), null, new DateOnly(2024, 8, 12), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(15, 0, 0) },
                    { new Guid("24ef5c1c-4ee0-408d-85b6-27ba88abbf91"), null, new DateOnly(2024, 8, 13), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(17, 0, 0) },
                    { new Guid("27e0cff6-0316-4260-a124-aaa60b4aba92"), null, new DateOnly(2024, 8, 13), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(15, 0, 0) },
                    { new Guid("299860fe-cf74-4c56-b9ea-af7dbaccd7f0"), null, new DateOnly(2024, 8, 15), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(13, 0, 0) },
                    { new Guid("2b3e50d0-2440-4a34-8fa4-a7a6e49a600f"), null, new DateOnly(2024, 8, 13), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(9, 0, 0) },
                    { new Guid("2b5333b6-cb90-4d94-a1aa-411bc3d659d5"), null, new DateOnly(2024, 8, 16), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(14, 0, 0) },
                    { new Guid("2c243df6-9b4d-417c-8cc9-b8afd1721309"), null, new DateOnly(2024, 8, 15), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(10, 0, 0) },
                    { new Guid("2f51b0c6-fd0a-4707-a977-83dfe7723c21"), null, new DateOnly(2024, 8, 12), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(10, 0, 0) },
                    { new Guid("32ba78df-98c2-4a20-87a4-b07354c49181"), null, new DateOnly(2024, 8, 12), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(13, 0, 0) },
                    { new Guid("33c609e0-eaed-4e54-8530-3c77d5530475"), null, new DateOnly(2024, 8, 12), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(16, 0, 0) },
                    { new Guid("34ec23c9-e96f-4865-83ae-0e93ea036a86"), null, new DateOnly(2024, 8, 13), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(14, 0, 0) },
                    { new Guid("387186df-bd52-4ed3-a554-d068eb3c1597"), null, new DateOnly(2024, 8, 13), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(10, 0, 0) },
                    { new Guid("3c05212b-27a0-476f-8200-e27b58b72f87"), null, new DateOnly(2024, 8, 16), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(9, 0, 0) },
                    { new Guid("3c7d9aef-00b9-4392-9f94-d34887cd2f11"), null, new DateOnly(2024, 8, 12), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(15, 0, 0) },
                    { new Guid("3dc3f469-9607-4aa7-983e-47321a502a9e"), null, new DateOnly(2024, 8, 12), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(10, 0, 0) },
                    { new Guid("3df3a2c8-4a3e-46ba-a35a-564a30b7dda6"), null, new DateOnly(2024, 8, 16), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(13, 0, 0) },
                    { new Guid("3e80d442-3d80-410c-8498-fdc1b3905281"), null, new DateOnly(2024, 8, 12), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(15, 0, 0) },
                    { new Guid("3eca9907-7e47-4b75-abcf-4596bc25dba9"), null, new DateOnly(2024, 8, 13), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(9, 0, 0) },
                    { new Guid("41db2b11-48a3-464e-b660-e7a4476c41a1"), null, new DateOnly(2024, 8, 16), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(13, 0, 0) },
                    { new Guid("42412104-617f-4900-b930-901491735fab"), null, new DateOnly(2024, 8, 15), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(16, 0, 0) },
                    { new Guid("437ae329-8529-4ed5-bc03-bb73f7062d69"), null, new DateOnly(2024, 8, 15), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(14, 0, 0) },
                    { new Guid("46e9d01f-64a7-4358-a76d-1d9fe1894bdc"), null, new DateOnly(2024, 8, 13), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(16, 0, 0) },
                    { new Guid("4978628b-8fa0-448d-a60b-8a28f1d9af53"), null, new DateOnly(2024, 8, 14), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(14, 0, 0) },
                    { new Guid("4a99bdd0-b93e-4d82-a9dc-9bb7f60ee100"), null, new DateOnly(2024, 8, 13), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(9, 0, 0) },
                    { new Guid("4b35b34f-b63d-41ae-a502-0f211495b9d6"), null, new DateOnly(2024, 8, 15), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(10, 0, 0) },
                    { new Guid("4cfe15c4-ae08-4552-ba88-7053cee8b0e8"), null, new DateOnly(2024, 8, 14), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(13, 0, 0) },
                    { new Guid("4f27025d-d852-4362-8b03-ee1542577d6e"), null, new DateOnly(2024, 8, 16), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(11, 0, 0) },
                    { new Guid("4fd91eb7-1980-450e-aa70-7b5442f306ae"), null, new DateOnly(2024, 8, 13), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(17, 0, 0) },
                    { new Guid("52c5438e-bf00-4a96-8b38-76c8ecd9abdc"), null, new DateOnly(2024, 8, 12), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(11, 0, 0) },
                    { new Guid("52ebfebe-67aa-4b12-9d8a-f3a5236863fc"), null, new DateOnly(2024, 8, 14), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(10, 0, 0) },
                    { new Guid("5420b11d-e64e-4486-986b-6f9ca1cdb8a2"), null, new DateOnly(2024, 8, 14), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(16, 0, 0) },
                    { new Guid("5815fc99-f108-4fc8-b7a3-f30474d500a0"), null, new DateOnly(2024, 8, 13), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(17, 0, 0) },
                    { new Guid("599d6870-63c1-477d-8621-a4f2a15b97fe"), null, new DateOnly(2024, 8, 12), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(13, 0, 0) },
                    { new Guid("5abcedbb-0749-42a4-aafb-a800ec477737"), null, new DateOnly(2024, 8, 14), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(14, 0, 0) },
                    { new Guid("5bb9e3ca-f76b-4e75-8991-1f7b2e384ddb"), null, new DateOnly(2024, 8, 16), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(10, 0, 0) },
                    { new Guid("625b673b-6c18-4a09-b1ff-a823bccf2e96"), null, new DateOnly(2024, 8, 12), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(9, 0, 0) },
                    { new Guid("62954989-a145-48f4-bfb9-41922f2633b5"), null, new DateOnly(2024, 8, 13), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(12, 0, 0) },
                    { new Guid("6762aeb7-5b00-4406-821f-bac157a5f0fb"), null, new DateOnly(2024, 8, 14), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(11, 0, 0) },
                    { new Guid("67b3b886-b56b-4531-806e-297c4db2467f"), null, new DateOnly(2024, 8, 12), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(13, 0, 0) },
                    { new Guid("6ab17bff-7d25-4246-b157-113ce2d79c48"), null, new DateOnly(2024, 8, 15), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(14, 0, 0) },
                    { new Guid("6b01ffe0-b2ba-4d96-bd8e-cb17319cb2bf"), null, new DateOnly(2024, 8, 16), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(16, 0, 0) },
                    { new Guid("70c1821b-dcc8-4bdc-8a39-5f45619f2104"), null, new DateOnly(2024, 8, 14), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(11, 0, 0) },
                    { new Guid("717a61a9-7149-4859-8932-d21ed2c29440"), null, new DateOnly(2024, 8, 16), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(12, 0, 0) },
                    { new Guid("71d0be96-4489-48c7-b056-6f1d935eaf04"), null, new DateOnly(2024, 8, 12), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(9, 0, 0) },
                    { new Guid("71f7e951-80de-43b3-b3be-bf97cdb18361"), null, new DateOnly(2024, 8, 16), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(16, 0, 0) },
                    { new Guid("731c07f3-0d18-42a2-a972-94ea7369bd32"), null, new DateOnly(2024, 8, 15), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(15, 0, 0) },
                    { new Guid("73619382-aebb-41c6-99d2-19f7925bdb72"), null, new DateOnly(2024, 8, 13), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(10, 0, 0) },
                    { new Guid("73a09c72-64f2-49ce-8a6e-0c850bf9fef0"), null, new DateOnly(2024, 8, 13), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(12, 0, 0) },
                    { new Guid("74c2b1ed-1324-4c67-a7f8-175409245c4e"), null, new DateOnly(2024, 8, 14), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(9, 0, 0) },
                    { new Guid("74e30a93-2d25-4f34-9ef5-15470d92d4cd"), null, new DateOnly(2024, 8, 12), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(12, 0, 0) },
                    { new Guid("76a69806-6a9b-43b2-864b-f1cfa49bd68f"), null, new DateOnly(2024, 8, 12), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(17, 0, 0) },
                    { new Guid("7af83827-ab65-42df-9dc4-dbc371d3ecca"), null, new DateOnly(2024, 8, 16), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(10, 0, 0) },
                    { new Guid("8001f797-5130-454d-9022-a07574e454a8"), null, new DateOnly(2024, 8, 16), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(9, 0, 0) },
                    { new Guid("8407c71e-c96e-43fd-9555-56c8481242c6"), null, new DateOnly(2024, 8, 14), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(16, 0, 0) },
                    { new Guid("863be127-01c1-4d75-845c-5e4f30f4391a"), null, new DateOnly(2024, 8, 15), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(13, 0, 0) },
                    { new Guid("867c2b2d-40fd-4a70-80df-592d37f45a54"), null, new DateOnly(2024, 8, 12), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(17, 0, 0) },
                    { new Guid("89c77159-6611-4502-9de2-ce455860bf35"), null, new DateOnly(2024, 8, 15), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(16, 0, 0) },
                    { new Guid("8e963922-d56e-4fcb-8612-63889221ff0e"), null, new DateOnly(2024, 8, 16), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(14, 0, 0) },
                    { new Guid("92e56069-50ab-49d7-b8e7-16a135ca9ccd"), null, new DateOnly(2024, 8, 16), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(15, 0, 0) },
                    { new Guid("95f1f869-a616-470f-af1b-33bf69087de8"), null, new DateOnly(2024, 8, 14), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(15, 0, 0) },
                    { new Guid("99199413-d127-415f-906e-d0441e45820f"), null, new DateOnly(2024, 8, 12), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(12, 0, 0) },
                    { new Guid("99ab2de3-f7e5-4fa1-95f6-5e5ea2dcc44f"), null, new DateOnly(2024, 8, 13), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(15, 0, 0) },
                    { new Guid("9b38c2d9-c193-4d73-84cb-f7b6e20becdb"), null, new DateOnly(2024, 8, 16), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(14, 0, 0) },
                    { new Guid("9faac794-f704-4c16-a9c5-f793f27818e5"), null, new DateOnly(2024, 8, 15), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(13, 0, 0) },
                    { new Guid("a303fc88-583f-4ecb-9689-5ca9a2e4819b"), null, new DateOnly(2024, 8, 15), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(10, 0, 0) },
                    { new Guid("a3915e94-db99-4fef-91e3-e76fb74ae913"), null, new DateOnly(2024, 8, 15), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(15, 0, 0) },
                    { new Guid("a4e90541-2b6b-4a3f-aa3a-20d58094b4c8"), null, new DateOnly(2024, 8, 14), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(17, 0, 0) },
                    { new Guid("a67ac469-66cf-4f21-b6dd-332e5db8eaf4"), null, new DateOnly(2024, 8, 12), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(11, 0, 0) },
                    { new Guid("a7005176-af63-4225-9762-38450e6f8da9"), null, new DateOnly(2024, 8, 14), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(10, 0, 0) },
                    { new Guid("a7f40db9-d5cc-4c32-b63a-434f8fce5866"), null, new DateOnly(2024, 8, 13), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(11, 0, 0) },
                    { new Guid("a81214ed-ecb7-4fbd-a9d8-67f90d04f11d"), null, new DateOnly(2024, 8, 15), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(17, 0, 0) },
                    { new Guid("a8a7f361-9ac1-4428-8e51-e3cc1b0a6c49"), null, new DateOnly(2024, 8, 13), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(14, 0, 0) },
                    { new Guid("a924db1d-8efb-47d1-a87f-441488ffea43"), null, new DateOnly(2024, 8, 12), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(16, 0, 0) },
                    { new Guid("a9636d5b-2382-4a55-be2b-848115278d00"), null, new DateOnly(2024, 8, 13), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(16, 0, 0) },
                    { new Guid("a9b4bf35-711a-4d81-86b5-1ac515299c7d"), null, new DateOnly(2024, 8, 13), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(13, 0, 0) },
                    { new Guid("aaf53e27-48ac-4933-a556-35ad4ffbd033"), null, new DateOnly(2024, 8, 12), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(11, 0, 0) },
                    { new Guid("ae55c977-d58a-4048-acb1-68a96a05244a"), null, new DateOnly(2024, 8, 13), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(15, 0, 0) },
                    { new Guid("b0b8b533-b677-48fd-bffc-431250ed2125"), null, new DateOnly(2024, 8, 16), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(11, 0, 0) },
                    { new Guid("b8626c53-a6f1-40c0-af79-28c12e68414a"), null, new DateOnly(2024, 8, 13), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(13, 0, 0) },
                    { new Guid("b8fe8959-262b-4dcd-8d07-a0b5f513fe6f"), null, new DateOnly(2024, 8, 14), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(12, 0, 0) },
                    { new Guid("ba922e72-3680-4544-bb21-1635d31195aa"), null, new DateOnly(2024, 8, 16), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(17, 0, 0) },
                    { new Guid("bb771b3a-833d-44f7-9f60-fdd5ddf730e5"), null, new DateOnly(2024, 8, 16), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(16, 0, 0) },
                    { new Guid("bcd1b1f6-6b3a-481f-a94f-ad602a2a601f"), null, new DateOnly(2024, 8, 16), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(12, 0, 0) },
                    { new Guid("be119f09-681d-47fe-8017-28feb30dfc57"), null, new DateOnly(2024, 8, 14), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(17, 0, 0) },
                    { new Guid("bfceca63-64e6-4e2d-b4a6-d97afda43484"), null, new DateOnly(2024, 8, 13), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(16, 0, 0) },
                    { new Guid("c116e0d0-b0e7-4280-846f-77348ad3cfd1"), null, new DateOnly(2024, 8, 13), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(11, 0, 0) },
                    { new Guid("c2db28a2-85bf-4e7d-b16d-0185eabe3224"), null, new DateOnly(2024, 8, 16), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(15, 0, 0) },
                    { new Guid("c3f9412f-4572-4090-bba0-54e14fd0467f"), null, new DateOnly(2024, 8, 16), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(17, 0, 0) },
                    { new Guid("c7079e6b-113e-411a-aeaa-84f0b2238edf"), null, new DateOnly(2024, 8, 14), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(13, 0, 0) },
                    { new Guid("c77bc02e-1e60-4acc-8988-7c813492b73d"), null, new DateOnly(2024, 8, 13), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(11, 0, 0) },
                    { new Guid("c7b5336c-3cfe-44dd-9f71-d4fd9bf35c7d"), null, new DateOnly(2024, 8, 14), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(14, 0, 0) },
                    { new Guid("ca3a59d6-9a0b-48ba-b433-c0de2fe54c1b"), null, new DateOnly(2024, 8, 13), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(10, 0, 0) },
                    { new Guid("cbedd360-7aca-407f-ad13-4f08257b49bf"), null, new DateOnly(2024, 8, 15), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(11, 0, 0) },
                    { new Guid("cfaa91ad-9af6-40ba-a855-5e1ab121f474"), null, new DateOnly(2024, 8, 15), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(9, 0, 0) },
                    { new Guid("d01c3b11-c827-4248-8371-5b4f8142968d"), null, new DateOnly(2024, 8, 12), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(14, 0, 0) },
                    { new Guid("d0755fe6-203d-42bd-9992-9f9c5c4ff44d"), null, new DateOnly(2024, 8, 15), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(14, 0, 0) },
                    { new Guid("d0bf4995-02bc-4e53-bc8d-74e71b39322c"), null, new DateOnly(2024, 8, 14), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(15, 0, 0) },
                    { new Guid("d21f8439-5e5e-44e0-945a-839f447af57a"), null, new DateOnly(2024, 8, 15), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(9, 0, 0) },
                    { new Guid("d50ab286-ce11-4407-9e8b-4ae238192e67"), null, new DateOnly(2024, 8, 16), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(13, 0, 0) },
                    { new Guid("d5a5e943-36d1-4bcb-b94a-b1e3e6599e1a"), null, new DateOnly(2024, 8, 15), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(12, 0, 0) },
                    { new Guid("d5fb2bc6-297d-43b3-ac41-cab80d56593d"), null, new DateOnly(2024, 8, 15), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(12, 0, 0) },
                    { new Guid("d809c965-ed4c-4fea-b397-0771b43b1b4a"), null, new DateOnly(2024, 8, 15), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(17, 0, 0) },
                    { new Guid("dc690654-f505-4167-8e82-2be04fe866d4"), null, new DateOnly(2024, 8, 14), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(11, 0, 0) },
                    { new Guid("e0592ce8-663e-4022-b1f2-5c29f310454c"), null, new DateOnly(2024, 8, 16), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(10, 0, 0) },
                    { new Guid("e0fee99b-fc06-4ae1-8cac-5fe91d7cb3ed"), null, new DateOnly(2024, 8, 16), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(17, 0, 0) },
                    { new Guid("e3f97746-33c6-4916-9a76-2eeae9b28a77"), null, new DateOnly(2024, 8, 14), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(16, 0, 0) },
                    { new Guid("e4a645c3-162f-4e3b-afbe-6719bec9728b"), null, new DateOnly(2024, 8, 14), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(15, 0, 0) },
                    { new Guid("e5e53f2c-7bc9-48d9-8e7b-998fcc3d5d2a"), null, new DateOnly(2024, 8, 13), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(13, 0, 0) },
                    { new Guid("ebf0af6a-0671-417c-ac63-f1aa52938f70"), null, new DateOnly(2024, 8, 15), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(16, 0, 0) },
                    { new Guid("ec53ffc5-d38d-40b5-8df7-af99ec1d7138"), null, new DateOnly(2024, 8, 15), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(9, 0, 0) },
                    { new Guid("f2698c31-bded-482a-8e9a-06200aa384e5"), null, new DateOnly(2024, 8, 16), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(11, 0, 0) },
                    { new Guid("f49dc490-f6d6-4a48-8c5b-43b9ab979867"), null, new DateOnly(2024, 8, 12), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(17, 0, 0) },
                    { new Guid("f589bede-942a-4869-b7ce-04c593e4776f"), null, new DateOnly(2024, 8, 12), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(14, 0, 0) },
                    { new Guid("f61e71dd-8d03-4a82-99f6-a471ee28fb9c"), null, new DateOnly(2024, 8, 15), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(15, 0, 0) },
                    { new Guid("f7e2233c-bb5b-48b7-8d8c-4deff3f18f85"), null, new DateOnly(2024, 8, 12), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(12, 0, 0) },
                    { new Guid("f8b9df4f-6d84-4fd9-ae70-ea8ded51cf3e"), null, new DateOnly(2024, 8, 14), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(12, 0, 0) },
                    { new Guid("f94f9cf5-fb02-4da6-8ab9-c7c0edcc7ced"), null, new DateOnly(2024, 8, 14), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(12, 0, 0) },
                    { new Guid("fa50fdb4-7ea8-4b3c-a693-b8b38b9363c3"), null, new DateOnly(2024, 8, 12), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(14, 0, 0) },
                    { new Guid("fc8d3e6a-f468-4ab0-8287-edd02dfc4eb8"), null, new DateOnly(2024, 8, 12), false, new Guid("774f4fff-8403-4148-a603-6215913c1ca0"), new TimeOnly(16, 0, 0) },
                    { new Guid("fed55217-2596-40c4-b9e2-f8ab8862c505"), null, new DateOnly(2024, 8, 13), false, new Guid("8e0b0640-50ee-43b8-bd01-ac21fb105def"), new TimeOnly(14, 0, 0) },
                    { new Guid("ff64c171-b849-4faa-949c-7b5e320b421a"), null, new DateOnly(2024, 8, 12), false, new Guid("5dfd5139-2283-4335-9b9b-dded0af43961"), new TimeOnly(10, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_reserva_id_sala",
                table: "reserva",
                column: "id_sala");

            migrationBuilder.CreateIndex(
                name: "IX_reserva_id_usuario",
                table: "reserva",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_sala_horario_id_reserva",
                table: "sala_horario",
                column: "id_reserva");

            migrationBuilder.CreateIndex(
                name: "IX_sala_horario_id_sala",
                table: "sala_horario",
                column: "id_sala");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sala_horario");

            migrationBuilder.DropTable(
                name: "reserva");

            migrationBuilder.DropTable(
                name: "sala");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
