using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingRoom.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InicitalMigrate : Migration
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
                    { new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), "Sala Diretoria" },
                    { new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), "Sala segundo piso" },
                    { new Guid("542eb137-873f-4920-88e3-b7401c36385f"), "Sala principal" }
                });

            migrationBuilder.InsertData(
                table: "usuario",
                columns: new[] { "id", "email", "nome", "senha", "tipo_usuario" },
                values: new object[,]
                {
                    { new Guid("689fe36b-0df8-4f45-aeb5-f5745fd854a6"), "admin1@gmail.com", "Administrador 1", "1234", "admin" },
                    { new Guid("6ceafafc-1356-48d4-a8c3-0921bcd0d055"), "user1@gmail.com", "Usuário 1", "1234", "user" },
                    { new Guid("d45b2219-db67-4442-86e8-9b30b743fe44"), "admin2@gmail.com", "Administrador 2", "1234", "admin" }
                });

            migrationBuilder.InsertData(
                table: "sala_horario",
                columns: new[] { "id", "id_reserva", "data", "reservado", "id_sala", "hora" },
                values: new object[,]
                {
                    { new Guid("01589429-ed27-4fa5-b1a2-0829079222e4"), null, new DateOnly(2024, 8, 13), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(16, 0, 0) },
                    { new Guid("02d7b9be-3212-4da4-b5f2-019717f0a5e0"), null, new DateOnly(2024, 8, 13), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(15, 0, 0) },
                    { new Guid("02fb66a0-424d-4bdf-9e49-3f888ac517a3"), null, new DateOnly(2024, 8, 16), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(14, 0, 0) },
                    { new Guid("040ea48b-7c21-484f-8f40-b252aab49b9f"), null, new DateOnly(2024, 8, 14), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(15, 0, 0) },
                    { new Guid("047ccc3b-28d2-44de-a3d9-d1d361aaa2c4"), null, new DateOnly(2024, 8, 14), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(12, 0, 0) },
                    { new Guid("06ef6a51-b1be-42d1-8598-d45275ede8bb"), null, new DateOnly(2024, 8, 12), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(17, 0, 0) },
                    { new Guid("074a9a8a-21c4-46b0-8409-77d35ce93ebb"), null, new DateOnly(2024, 8, 13), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(10, 0, 0) },
                    { new Guid("07a84edf-7995-46ed-aab2-77ade7c91f2f"), null, new DateOnly(2024, 8, 15), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(10, 0, 0) },
                    { new Guid("0e7c8b63-9dc5-4dfe-a3f7-51af49e06cda"), null, new DateOnly(2024, 8, 14), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(11, 0, 0) },
                    { new Guid("0fcb2dba-2130-44a0-befd-a68a76174f89"), null, new DateOnly(2024, 8, 13), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(12, 0, 0) },
                    { new Guid("10b1ce8d-c49d-4e66-b568-126a45d51975"), null, new DateOnly(2024, 8, 15), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(9, 0, 0) },
                    { new Guid("13987494-a433-4b04-83f3-30f45e39961f"), null, new DateOnly(2024, 8, 15), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(15, 0, 0) },
                    { new Guid("143f12d4-9bd6-498f-bdb9-5779f472581e"), null, new DateOnly(2024, 8, 16), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(17, 0, 0) },
                    { new Guid("17b9c655-bbe0-41f9-bdc7-f95b0040f8c6"), null, new DateOnly(2024, 8, 14), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(10, 0, 0) },
                    { new Guid("199be57b-85ed-4488-9800-c44d05b6b871"), null, new DateOnly(2024, 8, 15), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(13, 0, 0) },
                    { new Guid("1b90a4eb-c6b0-467d-8cb6-6c8487557d8f"), null, new DateOnly(2024, 8, 12), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(17, 0, 0) },
                    { new Guid("1cb35c78-cbd6-4559-a6ea-96bc3634b2de"), null, new DateOnly(2024, 8, 13), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(14, 0, 0) },
                    { new Guid("1ecbf4b5-c2f3-4144-8d2e-41716becca2c"), null, new DateOnly(2024, 8, 14), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(14, 0, 0) },
                    { new Guid("1ee2d595-0a79-4e5b-a99d-a12639180424"), null, new DateOnly(2024, 8, 12), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(17, 0, 0) },
                    { new Guid("248d8ce2-638d-4b8c-8cfe-c134f025d5da"), null, new DateOnly(2024, 8, 12), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(12, 0, 0) },
                    { new Guid("2544c308-e493-4b39-9d7f-03f70360fd2c"), null, new DateOnly(2024, 8, 13), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(13, 0, 0) },
                    { new Guid("26022889-ead9-4f48-a9cd-a0b08381f726"), null, new DateOnly(2024, 8, 16), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(10, 0, 0) },
                    { new Guid("26d76b31-932a-4393-8d12-6f1bd93e886b"), null, new DateOnly(2024, 8, 16), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(11, 0, 0) },
                    { new Guid("278cc29c-d844-465b-981c-e3422dcb1754"), null, new DateOnly(2024, 8, 16), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(15, 0, 0) },
                    { new Guid("28983191-3dd0-450c-8d22-64e18aac82e2"), null, new DateOnly(2024, 8, 16), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(12, 0, 0) },
                    { new Guid("2a294a39-be5e-407e-b1b4-39d0f0f2bfe0"), null, new DateOnly(2024, 8, 12), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(10, 0, 0) },
                    { new Guid("2ab467ec-bb0c-4344-8c16-be7eead3ca2c"), null, new DateOnly(2024, 8, 16), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(12, 0, 0) },
                    { new Guid("2af19195-0f8e-4193-bcaf-ab7c62457fa1"), null, new DateOnly(2024, 8, 14), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(16, 0, 0) },
                    { new Guid("2c0da222-bd67-4403-bbc1-3d4f0ac1014c"), null, new DateOnly(2024, 8, 12), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(10, 0, 0) },
                    { new Guid("2ca12906-9189-48ba-b0bd-38f7668b6c57"), null, new DateOnly(2024, 8, 15), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(14, 0, 0) },
                    { new Guid("2e56e769-e4c4-4c87-a165-c1df3423fe89"), null, new DateOnly(2024, 8, 14), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(10, 0, 0) },
                    { new Guid("32487588-cba7-4121-94f8-92bdac232479"), null, new DateOnly(2024, 8, 16), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(13, 0, 0) },
                    { new Guid("329c1ecb-3292-4d96-85d0-aabfb261a4f5"), null, new DateOnly(2024, 8, 12), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(15, 0, 0) },
                    { new Guid("3510d620-a6fc-4f62-b318-f51f022eec79"), null, new DateOnly(2024, 8, 13), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(16, 0, 0) },
                    { new Guid("365f492f-292b-418f-97b8-4353b9a386b5"), null, new DateOnly(2024, 8, 14), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(16, 0, 0) },
                    { new Guid("383ec9fe-e023-4468-bd7b-0b05239194df"), null, new DateOnly(2024, 8, 13), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(13, 0, 0) },
                    { new Guid("3c04525c-410b-4dfd-b3a0-9798a9017673"), null, new DateOnly(2024, 8, 12), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(9, 0, 0) },
                    { new Guid("3cd185c6-3da5-4cec-831b-e53d8afb26a4"), null, new DateOnly(2024, 8, 14), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(17, 0, 0) },
                    { new Guid("3dfa33e5-46f6-44a0-baf4-7f734982c34e"), null, new DateOnly(2024, 8, 13), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(9, 0, 0) },
                    { new Guid("408e6aa3-95f4-488b-bf82-9a0ac6edf6bb"), null, new DateOnly(2024, 8, 15), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(17, 0, 0) },
                    { new Guid("41de8e66-d8d8-4d2e-b923-7a48dbafe775"), null, new DateOnly(2024, 8, 14), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(13, 0, 0) },
                    { new Guid("432fb754-d8c9-4cd1-9a16-8df658145221"), null, new DateOnly(2024, 8, 12), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(13, 0, 0) },
                    { new Guid("4339dcfc-38d8-4844-82dc-41c2ba332b4e"), null, new DateOnly(2024, 8, 16), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(11, 0, 0) },
                    { new Guid("43b123f2-fa47-4ad0-92be-73092cf7175c"), null, new DateOnly(2024, 8, 12), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(9, 0, 0) },
                    { new Guid("47579a70-55e8-4fa8-9153-53a167935a32"), null, new DateOnly(2024, 8, 13), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(14, 0, 0) },
                    { new Guid("491a08cf-2baf-4286-a50d-d8fdf72bb65f"), null, new DateOnly(2024, 8, 16), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(15, 0, 0) },
                    { new Guid("4a3335d5-1dd6-4add-bbec-a427b34b0fc1"), null, new DateOnly(2024, 8, 16), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(16, 0, 0) },
                    { new Guid("5281833a-fb13-4726-a8e6-955aa6d4f7a4"), null, new DateOnly(2024, 8, 16), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(16, 0, 0) },
                    { new Guid("55c2ea40-3e12-4aa9-86eb-c332bfdfe155"), null, new DateOnly(2024, 8, 13), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(12, 0, 0) },
                    { new Guid("56fea3c7-6b6d-41ea-8ff2-59ce068c41ba"), null, new DateOnly(2024, 8, 14), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(12, 0, 0) },
                    { new Guid("572b9f28-4d00-4354-b293-e6f65cfeac1b"), null, new DateOnly(2024, 8, 12), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(11, 0, 0) },
                    { new Guid("597fb3e7-0860-4ce5-8e1a-fc8dd653b870"), null, new DateOnly(2024, 8, 15), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(17, 0, 0) },
                    { new Guid("5a9fcfe6-bc31-4d34-9b9d-071060042d30"), null, new DateOnly(2024, 8, 14), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(16, 0, 0) },
                    { new Guid("63781244-e851-451d-92c5-0fb3328b4501"), null, new DateOnly(2024, 8, 13), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(10, 0, 0) },
                    { new Guid("66c965b6-1fdd-4b07-bb3c-cf062265893c"), null, new DateOnly(2024, 8, 12), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(12, 0, 0) },
                    { new Guid("6b2fbeaa-2ad2-4933-ba60-9d2a4802365f"), null, new DateOnly(2024, 8, 13), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(16, 0, 0) },
                    { new Guid("6b39ff8f-7924-4e40-a9e7-964535a8fa88"), null, new DateOnly(2024, 8, 14), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(9, 0, 0) },
                    { new Guid("6ddf960a-22d4-49a4-88f1-8e800ab234eb"), null, new DateOnly(2024, 8, 14), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(9, 0, 0) },
                    { new Guid("6e01ca58-e382-40c6-8623-f6393b16d578"), null, new DateOnly(2024, 8, 16), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(13, 0, 0) },
                    { new Guid("710102d1-26fd-4dab-ac4a-d7fd3f2879ec"), null, new DateOnly(2024, 8, 13), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(10, 0, 0) },
                    { new Guid("74f2aae7-96dd-41a9-8bc4-cb2700cc728d"), null, new DateOnly(2024, 8, 15), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(12, 0, 0) },
                    { new Guid("757f91a6-e317-4b76-8a10-f52d29b5423e"), null, new DateOnly(2024, 8, 16), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(11, 0, 0) },
                    { new Guid("793b8ab6-c44c-4536-8b9c-117fb7327f65"), null, new DateOnly(2024, 8, 15), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(16, 0, 0) },
                    { new Guid("7c0b53a6-3e24-4d0a-a3a1-e940d74484df"), null, new DateOnly(2024, 8, 14), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(15, 0, 0) },
                    { new Guid("7e1f5ba6-ef9b-4168-bf44-f9018157002b"), null, new DateOnly(2024, 8, 16), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(13, 0, 0) },
                    { new Guid("859c765d-05df-4ded-bf47-e7996392d585"), null, new DateOnly(2024, 8, 15), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(16, 0, 0) },
                    { new Guid("8752d88a-4619-41d2-9265-9d5e1c80c39e"), null, new DateOnly(2024, 8, 13), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(15, 0, 0) },
                    { new Guid("88e637f0-8fb0-4b13-8365-dfc731c1df48"), null, new DateOnly(2024, 8, 16), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(10, 0, 0) },
                    { new Guid("88e811e6-1e70-434d-aec6-9554f9060661"), null, new DateOnly(2024, 8, 15), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(14, 0, 0) },
                    { new Guid("8af5973d-3c1f-4f11-9068-83206c127eac"), null, new DateOnly(2024, 8, 15), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(11, 0, 0) },
                    { new Guid("9176ad72-94ed-46ee-b44d-6f2c890b08b2"), null, new DateOnly(2024, 8, 14), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(12, 0, 0) },
                    { new Guid("92b6136b-0d55-466c-b08c-6059a6d60bc3"), null, new DateOnly(2024, 8, 13), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(11, 0, 0) },
                    { new Guid("9598d99f-d25b-471a-837b-ac371560948c"), null, new DateOnly(2024, 8, 14), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(17, 0, 0) },
                    { new Guid("98ee869c-539c-4edd-ad9e-9e528bc82f5f"), null, new DateOnly(2024, 8, 12), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(15, 0, 0) },
                    { new Guid("9bf71b9d-6b96-429c-bb56-da3cfe2309e2"), null, new DateOnly(2024, 8, 13), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(12, 0, 0) },
                    { new Guid("9c3f3105-4c0b-48dd-9b97-265adf107410"), null, new DateOnly(2024, 8, 14), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(17, 0, 0) },
                    { new Guid("9e10ca29-4f41-4352-9a39-86a504cebd6d"), null, new DateOnly(2024, 8, 12), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(10, 0, 0) },
                    { new Guid("9e1970b6-ebf0-477f-afb9-957e6313ad90"), null, new DateOnly(2024, 8, 16), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(17, 0, 0) },
                    { new Guid("9e414c1a-7b9b-4187-8721-e13c9194cc30"), null, new DateOnly(2024, 8, 15), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(12, 0, 0) },
                    { new Guid("9ed26f6c-65d3-4ae7-ac25-01d386f8a0dd"), null, new DateOnly(2024, 8, 16), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(14, 0, 0) },
                    { new Guid("a0edde3f-cacc-464a-bf1f-0580e70e82bc"), null, new DateOnly(2024, 8, 13), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(11, 0, 0) },
                    { new Guid("a32d4005-7a64-4915-956e-b2fc5723dba0"), null, new DateOnly(2024, 8, 12), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(14, 0, 0) },
                    { new Guid("a3487a8e-8a03-4b4c-8caf-876e799528d5"), null, new DateOnly(2024, 8, 16), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(15, 0, 0) },
                    { new Guid("a3c72672-4a04-49c2-9562-002a7f9368dc"), null, new DateOnly(2024, 8, 15), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(14, 0, 0) },
                    { new Guid("a597c3e1-72d5-4da1-9855-90e3fafbff8e"), null, new DateOnly(2024, 8, 12), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(11, 0, 0) },
                    { new Guid("a7ce35dd-d9f6-444b-be61-416dc0ca544b"), null, new DateOnly(2024, 8, 15), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(15, 0, 0) },
                    { new Guid("a8e94234-9270-4472-b916-67fb2a69ca97"), null, new DateOnly(2024, 8, 14), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(14, 0, 0) },
                    { new Guid("a934c8e3-2543-4fff-931b-9c13d8134200"), null, new DateOnly(2024, 8, 12), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(9, 0, 0) },
                    { new Guid("aa0c63b9-e5d4-4935-8282-65aa5fb20c8e"), null, new DateOnly(2024, 8, 15), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(13, 0, 0) },
                    { new Guid("aa9f1369-a9ec-4c57-b6b8-76d3cbe03c52"), null, new DateOnly(2024, 8, 14), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(14, 0, 0) },
                    { new Guid("ab3b7a19-40ca-48e2-a6a6-d39e43b67c9c"), null, new DateOnly(2024, 8, 12), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(11, 0, 0) },
                    { new Guid("ad243162-b937-4f53-aaf4-9b7f93ec474d"), null, new DateOnly(2024, 8, 15), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(10, 0, 0) },
                    { new Guid("ade55d16-97bf-441e-bf43-cdf72a86f7a6"), null, new DateOnly(2024, 8, 12), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(13, 0, 0) },
                    { new Guid("af5911a5-e84f-4389-83fd-31efce649673"), null, new DateOnly(2024, 8, 13), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(13, 0, 0) },
                    { new Guid("b0c970ec-8262-4015-aa28-825d86310023"), null, new DateOnly(2024, 8, 12), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(14, 0, 0) },
                    { new Guid("b4858847-b8c4-4b52-b606-97f360957e68"), null, new DateOnly(2024, 8, 15), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(15, 0, 0) },
                    { new Guid("b637b097-bb80-4a0f-99a1-049a852cb2f3"), null, new DateOnly(2024, 8, 15), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(11, 0, 0) },
                    { new Guid("b7fea78e-e6f8-47c9-a707-637f3c2decf5"), null, new DateOnly(2024, 8, 14), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(9, 0, 0) },
                    { new Guid("bc320a04-1b2c-44d4-971a-f15d14453bd5"), null, new DateOnly(2024, 8, 14), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(13, 0, 0) },
                    { new Guid("bce3284a-b53a-411c-ac55-38e14329c598"), null, new DateOnly(2024, 8, 14), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(11, 0, 0) },
                    { new Guid("c222f1dc-2cb0-478a-aa47-766246a00bd7"), null, new DateOnly(2024, 8, 12), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(15, 0, 0) },
                    { new Guid("c7f03ada-ccd9-4d90-8357-d5adad299f52"), null, new DateOnly(2024, 8, 16), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(12, 0, 0) },
                    { new Guid("c88319f8-9af7-49ce-8fa6-924badf98fa4"), null, new DateOnly(2024, 8, 14), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(15, 0, 0) },
                    { new Guid("ca2e76f6-6c2f-4f21-b11a-51007e9aa1eb"), null, new DateOnly(2024, 8, 13), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(17, 0, 0) },
                    { new Guid("cb4c463c-4122-4b89-bf31-0455d3711366"), null, new DateOnly(2024, 8, 16), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(17, 0, 0) },
                    { new Guid("cb4f65f3-f348-4395-924c-bd29aae23d1b"), null, new DateOnly(2024, 8, 16), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(9, 0, 0) },
                    { new Guid("cc4dc173-8ec3-4705-96d3-7187c5d97afc"), null, new DateOnly(2024, 8, 16), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(16, 0, 0) },
                    { new Guid("d17fbeed-908b-43bb-8381-7e09609c7d46"), null, new DateOnly(2024, 8, 15), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(9, 0, 0) },
                    { new Guid("d1fed78a-0f5a-4b3e-aa36-1c37690427bb"), null, new DateOnly(2024, 8, 13), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(17, 0, 0) },
                    { new Guid("d3b70ed9-36f9-4c42-b458-8c4219261807"), null, new DateOnly(2024, 8, 16), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(14, 0, 0) },
                    { new Guid("d46b608e-6dae-44d6-98f5-b6a8de179f3f"), null, new DateOnly(2024, 8, 12), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(16, 0, 0) },
                    { new Guid("d84c561c-60d0-4a33-a0f3-e36bb043de0d"), null, new DateOnly(2024, 8, 12), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(13, 0, 0) },
                    { new Guid("d8b84a74-0435-4001-810e-ad2c45606159"), null, new DateOnly(2024, 8, 15), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(9, 0, 0) },
                    { new Guid("dd2ef495-c3e4-406c-8700-a4b30d9f4962"), null, new DateOnly(2024, 8, 13), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(15, 0, 0) },
                    { new Guid("df08ab34-72a1-4202-a79c-3b3d52c20213"), null, new DateOnly(2024, 8, 12), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(14, 0, 0) },
                    { new Guid("e0829ef1-57d2-4d6f-bf04-6fd77c052868"), null, new DateOnly(2024, 8, 16), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(10, 0, 0) },
                    { new Guid("e1fe2763-47b4-4efd-9c0b-ea5af7703ada"), null, new DateOnly(2024, 8, 13), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(14, 0, 0) },
                    { new Guid("e353a750-1b9c-47dc-a598-275ecce0bb3b"), null, new DateOnly(2024, 8, 15), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(17, 0, 0) },
                    { new Guid("e429f349-407e-44cc-904d-9bef064f879a"), null, new DateOnly(2024, 8, 16), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(9, 0, 0) },
                    { new Guid("e60b55c3-0fef-4e7a-95df-405f8bebd208"), null, new DateOnly(2024, 8, 15), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(10, 0, 0) },
                    { new Guid("e9ce341e-c753-4eef-b4c9-575f270749ac"), null, new DateOnly(2024, 8, 15), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(13, 0, 0) },
                    { new Guid("edaed46e-a532-42ff-9836-cccb68a39dcf"), null, new DateOnly(2024, 8, 15), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(16, 0, 0) },
                    { new Guid("edb0d430-f377-4ee7-b50f-ca83ad8aa941"), null, new DateOnly(2024, 8, 14), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(11, 0, 0) },
                    { new Guid("edd6f17d-d667-4190-9508-8f5c5ecfb821"), null, new DateOnly(2024, 8, 12), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(16, 0, 0) },
                    { new Guid("ee2c8a07-59e4-47f6-b3d9-d57679cd14a7"), null, new DateOnly(2024, 8, 15), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(11, 0, 0) },
                    { new Guid("ee48f6e2-52cd-4ac2-a48c-7bd89bfdbf2b"), null, new DateOnly(2024, 8, 13), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(9, 0, 0) },
                    { new Guid("ef9a859a-f756-43c4-af54-18d6bf4e82de"), null, new DateOnly(2024, 8, 14), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(13, 0, 0) },
                    { new Guid("f170aa62-effb-433f-8433-bbef5d1084f4"), null, new DateOnly(2024, 8, 14), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(10, 0, 0) },
                    { new Guid("f77547df-8571-46db-80c6-0fade4282a93"), null, new DateOnly(2024, 8, 15), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(12, 0, 0) },
                    { new Guid("f7f150e0-ae18-411a-baef-161d3a85e316"), null, new DateOnly(2024, 8, 13), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(11, 0, 0) },
                    { new Guid("f822f10c-75d1-433d-83c2-4691d50edb2a"), null, new DateOnly(2024, 8, 13), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(17, 0, 0) },
                    { new Guid("f90a1aa1-5595-4626-ad81-2059bb56f554"), null, new DateOnly(2024, 8, 13), false, new Guid("2a0b3742-89c7-4e97-8b37-148bf45d005c"), new TimeOnly(9, 0, 0) },
                    { new Guid("fd6a4cc9-5d47-4c2a-bfee-d6d77cce9531"), null, new DateOnly(2024, 8, 12), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(12, 0, 0) },
                    { new Guid("ff0aaf3d-f303-410c-b407-9f214c0e3a50"), null, new DateOnly(2024, 8, 16), false, new Guid("36c88864-5d87-420a-9e25-914faa5fcf18"), new TimeOnly(9, 0, 0) },
                    { new Guid("ffdcf690-5a05-4c0e-a173-fe64b1949a69"), null, new DateOnly(2024, 8, 12), false, new Guid("542eb137-873f-4920-88e3-b7401c36385f"), new TimeOnly(16, 0, 0) }
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
