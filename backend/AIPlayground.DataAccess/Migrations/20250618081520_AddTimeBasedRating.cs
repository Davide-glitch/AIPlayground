using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AiPlayground.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeBasedRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResponseTimeMs",
                table: "Runs",
                type: "int",
                nullable: false,
                defaultValue: 2000);

            migrationBuilder.InsertData(
                table: "Prompts",
                columns: new[] { "Id", "ExpectedResult", "Name", "ScopeId", "SystemMessage", "UserMessage" },
                values: new object[,]
                {
                    { 1, "A friendly greeting message.", "Basic Greeting", 1, "You are a helpful assistant.", "Say hello to the user in a friendly way." },
                    { 2, "Code review with suggestions for improvement.", "Code Review", 2, "You are an expert code reviewer.", "Review this Python function for best practices." },
                    { 3, "An engaging opening paragraph for a mystery story.", "Story Beginning", 3, "You are a creative writer.", "Write the opening paragraph of a mystery novel." }
                });

            migrationBuilder.InsertData(
                table: "Runs",
                columns: new[] { "Id", "ActualResponse", "ModelId", "PromptId", "Rating", "ResponseTimeMs", "Temperature", "UserRating" },
                values: new object[,]
                {
                    { 1, "Hello! I'm delighted to meet you! How can I assist you today?", 1, 1, 4.5, 800, 0.69999999999999996, 4.5 },
                    { 2, "The code looks good but could benefit from type hints and docstrings.", 1, 2, 4.2000000000000002, 1200, 0.29999999999999999, 4.0 },
                    { 3, "The fog rolled in like a silent predator, consuming the streetlights one by one.", 1, 3, 4.7999999999999998, 900, 0.80000000000000004, 4.5 },
                    { 4, "Hi there! Nice to meet you!", 2, 1, 3.7999999999999998, 600, 0.69999999999999996, 3.5 },
                    { 5, "Your code is functional. Consider adding error handling.", 2, 2, 3.5, 750, 0.29999999999999999, 3.7999999999999998 },
                    { 6, "Greetings! I'm here to help with whatever you need.", 3, 1, 4.0999999999999996, 2500, 0.69999999999999996, 4.2000000000000002 },
                    { 7, "Detective Sarah Chen had seen enough crime scenes to know when something was off.", 3, 3, 4.2999999999999998, 3200, 0.80000000000000004, 4.0999999999999996 },
                    { 8, "This function demonstrates good separation of concerns. I recommend adding unit tests.", 4, 2, 4.5999999999999996, 8500, 0.20000000000000001, 4.7999999999999998 },
                    { 9, "Hello! I'm ready to assist you with any questions you might have.", 4, 1, 3.8999999999999999, 12000, 0.5, 4.0 },
                    { 10, "Hey! Great to meet you. What can I help you with today?", 5, 1, 3.7000000000000002, 4200, 0.59999999999999998, 3.8999999999999999 },
                    { 11, "The old mansion stood silent against the stormy night, hiding secrets within its walls.", 5, 3, 4.0, 5800, 0.90000000000000002, 3.7999999999999998 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Runs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Runs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Runs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Runs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Runs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Runs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Runs",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Runs",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Runs",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Runs",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Runs",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prompts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "ResponseTimeMs",
                table: "Runs");
        }
    }
}
