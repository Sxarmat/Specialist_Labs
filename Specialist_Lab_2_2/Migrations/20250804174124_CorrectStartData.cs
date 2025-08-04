using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Specialist_Lab_2_2.Migrations
{
    /// <inheritdoc />
    public partial class CorrectStartData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseTeacher",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CourseTeacher",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CourseTeacher",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "CourseTeacher",
                keyColumn: "Id",
                keyValue: 4,
                column: "TeacherId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "CourseTeacher",
                keyColumn: "Id",
                keyValue: 6,
                column: "TeacherId",
                value: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CourseTeacher",
                keyColumn: "Id",
                keyValue: 4,
                column: "TeacherId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "CourseTeacher",
                keyColumn: "Id",
                keyValue: 6,
                column: "TeacherId",
                value: 2);

            migrationBuilder.InsertData(
                table: "CourseTeacher",
                columns: new[] { "Id", "CourseId", "TeacherId" },
                values: new object[,]
                {
                    { 3, 1, 2 },
                    { 5, 2, 3 },
                    { 7, 3, 3 }
                });
        }
    }
}
