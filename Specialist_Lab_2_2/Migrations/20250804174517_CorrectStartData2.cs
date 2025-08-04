using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Specialist_Lab_2_2.Migrations
{
    /// <inheritdoc />
    public partial class CorrectStartData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseTeacher",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CourseTeacher",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.InsertData(
                table: "CourseStudent",
                columns: new[] { "Id", "CourseId", "StudentId" },
                values: new object[,]
                {
                    { 10, 2, 1 },
                    { 11, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "CourseTeacher",
                columns: new[] { "Id", "CourseId", "TeacherId" },
                values: new object[,]
                {
                    { 2, 2, 2 },
                    { 3, 3, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseStudent",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CourseStudent",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CourseTeacher",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CourseTeacher",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "CourseTeacher",
                columns: new[] { "Id", "CourseId", "TeacherId" },
                values: new object[,]
                {
                    { 4, 2, 2 },
                    { 6, 3, 3 }
                });
        }
    }
}
