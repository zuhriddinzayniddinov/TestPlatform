using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Exam2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ScienceId = table.Column<long>(type: "bigint", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizInExams",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<long>(type: "bigint", nullable: false),
                    QuizId = table.Column<long>(type: "bigint", nullable: false),
                    QuizStatus = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerGuid1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerGuid2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerGuid3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerGuid4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GivenGuid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizInExams", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "QuizInExams");
        }
    }
}
