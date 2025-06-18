using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiPlayground.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "gemini-2.0-flash");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "deepseek-reasoner");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "deepseek-chat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Gemini 1.5");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "DeepSeek-R1");

            migrationBuilder.UpdateData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "DeepSeek-V3");
        }
    }
}
