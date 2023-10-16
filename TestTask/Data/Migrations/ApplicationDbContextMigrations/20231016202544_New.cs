using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TestTask.Data.Migrations.ApplicationDbContextMigrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<byte[]>(type: "bytea", nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherArchiveFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherArchiveFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArchiveSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    WeatherArchiveFileId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchiveSheets_WeatherArchiveFiles_WeatherArchiveFileId",
                        column: x => x.WeatherArchiveFileId,
                        principalTable: "WeatherArchiveFiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WeatherArchives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MoscowTime = table.Column<string>(type: "text", nullable: false),
                    T = table.Column<double>(type: "double precision", nullable: false),
                    AirHumidity = table.Column<double>(type: "double precision", nullable: false),
                    Td = table.Column<double>(type: "double precision", nullable: false),
                    Pressure = table.Column<double>(type: "double precision", nullable: false),
                    AirDirection = table.Column<string>(type: "text", nullable: false),
                    AirSpeed = table.Column<double>(type: "double precision", nullable: false),
                    Cloudness = table.Column<double>(type: "double precision", nullable: false),
                    H = table.Column<double>(type: "double precision", nullable: false),
                    VV = table.Column<double>(type: "double precision", nullable: false),
                    Phenomena = table.Column<string>(type: "text", nullable: false),
                    ArchiveSheetId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherArchives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeatherArchives_ArchiveSheets_ArchiveSheetId",
                        column: x => x.ArchiveSheetId,
                        principalTable: "ArchiveSheets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArchiveSheets_WeatherArchiveFileId",
                table: "ArchiveSheets",
                column: "WeatherArchiveFileId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherArchives_ArchiveSheetId",
                table: "WeatherArchives",
                column: "ArchiveSheetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "WeatherArchives");

            migrationBuilder.DropTable(
                name: "ArchiveSheets");

            migrationBuilder.DropTable(
                name: "WeatherArchiveFiles");
        }
    }
}
