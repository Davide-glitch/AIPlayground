using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiPlayground.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PendingModelChanges_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Run_Prompt",
                table: "Run");

            migrationBuilder.DropForeignKey(
                name: "FK_Run_Model",
                table: "Run");

            migrationBuilder.DropForeignKey(
                name: "FK_Prompt_Scope",
                table: "Prompt");

            migrationBuilder.DropForeignKey(
                name: "FK_Model_Platform",
                table: "Model");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scope",
                table: "Scope");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Run",
                table: "Run");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prompt",
                table: "Prompt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Platform",
                table: "Platform");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Model",
                table: "Model");

            migrationBuilder.RenameTable(
                name: "Scope",
                newName: "Scopes");

            migrationBuilder.RenameTable(
                name: "Run",
                newName: "Runs");

            migrationBuilder.RenameTable(
                name: "Prompt",
                newName: "Prompts");

            migrationBuilder.RenameTable(
                name: "Platform",
                newName: "Platforms");

            migrationBuilder.RenameTable(
                name: "Model",
                newName: "Models");

            migrationBuilder.RenameIndex(
                name: "IX_Run_PromptId",
                table: "Runs",
                newName: "IX_Runs_PromptId");

            migrationBuilder.RenameIndex(
                name: "IX_Run_ModelId",
                table: "Runs",
                newName: "IX_Runs_ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Prompt_ScopeId",
                table: "Prompts",
                newName: "IX_Prompts_ScopeId");

            migrationBuilder.RenameIndex(
                name: "IX_Model_PlatformId",
                table: "Models",
                newName: "IX_Models_PlatformId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scopes",
                table: "Scopes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Runs",
                table: "Runs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prompts",
                table: "Prompts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Platforms",
                table: "Platforms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Models",
                table: "Models",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Model_Platform",
                table: "Models",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prompt_Scope",
                table: "Prompts",
                column: "ScopeId",
                principalTable: "Scopes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Run_Model",
                table: "Runs",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Run_Prompt",
                table: "Runs",
                column: "PromptId",
                principalTable: "Prompts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Run_Prompt",
                table: "Runs");

            migrationBuilder.DropForeignKey(
                name: "FK_Run_Model",
                table: "Runs");

            migrationBuilder.DropForeignKey(
                name: "FK_Prompt_Scope",
                table: "Prompts");

            migrationBuilder.DropForeignKey(
                name: "FK_Model_Platform",
                table: "Models");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Scopes",
                table: "Scopes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Runs",
                table: "Runs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prompts",
                table: "Prompts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Platforms",
                table: "Platforms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Models",
                table: "Models");

            migrationBuilder.RenameTable(
                name: "Scopes",
                newName: "Scope");

            migrationBuilder.RenameTable(
                name: "Runs",
                newName: "Run");

            migrationBuilder.RenameTable(
                name: "Prompts",
                newName: "Prompt");

            migrationBuilder.RenameTable(
                name: "Platforms",
                newName: "Platform");

            migrationBuilder.RenameTable(
                name: "Models",
                newName: "Model");

            migrationBuilder.RenameIndex(
                name: "IX_Runs_PromptId",
                table: "Run",
                newName: "IX_Run_PromptId");

            migrationBuilder.RenameIndex(
                name: "IX_Runs_ModelId",
                table: "Run",
                newName: "IX_Run_ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Prompts_ScopeId",
                table: "Prompt",
                newName: "IX_Prompt_ScopeId");

            migrationBuilder.RenameIndex(
                name: "IX_Models_PlatformId",
                table: "Model",
                newName: "IX_Model_PlatformId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Scope",
                table: "Scope",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Run",
                table: "Run",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prompt",
                table: "Prompt",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Platform",
                table: "Platform",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Model",
                table: "Model",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Model_Platform",
                table: "Model",
                column: "PlatformId",
                principalTable: "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prompt_Scope",
                table: "Prompt",
                column: "ScopeId",
                principalTable: "Scope",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Run_Model",
                table: "Run",
                column: "ModelId",
                principalTable: "Model",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Run_Prompt",
                table: "Run",
                column: "PromptId",
                principalTable: "Prompt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
