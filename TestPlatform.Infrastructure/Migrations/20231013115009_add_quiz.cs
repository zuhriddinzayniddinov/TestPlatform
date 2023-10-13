using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_quiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScienceId = table.Column<long>(type: "bigint", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wrong1Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wrong1Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wrong2Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wrong2Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Wrong3Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wrong3Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Quizzes_ScienceTypes_ScienceId",
                        column: x => x.ScienceId,
                        principalTable: "ScienceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_ScienceId",
                table: "Quizzes",
                column: "ScienceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quizzes");
        }
    }
}
