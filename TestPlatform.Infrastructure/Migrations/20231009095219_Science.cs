using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Science : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sciences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sciences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScienceTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScienceTypesId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CountQuizzes = table.Column<int>(type: "int", nullable: false),
                    isPrivate = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScienceTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScienceTypes_Sciences_ScienceTypesId",
                        column: x => x.ScienceTypesId,
                        principalTable: "Sciences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScienceTypes_ScienceTypesId",
                table: "ScienceTypes",
                column: "ScienceTypesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScienceTypes");

            migrationBuilder.DropTable(
                name: "Sciences");
        }
    }
}
